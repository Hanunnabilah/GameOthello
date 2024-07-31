using PositionBoard;
using Discs;
using Player;
using ColorDiscs;
namespace Board;

public class Board
{
	private Disc[,] discs = new Disc[8,8];
	public void InitializeBoard()
	{
		for (int x = 0; x <= 8; x++)
		{
			for (int y = 0; y <= 8; y++)
			{
				// first place 4 discs
				discs[3,3] = new Disc(1, Color.White);
				discs[3,4] = new Disc(2, Color.Black);
				discs[4,3] = new Disc(3, Color.Black);
				discs[4,4] = new Disc(4, Color.White);
			}
		}
	}
	public void GetBoard()
	{
		for (int x = 0; x <= 8; x++)
		{
			for (int y = 0; y <= 8; y++)
			{
				
			}
		}
	}
	public void CheckPlayer(Position position, IDisc dics)
	{
		for(position is )
	}
	public IDisc GetDisc(Position position)
	{

	}
	public bool IsFull()
	{
		if(discs is Disc[,])
		{
			return true;
		}
		return false;
	}
	public bool CheckWinner()
	{
		// check if not any possible move each player 
		// check if board is full
	}
	public IPlayer GetWinner(Dictionary<IPlayer, Disc> _playerColors, IPlayer player1, IPlayer player2)
	{

	}
}