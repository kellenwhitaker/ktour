using System.Collections.Generic;

public class Location
{
	public int row, col;
    public bool TiedLocation = false;
    public List<Location> MovesTried = new List<Location>();

	public Location(int row, int col)
	{
		this.row = row;
		this.col = col;
	}  

	public Location(Location currentLoc, int dRow, int dCol)
	{
		row = currentLoc.row + dRow;
		col = currentLoc.col + dCol;
	}
}
