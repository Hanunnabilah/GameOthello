using PositionBoard;
using Discs;
// using Players;
using ColorDiscs;
using InterfaceDisc;
using InterfacePlayer;
using System.Security.Authentication;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
// using System.Security.Cryptography.X509Certificates;
// using System.Runtime.CompilerServices;

namespace BoardGame;

public class Board
{
	private Disc[,] _discs = new Disc[8,8];

    public void InitializeBoard()
	{
		int idDisc = 0;
		for(int y = 0; y < _discs.GetLength(0); y++)
		{
			for(int x = 0; x < _discs.GetLength(1); x++)
			{
				_discs[x,y] = new Disc(idDisc, Color.Empty);
				idDisc++;
			}
		}
		// set position in center board
		int mid = _discs.GetLength(0) / 2;

		// set 4 discs for first place on board
		_discs[mid - 1, mid - 1].color = Color.White;
		_discs[mid - 1, mid].color = Color.Black;
		_discs[mid, mid - 1].color = Color.Black;
		_discs[mid, mid].color = Color.White;
	}

	public void SetDisc(int x, int y, Color color)
	// set disc sesuai dengan color pada argumen atau pparamter
	{
		_discs[x,y].color = color; 
	}
	// TODO - fungsi getBoard?
	public void GetBoard()
	{		
		
	}
	// FITRI - check apa?
	// public void CheckPlayer(Position position, IDisc dics)
	// {
	// 	// check disc on board
	// 	if(position.x < 0 || position.x >= _discs.GetLength(0) || position.y < 0 || position.y >= _discs.GetLength(1))
	// 	{
	// 		return;	
	// 	}
	// 	// position is filled?
	// 	if(_discs is not null)
	// 	{
	// 		return;
	// 	}
	// }

	// FITRI - ganti setdics
	// public IDisc GetDisc(Position position)
	// {
	// 	// return discs based on position
	// 	return _discs[position.x, position.y];
	// }
	public Disc[,] GetAllDisc()
	{
		return _discs;
	}
	public bool IsFull()
	{
		for(int y = 0; y < _discs.GetLength(1); y++)
		{
			for(int x = 0; x < _discs.GetLength(0); x++)
			{
				if(_discs[x,y].color == Color.Empty)
				{
					return false;
				}
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
	// FITRI - parameter player 1 dan 2 sudah diset di dlm _playerColors
	public IPlayer GetWinner (Dictionary<IPlayer, Disc> _playerColors)
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
	public List<Position> GetHints()
	{
		List<Position> hints = new List<Position>();
		for(int y = 0; y < _discs.GetLength(1); y++)
		{
			for(int x = 0; x < _discs.GetLength(0); x++)
			{
				if(_discs[x,y].color == Color.Hint)
				{
					Position hint = new Position(x,y);
					hints.Add(hint);
				}
			}
		}
		return hints;	
	}
}