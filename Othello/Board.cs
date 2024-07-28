using PositionBoard;
using Discs;
using Player;
using ColorDisc = ColorDisc.Color;
using System.Drawing;
namespace Board;

public class Board
{
    private Disc[,] discs = new Disc[8,8];
    public void InitializeBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                discs[x, y] = new Disc();
            }
        }
        // Place 4 discs
        discs[3,3] = new Disc(1, Color.White);
        discs[3,4] = new Disc(2, Color.Black);
        discs[4,3] = new Disc(3, Color.Black);
        discs[4,4] = new Disc(4, Color.White);

        // Mark the discs on board
        discs[3,3].Place();
        discs[3,4].Place();
        discs[4,3].Place();
        discs[4,4].Place();
    }
    public void GetBoard()
    {
        Disc[,] discs = new Disc[8,8];
    }
    public void CheckPlayer(Position position, IDisc dics)
    {
        
    }
    public IDisc GetDisc(Position position)
    {

    }
    public bool IsFull()
    {

    }
    public bool CheckWinner()
    {
        
    }
    public Player IPlayer GetWinner(Dictionary<IPlayer, Disc> _playerColors, IPlayer player1, IPlayer player2)
    {

    }
}