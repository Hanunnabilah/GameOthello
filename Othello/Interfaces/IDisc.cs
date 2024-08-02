using ColorDiscs;

namespace InterfaceDisc;

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
