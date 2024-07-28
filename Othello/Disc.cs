namespace Discs;
using System.Drawing;
using ColorDics = ColorDisc.Color;

public interface IDisc
{
    public int IdDisc{get;}
    public Color color;
    public bool place;
    public void SetColor(Color color) {}
    public void Disc(int IdDisc, Color color) {}
    public bool IsPlace();
    public void Place();
}
public class Disc : IDisc
{
    public int IdDisc{get; private set;}
    public Color color;
    private bool _place;
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
        return _place;   
    }
    public void Place()
    {
        _place = true;
    }
}