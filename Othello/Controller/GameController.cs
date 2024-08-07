using Discs;
using PositionBoard;
using PieceDiscs;
using BoardGame;
using InterfacePlayer;

namespace GameControl;

public class GameController
{
	private Board _board;
	private Dictionary<IPlayer, Disc> _playerColors = new Dictionary<IPlayer, Disc>();
	private Dictionary<IPlayer, int> _countDiscPlayer;
	public List<IPlayer> players;
	private int _CurrentPlayerIndex;

	public GameController(IPlayer player1, IPlayer player2, Board _board)
	{
		this._board = _board;
		_CurrentPlayerIndex = 0;
		players = new List<IPlayer> { player1, player2 };
		_playerColors.Add(player1, new Disc(1, Piece.Black));
		_playerColors.Add(player2, new Disc(2, Piece.White));
		_countDiscPlayer = new Dictionary<IPlayer, int>
		{
			{ player1, 0 },
			{ player2, 0 }
		};
	}
	public IPlayer GetCurrentPlayer()
	{
		return players[_CurrentPlayerIndex];
	}
	public bool StartTurn()
	{
		return PossibleMove();
	}
	// ganti return type
	public void NextTurn()
	{
		// get current player by index in list players
		if (_CurrentPlayerIndex == 0)
		{
			_CurrentPlayerIndex = 1;
		}
		else
		{
			_CurrentPlayerIndex = 0;
		}
	}
	// ganti return type	
	public void EndTurn()
	{
		Disc[,] getAllDisc = _board.GetAllDisc();

		for (int y = 0; y < getAllDisc.GetLength(1); y++)
		{
			for (int x = 0; x < getAllDisc.GetLength(0); x++)
			{
				if (getAllDisc[x, y].piece == Piece.Hint)
				{
					_board.SetDisc(x, y, Piece.Empty);
				}
			}
		}
	}
	// ganti return type	
	public void PassTurn()
	// Give permission for players to pass the turn 
	{
		IPlayer currentPlayer = GetCurrentPlayer();
		// get list of hint position for current player
		List<Position> possibleMoves = GetHints(currentPlayer);

		if(possibleMoves.Count == 0)
		{
			NextTurn();
		}
	}
	// return type bool makeMove
	public void MakeMove(Position positionMove)
	{
		IPlayer currentPlayer = players[_CurrentPlayerIndex];
		Piece currentPlayerColor = _playerColors[currentPlayer].piece;

		_board.SetDisc(positionMove.x, positionMove.y, currentPlayerColor);
	}
	
	public bool PossibleMove()
	{
		bool PossibleMoveExist = false;
		IPlayer currentPlayer = players[_CurrentPlayerIndex];
		Piece currentPlayerColor = _playerColors[currentPlayer].piece;

		Disc[,] allDisc = _board.GetAllDisc();
		for (int y = 0; y < allDisc.GetLength(0); y++)
		{
			for (int x = 0; x < allDisc.GetLength(1); x++)
			{
				if (currentPlayerColor == allDisc[x, y].piece)
				// jika warna pemain saat ini adalah warna yang sama 
				{
					Position position = new Position(x, y);
					// maka cek disc sebelahnya
					PossibleMoveExist = CheckPossibleMove(position, allDisc, currentPlayerColor);
				}
			}
		}
		return PossibleMoveExist;
	}
	// refactor --> method baru
	private bool CheckPossibleMove (Position position, Disc[,] allDisc, Piece currentPlayerColor)
	{
		bool PossibleMoveExist = false;

		// default color lawan = black
		Piece enemyColor = Piece.Black;
		if (currentPlayerColor == Piece.Black)
		{
			enemyColor = Piece.White;
		}

		// diagonal kiri atas
		int i = 1;
		bool isEnemyFound = false;
		while (position.x - i >= 0 && position.y - i >= 0)
		{
			if (allDisc[position.x - i, position.y - i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x - i, position.y - i].piece == Piece.Empty)
			{
				_board.SetDisc(position.x - i, position.y - i, Piece.Hint);
				PossibleMoveExist = true;
				break;
			}
			else
			{
				break;
			}
		}

		// diagonal kanan atas
		i = 1;
		isEnemyFound = false;
		int boardLength = allDisc.GetLength(0);
		while (position.x + i <= boardLength - 1 && position.y - i >= 0)
		{
			if (allDisc[position.x + i, position.y - i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x + i, position.y - i].piece == Piece.Empty)
			{
				_board.SetDisc(position.x + i, position.y - i, Piece.Hint);
				PossibleMoveExist = true;
				break;
			}
			else
			{
				break;
			}
		}
		// diagonal kiri bawah
		i = 1;
		isEnemyFound = false;
		while (position.x - i >= 0 && position.y + i <= boardLength - 1)
		{
			if (allDisc[position.x - i, position.y + i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x - i, position.y + i].piece == Piece.Empty)
			{
				_board.SetDisc(position.x - i, position.y + i, Piece.Hint);
				PossibleMoveExist = true;
				break;
			}
			else
			{
				break;
			}
		}
		// diagonal kanan bawah
		i = 1;
		isEnemyFound = false;
		while (position.x + i <= boardLength - 1 && position.y + i <= boardLength - 1)
		{
			if (allDisc[position.x + i, position.y + i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x + i, position.y + i].piece == Piece.Empty)
			{
				_board.SetDisc(position.x + i, position.y + i, Piece.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		// bawah
		i = 1;
		isEnemyFound = false;
		while (position.y + i <= boardLength - 1)
		{
			if (allDisc[position.x, position.y + i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x, position.y + i].piece == Piece.Empty)
			{
				_board.SetDisc(position.x, position.y + i, Piece.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		// atas
		i = 1;
		isEnemyFound = false;
		while (position.y - i >= 0)
		{
			if (allDisc[position.x, position.y - i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x, position.y - i].piece == Piece.Empty)
			{
				_board.SetDisc(position.x, position.y - i, Piece.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		// samping kanan
		i = 1;
		isEnemyFound = false;
		while (position.x + i <= boardLength - 1)
		{
			if (allDisc[position.x + i, position.y].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x + i, position.y].piece == Piece.Empty)
			{
				_board.SetDisc(position.x + i, position.y, Piece.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		// samping kiri
		i = 1;
		isEnemyFound = false;
		while (position.x - i >= 0)
		{
			if (allDisc[position.x - i, position.y].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x - i, position.y].piece == Piece.Empty)
			{
				_board.SetDisc(position.x - i, position.y, Piece.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		return PossibleMoveExist;
	}
	public List<Position> GetHints(IPlayer player)
	{
		List<Position> hints = new List<Position>();
		Disc[,] allDisc = _board.GetAllDisc();

		for(int y = 0; y < allDisc.GetLength(0); y++)
		{
			for(int x = 0; x < allDisc.GetLength(1); x++)
			{
				if(allDisc[x,y].piece == Piece.Hint)
				{
					Position hint = new Position(x,y);
					hints.Add(hint);
				}
			}
		}
		return hints;	
	}
	// sebelumnya method ini berada di board
	// check if any winner
	public bool CheckWinner(Dictionary<IPlayer, Disc> playerColors)
	{
		IPlayer winner = GetWinner(playerColors);
		return winner != null;
	}
	// tidak memakai parameter
	public bool /*CheckWin*/ ValidateWinner(Dictionary<IPlayer, Disc> playerColors)
	{
		if(CheckWinner(playerColors))
		{
			return true;
		}
		return false;
	}
	// tidak memakai parameter
	public IPlayer GetWinner(Dictionary<IPlayer, Disc> playerColors)
	{
		return _board.GetWinner(playerColors);
	}
	public Dictionary<IPlayer, int> CountDisc(Dictionary<IPlayer, Disc> playerColors)
	{
		Disc[,] allDisc = _board.GetAllDisc();
		var countDiscPlayer = new Dictionary<IPlayer, int>();

		foreach (var playerColor in playerColors)
		{
			countDiscPlayer[playerColor.Key] = 0;
		}

		foreach (Disc disc in allDisc)
		{
			if (disc != null)
			{
				foreach (var playerColor in playerColors)
				{
					if (disc.piece == playerColor.Value.piece)
					{
						countDiscPlayer[playerColor.Key]++;
						break; // Jika disc sudah dihitung, tidak perlu memeriksa pemain lain
					}
				}
			}
		}
		return countDiscPlayer;
	}
	public void FlipDisc(Position positionFlip)
	{

		IPlayer currentPlayer = players[_CurrentPlayerIndex];
		Piece currentPlayerColor = _playerColors[currentPlayer].piece;

		Piece enemyColor = Piece.Black;
		if (currentPlayerColor == Piece.Black)
		{
			enemyColor = Piece.White;
		}
		Disc[,] allDisc = _board.GetAllDisc();

		// bawah
		int i = 1;
		bool isEnemyFound = false;
		int boardLength = allDisc.GetLength(0);
		while (positionFlip.y + i <= boardLength - 1)
		{
			if (allDisc[positionFlip.x, positionFlip.y + i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x, positionFlip.y + i].piece == currentPlayerColor)
			{
				for (int discEnemy = 1; discEnemy <= i - 1; discEnemy++)
				{
					_board.SetDisc(positionFlip.x, positionFlip.y + discEnemy, currentPlayerColor);
				}
				break;
			}
			else
			{
				break;
			}
		}
		// atas
		i = 1;
		while (positionFlip.y - i >= 0)
		{
			if (allDisc[positionFlip.x, positionFlip.y - i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x, positionFlip.y - i].piece == currentPlayerColor)
			{
				for(int discEnemy = 1; discEnemy <= i - 1; discEnemy++)
				{
					_board.SetDisc(positionFlip.x, positionFlip.y - discEnemy, currentPlayerColor);
				}
				break;
			}
			else
			{
				break;
			}
		}
		// samping kanan
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x + i <= boardLength - 1)
		{
			if (allDisc[positionFlip.x + i, positionFlip.y].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x + i, positionFlip.y].piece == currentPlayerColor)
			{
				for(int discEnemy = 1; discEnemy <= i - 1; discEnemy++)
				{
					_board.SetDisc(positionFlip.x + discEnemy, positionFlip.y, currentPlayerColor);
				}
				break;
			}
			else
			{
				break;
			}
		}
		// samping kiri
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x - i >= 0)
		{
			if (allDisc[positionFlip.x - i, positionFlip.y].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x - i, positionFlip.y].piece == currentPlayerColor)
			{
				for(int discEnemy = 1; discEnemy <= i - 1; discEnemy++)
				{
					_board.SetDisc(positionFlip.x - discEnemy, positionFlip.y, currentPlayerColor);
				}
				break;
			}
			else
			{
				break;
			}
		}
		// diagonal atas kanan
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x + i <= boardLength - 1 && positionFlip.y - i >= 0)
		{
			if (allDisc[positionFlip.x + i, positionFlip.y - i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x + i, positionFlip.y - i].piece == currentPlayerColor)
			{
				for(int discEnemy = 1; discEnemy <= i - 1; discEnemy++)
				{
					_board.SetDisc(positionFlip.x + discEnemy, positionFlip.y - discEnemy, currentPlayerColor);
				}
				break;
			}
			else
			{
				break;
			}
		}
		// diagonal atas kiri
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x - i >= 0 && positionFlip.y - i >= 0)
		{
			if (allDisc[positionFlip.x - i, positionFlip.y - i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x - i, positionFlip.y - i].piece == currentPlayerColor)
			{
				for(int discEnemy = 1; discEnemy <= i - 1; discEnemy++)
				{
					_board.SetDisc(positionFlip.x - discEnemy, positionFlip.y - discEnemy, currentPlayerColor);
				}
				break;
			}
			else
			{
				break;
			}
		}
		// diagonal kanan bawah
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x + i <= boardLength - 1 && positionFlip.y + i <= boardLength - 1)
		{
			if (allDisc[positionFlip.x + i, positionFlip.y + i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x + i, positionFlip.y + i].piece == currentPlayerColor)
			{
				for(int discEnemy = 1; discEnemy <= i - 1; discEnemy++)
				{
					_board.SetDisc(positionFlip.x + discEnemy, positionFlip.y + discEnemy, currentPlayerColor);					
				}
				break;
			}
			else
			{
				break;
			}
		}
		// diagonal kiri bawah
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x - i >= 0 && positionFlip.y + i <= boardLength - 1)
		{
			if (allDisc[positionFlip.x - i, positionFlip.y + i].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x - i, positionFlip.y + i].piece == currentPlayerColor)
			{
				
				for(int discEnemy = 1; discEnemy <= i - 1; discEnemy++)
				{
					_board.SetDisc(positionFlip.x - discEnemy, positionFlip.y + discEnemy, currentPlayerColor);				
				}
				break;
			}
			else
			{
				break;
			}
		}
	}
}