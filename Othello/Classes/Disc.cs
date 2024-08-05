using ColorDiscs;
using InterfaceDisc;

namespace Discs;

public class Disc : IDisc
{
	public int IdDisc{get; private set;}
	public Color color{get; set;}
	//FITRI - buat apa?
	public bool placed{get; set;} 
	//FITRI - color public, knp butuh setcolor?
	public void SetColor(Color color)
	{
		this.color = color;
	}
	public Disc(int IdDisc, Color color)
	{
		this.IdDisc = IdDisc;
		this.color = color;
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