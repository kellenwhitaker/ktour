using System.Collections.Generic;
/*
public class PossibleMoves
{
	public List<Location> moves = new List<Location>();
	
	public PossibleMoves(Location currentLoc)
	{
		AddMove(new Location(currentLoc,  1,  2));
		AddMove(new Location(currentLoc,  1, -2));
		AddMove(new Location(currentLoc, -1,  2));
		AddMove(new Location(currentLoc, -1, -2));
		AddMove(new Location(currentLoc,  2,  1));
		AddMove(new Location(currentLoc,  2, -1));
		AddMove(new Location(currentLoc, -2,  1));
		AddMove(new Location(currentLoc, -2, -1));
	}

	public void SortByMoveCount(MoveCountDelegate moveCountDelegate)
	{
		moves.Sort((a, b) => moveCountDelegate(a).CompareTo(moveCountDelegate(b)));
	}

	public PossibleMoves(PossibleMoves existing)
	{
		moves.AddRange(existing.moves);
	}

	void AddMove(Location loc)
	{
		if (loc.row >= 0 && loc.row <= 7 && loc.col >= 0 && loc.col <= 7)
			moves.Add(loc);
	}

	public Location getMove()
	{
		Location move = moves[0];
		moves.RemoveAt(0);
		return move;
	}

	public int numMoves()
	{
		return moves.Count;
	}

	public bool hasValidMove()
	{
		return moves.Count > 0;
	}
  
}
*/