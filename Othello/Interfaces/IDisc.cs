using PieceDiscs;

namespace InterfaceDisc;

public interface IDisc
{
	public int IdDisc{get;}
	public Piece piece{get;}
	public bool placed{get;}
	public void SetColor(Piece piece) {}
	public void Disc(int IdDisc, Piece piece) {}
}
