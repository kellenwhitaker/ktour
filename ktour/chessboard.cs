using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// Author: Kellen Whitaker
public delegate int MoveCountDelegate(Location loc);

public class ChessBoard
{
	public enum SquareState { Empty, Visited, Occupied };
	List<List<SquareState>> squares = new List<List<SquareState>>();
	Location currentLoc = new Location(-1, -1);
	Stack<Location> prevLocations = new Stack<Location>();
	Stack<Location> solution;
	List<List<int>> numMovesFromSquare = new List<List<int>>();
  
	public ChessBoard()
	{
        BuildBoard(8, 8);
	}

    public bool CanMoveForward()
    {
        return solution.Count > 0;
    }

    public bool CanMoveBack()
    {
        return prevLocations.Count > 0;
    }

    public SquareState SquareStateAt(int row, int col)
    {
        return squares[row][col];
    }

    public ChessBoard(int numRows, int numCols)
    {
        BuildBoard(numRows, numCols);
    }

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
  
	private bool IsLocationInBounds(int row, int col)
	{
		return (row >= 0 && row < NumRows() && col >= 0 && col < NumCols());
	}

    private void AddOneMoveToSurroundingSquares(Location loc)
    {
        if (IsLocationInBounds(loc.row + 1, loc.col + 2))
            numMovesFromSquare[loc.row + 1][loc.col + 2]++;
        if (IsLocationInBounds(loc.row - 1, loc.col + 2))
            numMovesFromSquare[loc.row - 1][loc.col + 2]++;
        if (IsLocationInBounds(loc.row + 1, loc.col - 2))
            numMovesFromSquare[loc.row + 1][loc.col - 2]++;
        if (IsLocationInBounds(loc.row - 1, loc.col - 2))
            numMovesFromSquare[loc.row - 1][loc.col - 2]++;
        if (IsLocationInBounds(loc.row + 2, loc.col + 1))
            numMovesFromSquare[loc.row + 2][loc.col + 1]++;
        if (IsLocationInBounds(loc.row - 2, loc.col + 1))
            numMovesFromSquare[loc.row - 2][loc.col + 1]++;
        if (IsLocationInBounds(loc.row + 2, loc.col - 1))
            numMovesFromSquare[loc.row + 2][loc.col - 1]++;
        if (IsLocationInBounds(loc.row - 2, loc.col - 1))
            numMovesFromSquare[loc.row - 2][loc.col - 1]++;
    }

    private void SubtractOneMoveFromSurroundingSquares(Location loc)
    {
        if (IsLocationInBounds(loc.row + 1, loc.col + 2))
            numMovesFromSquare[loc.row + 1][loc.col + 2]--;
        if (IsLocationInBounds(loc.row - 1, loc.col + 2))
            numMovesFromSquare[loc.row - 1][loc.col + 2]--;
        if (IsLocationInBounds(loc.row + 1, loc.col - 2))
            numMovesFromSquare[loc.row + 1][loc.col - 2]--;
        if (IsLocationInBounds(loc.row - 1, loc.col - 2))
            numMovesFromSquare[loc.row - 1][loc.col - 2]--;
        if (IsLocationInBounds(loc.row + 2, loc.col + 1))
            numMovesFromSquare[loc.row + 2][loc.col + 1]--;
        if (IsLocationInBounds(loc.row - 2, loc.col + 1))
            numMovesFromSquare[loc.row - 2][loc.col + 1]--;
        if (IsLocationInBounds(loc.row + 2, loc.col - 1))
            numMovesFromSquare[loc.row + 2][loc.col - 1]--;
        if (IsLocationInBounds(loc.row - 2, loc.col - 1))
            numMovesFromSquare[loc.row - 2][loc.col - 1]--;
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
  
	public bool AttemptSolutionWarnsdorf()
	{
		Stopwatch watch = new Stopwatch();
		watch.Start();
        solution = new Stack<Location>();
		int moveCount = 0;
        RecalcNumMovesFromSquares();
		do
		{
            if (NextMoveWarnsdorf(out Location nextMove))
                currentLoc.TiedLocation = true;
            
			if (nextMove.row != -1)
			{
                moveCount++;
				if (moveCount % 1000000 == 0)
					Console.WriteLine(moveCount);
                currentLoc.MovesTried.Add(nextMove);
                Move(nextMove);
                //RecalcNumMovesFromSquares();
                SubtractOneMoveFromSurroundingSquares(currentLoc);
            }
			else
			{
                while (prevLocations.Peek() != null)
                {
                    AddOneMoveToSurroundingSquares(currentLoc);
                    TakeBack();
                    //RecalcNumMovesFromSquares();
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

    bool CanMoveTo(int row, int col)
    {
        return IsLocationInBounds(row, col) && squares[row][col] == SquareState.Empty;
    }

	bool NextMoveWarnsdorf(out Location bestMove)
	{
		bestMove = new Location(-1, -1);
        int row = currentLoc.row;
        int col = currentLoc.col;
        int lowestMoves = 9;
        bool tied = false;
        Location move = new Location(row + 1, col + 2);
        if (CanMoveTo(move.row, move.col) && numMovesFromSquare[move.row][move.col] <= lowestMoves && currentLoc.MovesTried.Where(x => x.row == move.row && x.col == move.col).Count() == 0)
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
        move = new Location(row - 1, col + 2);
        if (CanMoveTo(move.row, move.col) && numMovesFromSquare[move.row][move.col] <= lowestMoves && currentLoc.MovesTried.Where(x => x.row == move.row && x.col == move.col).Count() == 0)
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
        move = new Location(row + 1, col - 2);
        if (CanMoveTo(move.row, move.col) && numMovesFromSquare[move.row][move.col] <= lowestMoves && currentLoc.MovesTried.Where(x => x.row == move.row && x.col == move.col).Count() == 0)
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
        move = new Location(row - 1, col - 2);
        if (CanMoveTo(move.row, move.col) && numMovesFromSquare[move.row][move.col] <= lowestMoves && currentLoc.MovesTried.Where(x => x.row == move.row && x.col == move.col).Count() == 0)
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
        move = new Location(row + 2, col + 1);
        if (CanMoveTo(move.row, move.col) && numMovesFromSquare[move.row][move.col] <= lowestMoves && currentLoc.MovesTried.Where(x => x.row == move.row && x.col == move.col).Count() == 0)
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
        move = new Location(row - 2, col + 1);
        if (CanMoveTo(move.row, move.col) && numMovesFromSquare[move.row][move.col] <= lowestMoves && currentLoc.MovesTried.Where(x => x.row == move.row && x.col == move.col).Count() == 0)
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
        move = new Location(row + 2, col - 1);
        if (CanMoveTo(move.row, move.col) && numMovesFromSquare[move.row][move.col] <= lowestMoves && currentLoc.MovesTried.Where(x => x.row == move.row && x.col == move.col).Count() == 0)
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
        move = new Location(row - 2, col - 1);
        if (CanMoveTo(move.row, move.col) && numMovesFromSquare[move.row][move.col] <= lowestMoves && currentLoc.MovesTried.Where(x => x.row == move.row && x.col == move.col).Count() == 0)
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
        return tied;
	}

	void PrintBoard()
	{
		String strRow;
		for (int row = 0; row < NumRows(); row++)
		{
			strRow = "";
			for (int col = 0; col < NumCols(); col++)
			{
				if (squares[row][col] == SquareState.Empty)
					strRow += "O";
				else if (squares[row][col] == SquareState.Occupied)
					strRow += "-";
				else
					strRow += "X";
			}
			Console.WriteLine(strRow);
		}
        Console.WriteLine("");
	}

	public int NumRows()
	{
		return squares.Count;
	}

	public int NumCols()
	{
		return squares[0].Count;
	}
}
