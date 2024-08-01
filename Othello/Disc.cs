using ColorDiscs;
using InterfaceDisc;

namespace Discs;

public class Disc : IDisc
{
	public int IdDisc{get; private set;}
	public Color color;
	private bool _placed;
	public void SetColor(Color color)
	{
		this.color = color;
	}
	public Disc(int IdDisc, Color color)
	{
		this.IdDisc = IdDisc;
		this.color = color;
	}
	public bool IsPlace()
	{
		return _placed;
	}
	public void Place()
	{
		_placed = true;
	}
}