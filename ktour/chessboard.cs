using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// Author: Kellen Whitaker

public class ChessBoard
{
    #region Fields
	public enum SquareState { Empty, Visited, Occupied };
	List<List<SquareState>> squares = new List<List<SquareState>>();
	Location currentLoc = new Location(-1, -1);
	Stack<Location> prevLocations = new Stack<Location>();
	Stack<Location> solution;
	List<List<int>> numMovesFromSquare = new List<List<int>>();
    Location[] ValidRelativeMoves = new[] { new Location(1, 2), new Location(-1, 2), new Location(1, -2), new Location(-1, -2),
        new Location(2, 1), new Location(-2, 1), new Location(2, -1), new Location(-2, -1)};
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
  
	private bool IsLocationInBounds(Location loc) => (loc.row >= 0 && loc.row < NumRows() && loc.col >= 0 && loc.col < NumCols());

    private void AddMovesToSurroundingSquares(Location loc, int numberToAdd)
    {
        foreach (Location move in ValidRelativeMoves)
            if (IsLocationInBounds(new Location(loc.row + move.row, loc.col + move.col)))
                numMovesFromSquare[loc.row + move.row][loc.col + move.col] += numberToAdd;
    }
  
	private void RecalcNumMovesFromSquares()
	{
		int numMoves;
		for (int row = 0; row < NumRows(); row++)
			for (int col = 0; col < NumCols(); col++)
			{
				numMoves = 0;
                foreach (Location move in ValidRelativeMoves)
                    if (CanMoveTo(new Location(row + move.row, col + move.col)))
                        numMoves++;

				numMovesFromSquare[row][col] = numMoves;
			}
	}

    public bool PlaceKnight(Location loc)
    {
        if (currentLoc.row == -1)
        {
            Move(loc);
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
        bool IsValidSolution = false;
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
            IsValidSolution = DoubleCheckSolution();            
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
        return IsValidSolution;
    }

    bool CanMoveTo(Location loc) => IsLocationInBounds(loc) && squares[loc.row][loc.col] == SquareState.Empty;

    void CheckIfBetterMove(Location move, ref Location bestMove, ref int lowestMoves, ref bool tied)
    {
        if (CanMoveTo(move) && numMovesFromSquare[move.row][move.col] <= lowestMoves 
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

        foreach (Location move in ValidRelativeMoves)
            CheckIfBetterMove(new Location(row + move.row, col + move.col), ref bestMove, ref lowestMoves, ref tied);

        return tied;
	}

    bool DoubleCheckSolution()
    {
        if (solution.Count != NumRows() * NumCols() - 1)
            return false;
        bool IsValidSolution = false;
        while (CanMoveForward()) { MoveSolutionForward(); }
        int occupied = 0;
        int visited = 0;
        for (int row = 0; row < NumRows(); row++)
            for (int col = 0; col < NumCols(); col++)
            {
                if (squares[row][col] == SquareState.Visited)
                    visited++;
                else if (squares[row][col] == SquareState.Occupied)
                    occupied++;
            }

        if (occupied == 1 && visited == NumRows() * NumCols() - 1)
            IsValidSolution = true;

        while (CanMoveBack()) { MoveSolutionBack(); }

        return IsValidSolution;
    }
    #endregion
}
