using Discs;
using PositionBoard;
using PieceDiscs;
using BoardGame;
using InterfacePlayer;
using NLog;
// using log4net;
// using log4net.Config;

namespace GameControl;

public class GameController
{
	private Board _board;
	private Dictionary<IPlayer, Disc> _playerColors = new Dictionary<IPlayer, Disc>();
	public List<IPlayer> players;
	private int _CurrentPlayerIndex;
	// private ILog _log;
	private ILogger _log;

	public GameController(IPlayer player1, IPlayer player2, Board _board, ILogger log)
	{
		_log = log;
		this._board = _board;
		_CurrentPlayerIndex = 0;
		players = new List<IPlayer> { player1, player2 };
		_playerColors.Add(player1, new Disc(1, Piece.Black));
		_playerColors.Add(player2, new Disc(2, Piece.White));
	}
	public IPlayer GetCurrentPlayer()
	{
		return players[_CurrentPlayerIndex];
	}
	public bool StartTurn()
	{
		return PossibleMove();
	}
	public bool NextTurn()
	{
		// Get current player by index in list players
		if (_CurrentPlayerIndex == 0)
		{
			_CurrentPlayerIndex = 1;
		}
		else
		{
			_CurrentPlayerIndex = 0;
		}
		return true;
	}
	public bool EndTurn()
	{
		Disc[,] getAllDisc = _board.GetAllDisc();
		bool hintsCleared = false;

		for (int y = 0; y < getAllDisc.GetLength(1); y++)
		{
			for (int x = 0; x < getAllDisc.GetLength(0); x++)
			{
				if (getAllDisc[x, y].piece == Piece.Hint)
				{
					_board.SetDisc(x, y, Piece.Empty);
					hintsCleared = true;
				}
			}
		}
		return hintsCleared;
	}
	public bool PassTurn()
	// Give permission for players to pass the turn 
	{
		IPlayer currentPlayer = GetCurrentPlayer();
		// Get list of hint position for current player
		List<Position> possibleMoves = GetHints(currentPlayer);

		if (possibleMoves.Count == 0)
		{
			NextTurn();
		}
		return possibleMoves.Any();
	}
	public bool MakeMove(Position positionMove)
	{
		IPlayer currentPlayer = players[_CurrentPlayerIndex];
		Piece currentPlayerColor = _playerColors[currentPlayer].piece;

		if (IsValidMove(positionMove, currentPlayerColor))
		{
			_board.SetDisc(positionMove.x, positionMove.y, currentPlayerColor);
			FlipDisc(positionMove);
			// _log.Info($"Move executed at position ({positionMove.x}, {positionMove.y}) by player with color {currentPlayerColor}");
			return true; 
		}
		else
		{
			// _log.Warn($"Move failed at position ({positionMove.x}, {positionMove.y}) by player with color {currentPlayerColor}");
			return false;
		}
		}
	private bool IsValidMove(Position position, Piece color)
	{
		return CheckPossibleMove(position, _board.GetAllDisc(), color);
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
				// If the current player's color is the same color
				{
					Position position = new Position(x, y);
					// Check the disc next to it
					PossibleMoveExist = CheckPossibleMove(position, allDisc, currentPlayerColor);
				}
			}
		}
		return PossibleMoveExist;
	}
	private bool CheckPossibleMove(Position position, Disc[,] allDisc, Piece currentPlayerColor)
	{
		bool possibleMoveExist = false;

		Piece enemyColor = currentPlayerColor == Piece.Black ? Piece.White : Piece.Black;
		int boardLength = allDisc.GetLength(0);

		// Define the directions to check: (x, y) pairs
		var directions = new (int x, int y)[]
		{
			(-1, -1), // Diagonal top-left
			(1, -1),  // Diagonal top-right
			(-1, 1),  // Diagonal bottom-left
			(1, 1),   // Diagonal bottom-right
			(0, 1),   // Down
			(0, -1),  // Up
			(1, 0),   // Right
			(-1, 0)   // Left
		};

		foreach (var (dx, dy) in directions)
		{
			if (CheckDirection(position, allDisc, enemyColor, dx, dy))
			{
				possibleMoveExist = true;
			}
		}
		return possibleMoveExist;
	}
	private bool CheckDirection(Position position, Disc[,] allDisc, Piece enemyColor, int dx, int dy)
	{
		int x = position.x + dx;
		int y = position.y + dy;
		bool isEnemyFound = false;
		int boardLength = allDisc.GetLength(0);

		while (x >= 0 && x < boardLength && y >= 0 && y < boardLength)
		{
			if (allDisc[x, y].piece == enemyColor)
			{
				isEnemyFound = true;
			}
			else if (isEnemyFound && allDisc[x, y].piece == Piece.Empty)
			{
				_board.SetDisc(x, y, Piece.Hint);
				return true;
			}
			else
			{
				break;
			}
			x += dx;
			y += dy;
		}
		return false;
	}
	public List<Position> GetHints(IPlayer player)
	{
		List<Position> hints = new List<Position>();
		Disc[,] allDisc = _board.GetAllDisc();

		for (int y = 0; y < allDisc.GetLength(0); y++)
		{
			for (int x = 0; x < allDisc.GetLength(1); x++)
			{
				if (allDisc[x, y].piece == Piece.Hint)
				{
					Position hint = new Position(x, y);
					hints.Add(hint);
				}
			}
		}
		return hints;
	}
	// REVISI - tidak memakai parameter
	public bool CheckWinner(Dictionary<IPlayer, Disc> playerColors)
	{
		IPlayer winner = GetWinner(playerColors);
		return winner != null;
	}
	// REVISI - tidak memakai parameter
	public IPlayer GetWinner(Dictionary<IPlayer, Disc> playerColors)
	{
		// Count discs for each color
		int blackCount = CountDisc(Piece.Black);
		int whiteCount = CountDisc(Piece.White);

		foreach (var player in _playerColors)
		{
			if (blackCount > whiteCount && player.Value.piece == Piece.Black)
			{
				return player.Key;
			}
			else if (whiteCount > blackCount && player.Value.piece == Piece.White)
			{
				return player.Key;
			}
		}
		// If the number of discs is the same or no winner can be found
		return null;
	}
	public int CountDisc(Piece pieceColor)
	{
		int count = 0;
		Disc[,] allDisc = _board.GetAllDisc();

		foreach (Disc disc in allDisc)
		{
			if (disc != null && disc.piece == pieceColor)
			{
				count++;
			}
		}
		return count;
	}
	public void FlipDisc(Position positionFlip)
	{
		IPlayer currentPlayer = players[_CurrentPlayerIndex];
		Piece currentPlayerColor = _playerColors[currentPlayer].piece;
		Piece enemyColor = currentPlayerColor == Piece.Black ? Piece.White : Piece.Black;

		Disc[,] allDisc = _board.GetAllDisc();
		int boardLength = allDisc.GetLength(0);

		var directions = new (int x, int y)[]
		{
			(-1, -1), // Diagonal top-left
			(1, -1),  // Diagonal top-right
			(-1, 1),  // Diagonal bottom-left
			(1, 1),   // Diagonal bottom-right
			(0, 1),   // Down
			(0, -1),  // Up
			(1, 0),   // Right
			(-1, 0)   // Left
		};
		foreach (var (dx, dy) in directions)
		{
			FlipInDirection(positionFlip, dx, dy, currentPlayerColor, enemyColor, allDisc, boardLength);
		}
	}

	private void FlipInDirection(Position start, int xIncrement, int yIncrement, Piece currentPlayerColor, Piece enemyColor, Disc[,] allDisc, int boardLength)
	{
		int x = start.x + xIncrement;
		int y = start.y + yIncrement;
		int i = 1;
		bool isEnemyFound = false;

		while (x >= 0 && x < boardLength && y >= 0 && y < boardLength)
		{
			if (allDisc[x, y].piece == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[x, y].piece == currentPlayerColor)
			{
				for (int discEnemy = 1; discEnemy < i; discEnemy++)
				{
					_board.SetDisc(start.x + discEnemy * xIncrement, start.y + discEnemy * yIncrement, currentPlayerColor);
				}
				break;
			}
			else
			{
				break;
			}
			x += xIncrement;
			y += yIncrement;
		}
	}
}