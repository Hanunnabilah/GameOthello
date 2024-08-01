using PositionBoard;
using Discs;
using Player;
using ColorDiscs;
using InterfaceDisc;
using GameResult;
using System.Security.Cryptography.X509Certificates;
namespace Board;

public class Board
{
	private Disc[,] discs = new Disc[8,8];
	public void InitializeBoard()
	{
		// set position in center board
        int mid = discs.GetLength(0) / 2;

        for (int x = 0; x < discs.GetLength(0); x++)
        {
            for(int y = 0; y < discs.GetLength(1); y++)
            {
                discs[x, y] = null;
            }
        }
        // set 4 discs for first place on board
		discs[mid - 1, mid - 1] = new Disc(1, Color.White);
		discs[mid - 1,mid] = new Disc(2, Color.Black);
		discs[mid,mid - 1] = new Disc(3, Color.Black);
		discs[mid,mid] = new Disc(4, Color.White);
	}
	public void GetBoard()
	{
        InitializeBoard();
        
	}
	public void CheckPlayer(Position position, IDisc dics)
	{
		
	}
	public IDisc GetDisc(Position position)
	{
		
	}
	public bool IsFull()
	{
		for (int x = 0; x < discs.GetLength(0); x++)
        {
            for(int y = 0; y < discs.GetLength(1); y++)
            {
				// if any disc is null 
				if(discs[x, y] == null)
				{
					return false;
				}
			}
		}
		return true;
	}
	public bool CheckWinner()
	{
		// check if not any possible move each player 
		// check if board is full
		int blackCount = 0;
		int whiteCount = 0;

		if(discs[x, y].color is Color.Black)
		{
			blackCount++;
		}
		else if(discs[x, y].color = Color.White)
		{
			whiteCount++;
		}
		if(blackCount > whiteCount)
		{
			return GameResult.BlackWins;
		}
		else if(whiteCount > blackCount)
		{
			return GameResult.WhiteWins;
		}
	}
	public IPlayer GetWinner(Dictionary<IPlayer, Disc> _playerColors, IPlayer player1, IPlayer player2)
	{
		
    }
}