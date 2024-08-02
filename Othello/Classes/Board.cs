using PositionBoard;
using Discs;
using Player;
using ColorDiscs;
using InterfaceDisc;
using InterfacePlayer;
// using System.Security.Cryptography.X509Certificates;
// using System.Runtime.CompilerServices;

namespace BoardGame;

public class Board
{
	private Disc[,] _discs = new Disc[8,8];

    public void InitializeBoard()
	{
		// set position in center board
		int mid = _discs.GetLength(0) / 2;

		// set 4 discs for first place on board
		_discs[mid - 1, mid - 1] = new Disc(1, Color.White);
		_discs[mid - 1,mid] = new Disc(2, Color.Black);
		_discs[mid,mid - 1] = new Disc(3, Color.Black);
		_discs[mid,mid] = new Disc(4, Color.White);
	}
	public void GetBoard()
	{
		_discs = new Disc[_discs.GetLength(0), _discs.GetLength(1)];
		
		for(int x = 0; x < _discs.GetLength(0); x++)
		{
			for(int y = 0; y < _discs.GetLength(1); y++)
			{
				_discs[x,y] = _discs[x,y];
			}
		}
	}
	public void CheckPlayer(Position position, IDisc dics)
	{
		// check disc on board
		if(position.x < 0 || position.x >= _discs.GetLength(0) || position.y < 0 || position.y >= _discs.GetLength(1))
		{
			return;	
		}
		// position is filled?
		if(_discs is not null)
		{
			return;
		}
	}
	public IDisc GetDisc(Position position)
	{
		// return discs based on position
		return _discs[position.x, position.y];
	}
	public bool IsFull()
	{
		foreach(Disc disc in _discs)
		{
			if(disc is null)
			{
				return false;
			} 		
		}
		return true;
	}
	public bool CheckWinner()
	{
		int blackCount = 0;
		int whiteCount = 0;
		
		foreach(Disc disc in _discs)
		{
			if(disc is not null)
			{
				if(disc.color is Color.Black)
				{
					blackCount++;
				}
				else if(disc.color is Color.White)
				{
					whiteCount++;
				}
			}
			if(blackCount > whiteCount)
			{
				return true;
			}
			else if(blackCount < whiteCount)
			{
				return true;
			}
			return false;
		}
		return false;
	}
	public IPlayer GetWinner (Dictionary<IPlayer, Disc> _playerColors, IPlayer player1, IPlayer player2)
	{
		int blackCount = 0;
		int whiteCount = 0;
		
		foreach(Disc disc in _discs)
		{
			if(disc is not null)
			{
				if(disc.color is Color.Black)
				{
					blackCount++;
				}
				else if(disc.color is Color.White)
				{
					whiteCount++;
				}
			}
			if(blackCount > whiteCount)
			{
				foreach(var playerColor in _playerColors)
				{
					if(playerColor.Value.color is Color.Black)
					{
						return playerColor.Key;
					}
				}
			}
			else if(blackCount < whiteCount)
			{
				foreach(var playerColor in _playerColors)
				{
					if(playerColor.Value.color is Color.White)
					{
						return playerColor.Key;
					}
				}
			}
		}
		return null;
	}
}