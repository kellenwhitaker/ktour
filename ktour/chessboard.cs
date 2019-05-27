﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// Author: Kellen Whitaker
public delegate int MoveCountDelegate(Location loc);

public class ChessBoard
{
    #region Fields
	public enum SquareState { Empty, Visited, Occupied };
	List<List<SquareState>> squares = new List<List<SquareState>>();
	Location currentLoc = new Location(-1, -1);
	Stack<Location> prevLocations = new Stack<Location>();
	Stack<Location> solution;
	List<List<int>> numMovesFromSquare = new List<List<int>>();
    #endregion

    #region Constructors
	public ChessBoard() => BuildBoard(8, 8);

    public ChessBoard(int numRows, int numCols) => BuildBoard(numRows, numCols);
    #endregion

    #region Methods
    public int NumRows() => squares.Count;

	public int NumCols() => squares[0].Count;

    public SquareState SquareStateAt(int row, int col) => squares[row][col];

    public bool CanMoveForward() => solution.Count > 0;

    public bool CanMoveBack() => prevLocations.Count > 0;
 
    private void BuildBoard(int numRows, int numCols)
    {
        for (int row = 0; row < numRows; row++)
        {
            squares.Add(new List<SquareState>());
            numMovesFromSquare.Add(new List<int>());
            for (int col = 0; col < numCols; col++)
            {
                squares[row].Add(0);
                numMovesFromSquare[row].Add(0);
            }
        }
    }
  
	private bool IsLocationInBounds(int row, int col) => (row >= 0 && row < NumRows() && col >= 0 && col < NumCols());

    private void AddMovesToSurroundingSquares(Location loc, int numberToAdd)
    {
        if (IsLocationInBounds(loc.row + 1, loc.col + 2))
            numMovesFromSquare[loc.row + 1][loc.col + 2] += numberToAdd;
        if (IsLocationInBounds(loc.row - 1, loc.col + 2))
            numMovesFromSquare[loc.row - 1][loc.col + 2] += numberToAdd;
        if (IsLocationInBounds(loc.row + 1, loc.col - 2))
            numMovesFromSquare[loc.row + 1][loc.col - 2] += numberToAdd;
        if (IsLocationInBounds(loc.row - 1, loc.col - 2))
            numMovesFromSquare[loc.row - 1][loc.col - 2] += numberToAdd;
        if (IsLocationInBounds(loc.row + 2, loc.col + 1))
            numMovesFromSquare[loc.row + 2][loc.col + 1] += numberToAdd;
        if (IsLocationInBounds(loc.row - 2, loc.col + 1))
            numMovesFromSquare[loc.row - 2][loc.col + 1] += numberToAdd;
        if (IsLocationInBounds(loc.row + 2, loc.col - 1))
            numMovesFromSquare[loc.row + 2][loc.col - 1] += numberToAdd;
        if (IsLocationInBounds(loc.row - 2, loc.col - 1))
            numMovesFromSquare[loc.row - 2][loc.col - 1] += numberToAdd;
    }
  
	private void RecalcNumMovesFromSquares()
	{
		int numMoves;
		for (int row = 0; row < NumRows(); row++)
			for (int col = 0; col < NumCols(); col++)
			{
				numMoves = 0;
				if (CanMoveTo(row + 1, col + 2))
					numMoves++;
				if (CanMoveTo(row - 1, col + 2))
					numMoves++;
				if (CanMoveTo(row + 1, col - 2))
					numMoves++;
				if (CanMoveTo(row - 1, col - 2))
					numMoves++;
				if (CanMoveTo(row + 2, col + 1))
					numMoves++;
				if (CanMoveTo(row - 2, col + 1))
					numMoves++;
				if (CanMoveTo(row + 2, col - 1))
					numMoves++;
				if (CanMoveTo(row - 2, col - 1))
					numMoves++;
				numMovesFromSquare[row][col] = numMoves;
			}
	}

    public bool PlaceKnight(int row, int col)
    {
        if (currentLoc.row == -1)
        {
            Move(new Location(row, col));
            return true;
        }
        else
            return false;
    }

	void Move(Location loc)
	{
		if (currentLoc.row != -1)
		{
			prevLocations.Push(currentLoc);
			squares[currentLoc.row][currentLoc.col] = SquareState.Visited;
		}
		currentLoc = new Location(loc.row, loc.col);
		squares[currentLoc.row][currentLoc.col] = SquareState.Occupied;
	}
  
    public bool MoveSolutionForward()
    {
        if (solution.Peek() != null)
        {
            Move(solution.Pop());
            return true;
        }
        else
            return false;
    }

    public bool MoveSolutionBack()
    {
        if (prevLocations.Count > 0)
        {
            solution.Push(currentLoc);
            TakeBack();
            return true;
        }
        else
            return false;
    }

	private bool TakeBack()
	{
		if (prevLocations.Count >  0)
		{
			squares[currentLoc.row][currentLoc.col] = SquareState.Empty;
			currentLoc = prevLocations.Pop();
			squares[currentLoc.row][currentLoc.col] = SquareState.Occupied;
			return true;
		}
		else
		{
			return false;
		}
	}
  
	public bool AttemptWarnsdorfSolution()
	{
		Stopwatch watch = new Stopwatch();
		watch.Start();
        solution = new Stack<Location>();
		int moveCount = 0;
        RecalcNumMovesFromSquares();
		do
		{
            if (NextWarnsdorfMove(out Location nextMove))
                currentLoc.TiedLocation = true;
            
			if (nextMove.row != -1)
			{
                moveCount++;
				if (moveCount % 1000000 == 0)
					Console.WriteLine(moveCount);
                currentLoc.MovesTried.Add(nextMove);
                Move(nextMove);
                AddMovesToSurroundingSquares(currentLoc, -1);
            }
			else
			{
                while (prevLocations.Peek() != null)
                {
                    AddMovesToSurroundingSquares(currentLoc, 1);
                    TakeBack();
                    if (currentLoc.TiedLocation && currentLoc.MovesTried.Count == numMovesFromSquare[currentLoc.row][currentLoc.col])
                        currentLoc.TiedLocation = false;
                    if (currentLoc.TiedLocation)
                        break;
                }
			}
		} while (prevLocations.Count < NumRows() * NumCols() - 1);

        if (prevLocations.Count == NumRows() * NumCols() - 1)
        {
            solution.Push(currentLoc);
            Location prevLocation;
            while (prevLocations.Count > 1)
            {
                prevLocation = prevLocations.Peek();
                solution.Push(prevLocation);
                TakeBack();
            }
            TakeBack();
            Console.WriteLine(watch.ElapsedMilliseconds);
            return true;
        }
        else
            return false;
	}

    bool CanMoveTo(int row, int col) => IsLocationInBounds(row, col) && squares[row][col] == SquareState.Empty;

    void CheckIfBetterMove(Location move, ref Location bestMove, ref int lowestMoves, ref bool tied)
    {
        if (CanMoveTo(move.row, move.col) && numMovesFromSquare[move.row][move.col] <= lowestMoves 
            && currentLoc.MovesTried.Where(x => x.row == move.row && x.col == move.col).Count() == 0)
        {
            if (numMovesFromSquare[move.row][move.col] == lowestMoves)
                tied = true;
            else
            {
                tied = false;
                lowestMoves = numMovesFromSquare[move.row][move.col];
                bestMove = new Location(move.row, move.col);
            }
        }
    }
	bool NextWarnsdorfMove(out Location bestMove)
	{
		bestMove = new Location(-1, -1);
        int row = currentLoc.row;
        int col = currentLoc.col;
        int lowestMoves = 9;
        bool tied = false;
        Location move = new Location(row + 1, col + 2);
        CheckIfBetterMove(move, ref bestMove, ref lowestMoves, ref tied);
        move = new Location(row - 1, col + 2);
        CheckIfBetterMove(move, ref bestMove, ref lowestMoves, ref tied);
        move = new Location(row + 1, col - 2);
        CheckIfBetterMove(move, ref bestMove, ref lowestMoves, ref tied);
        move = new Location(row - 1, col - 2);
        CheckIfBetterMove(move, ref bestMove, ref lowestMoves, ref tied);
        move = new Location(row + 2, col + 1);
        CheckIfBetterMove(move, ref bestMove, ref lowestMoves, ref tied);
        move = new Location(row - 2, col + 1);
        CheckIfBetterMove(move, ref bestMove, ref lowestMoves, ref tied);
        move = new Location(row + 2, col - 1);
        CheckIfBetterMove(move, ref bestMove, ref lowestMoves, ref tied);
        move = new Location(row - 2, col - 1);
        CheckIfBetterMove(move, ref bestMove, ref lowestMoves, ref tied);
        return tied;
	}
    #endregion
}
