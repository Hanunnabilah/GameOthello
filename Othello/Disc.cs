using ColorDiscs;

namespace Discs;

public interface IDisc
{
	public int IdDisc{get;}
	public Color color;
	public bool placed;
	public void SetColor(Color color) {}
	public void Disc(int IdDisc, Color color) {}
	public bool IsPlace();
	public void Place();
}
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
		
	}
}