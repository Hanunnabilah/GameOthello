using PositionBoard;
using Discs;
using PieceDiscs;
using InterfacePlayer;

namespace BoardGame;

public class Board
{
	private Disc[,] _discs = new Disc[8,8];

	public void InitializeBoard()
	{
		int idDisc = 0;
		for(int y = 0; y < _discs.GetLength(1); y++)
		{
			for(int x = 0; x < _discs.GetLength(0); x++)
			{
				_discs[x,y] = new Disc(idDisc, Piece.Empty);
				idDisc++;
			}
		}
		// set position in center board
		int mid = _discs.GetLength(0) / 2;

		// set 4 discs for first place on board
		_discs[mid - 1, mid - 1].piece = Piece.White;
		_discs[mid - 1, mid].piece = Piece.Black;
		_discs[mid, mid - 1].piece = Piece.Black;
		_discs[mid, mid].piece = Piece.White;
	}

	public void SetDisc(int x, int y, Piece piece)
	// set disc sesuai dengan color pada argumen atau pparamter
	{
		_discs[x,y].piece = piece; 
	}
	public Disc[,] GetAllDisc()
	{
		return _discs;
	}
	public bool IsFull()
	{
		for(int y = 0; y < _discs.GetLength(0); y++)
		{
			for(int x = 0; x < _discs.GetLength(1); x++)
			{
				if(_discs[x,y].piece == Piece.Empty)
				{
					return false;
				}
			}
		}
		return true;
	}
	// Get the winner
	// pindah gamecontroller 
	public IPlayer GetWinner(Dictionary<IPlayer, Disc> _playerColors)
	{
		int blackCount = 0;
		int whiteCount = 0;

		foreach (Disc disc in _discs)
		{
			if (disc is not null)
			{
				if (disc.piece is Piece.Black)
				{
					blackCount++;
				}
				else if (disc.piece is Piece.White)
				{
					whiteCount++;
				}
			}

			foreach (var playerColor in _playerColors)
			{
				if (blackCount > whiteCount)
				{
					if (playerColor.Value.piece is Piece.Black)
					{
						return playerColor.Key;
					}
				}
				else if (blackCount < whiteCount)
				{
					if (playerColor.Value.piece is Piece.White)
					{
						return playerColor.Key;
					}
				}
			}
		}
		// If the number of disks is the same or no winner can be found
		return null;
	}		
}