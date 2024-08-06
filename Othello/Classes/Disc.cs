using PieceDiscs;
using InterfaceDisc;

namespace Discs;

public class Disc : IDisc
{
	public int IdDisc{get; private set;}
	public Piece piece{get; set;}
	public bool placed{get; set;} 
	public void SetColor(Piece piece)
	{
		this.piece = piece;
	}
	public Disc(int IdDisc, Piece piece)
	{
		this.IdDisc = IdDisc;
		this.piece = piece;
	}
	// FITRI - dibuat apa?
	public bool IsPlace()
	{
		return placed;
	}
	// FITRI - dibuat apa?
	public void Place()
	{
		placed = true;
	}
}