using Discs;
using Players;
using PositionBoard;
using InterfaceDisc;
using ColorDiscs;
using BoardGame;
using InterfacePlayer;
using System.Security;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using System.Security.AccessControl;
// using System.Drawing;

namespace GameControl;

public class GameController
{
	private int _MaxPlayer;
	private Board _board;
	private Dictionary<IPlayer, Disc> _playerColors = new Dictionary<IPlayer, Disc>();
	public List<IPlayer> players;
	private int _CurrentPlayerIndex;
	public Action<IPlayer, IDisc> OnPlace;
	public Action<IPlayer, IDisc> OnFlip;
	public Action<IPlayer, IDisc, Position> MovePlayer;

	public GameController(IPlayer player1, IPlayer player2, Board _board)
	{
		this._board = _board;
		_MaxPlayer = 2;
		_CurrentPlayerIndex = 0;
		players = new List<IPlayer> { player1, player2 };
		_playerColors.Add(player1, new Disc(1, Color.Black));
		_playerColors.Add(player2, new Disc(2, Color.White));
	}
	public List<IPlayer> GetPlayers()
	{
		return players;
	}
	// FITRI - sudah memakai index player dalam list
	// public bool CheckPlayer(IPlayer player)
	// {
	// 	// check player in list player
	// 	if (!players.Contains(player))
	// 	{
	// 		return false;
	// 	}
	// 	return true;
	// }
	public IPlayer GetCurrentPlayer()
	{
    	return players[_CurrentPlayerIndex];
	}	
	public bool StartTurn()
	{
		return PossibleMove();
	}
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

	public void EndTurn()
	{
		Disc[,] getAllDisc = _board.GetAllDisc();

		for (int y = 0; y < getAllDisc.GetLength(1); y++)
		{
			for (int x = 0; x < getAllDisc.GetLength(0); x++)
			{
				if (getAllDisc[x, y].color == Color.Hint)
				{
					_board.SetDisc(x, y, Color.Empty);
				}
			}
		}
	}
	public void PassTurn()
	// Give permission for players to pass the turn 
	{
		IPlayer currentPlayer = GetCurrentPlayer();
		// get list of hint position for current player
		List<Position> possibleMoves = _board.GetHints(currentPlayer);

		if(possibleMoves.Count == 0)
		{
			NextTurn();
		}
	}
	// public Position MakeMove(IPlayer player, IDisc disc, Position position1, Position position2)
	// FITRI - method makemove hanya diganti parameter atau argument saja 
	public void MakeMove(Position positionMove)
	{
		IPlayer currentPlayer = players[_CurrentPlayerIndex];
		Color currentPlayerColor = _playerColors[currentPlayer].color;

		_board.SetDisc(positionMove.x, positionMove.y, currentPlayerColor);
	}
	
	// FITRI - possible move diganti argument atau parameternya 
	public bool PossibleMove()
	{
		bool PossibleMoveExist = false;
		IPlayer currentPlayer = players[_CurrentPlayerIndex];
		Color currentPlayerColor = _playerColors[currentPlayer].color;

		Disc[,] allDisc = _board.GetAllDisc();
		for (int y = 0; y < allDisc.GetLength(0); y++)
		{
			for (int x = 0; x < allDisc.GetLength(1); x++)
			{
				if (currentPlayerColor == allDisc[x, y].color)
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
	private bool CheckPossibleMove (Position position, Disc[,] allDisc, Color currentPlayerColor)
	{
		bool PossibleMoveExist = false;

		// default color lawan = black
		Color enemyColor = Color.Black;
		if (currentPlayerColor == Color.Black)
		{
			enemyColor = Color.White;
		}

		// diagonal kiri atas
		// if (allDisc[x-1, y-1].color == enemyColor && allDisc[x-2,y-2].color == Color.Empty) 
		// {
		// 	_board.SetDisc(x-2, y-2, Color.Hint);
		// 	PossibleMoveExist = true;
		// }
		// int size = allDisc.GetLength(0);

		int i = 1;
		bool isEnemyFound = false;
		while (position.x - i >= 0 && position.y - i >= 0)
		{
			if (allDisc[position.x - i, position.y - i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x - i, position.y - i].color == Color.Empty)
			{
				_board.SetDisc(position.x - i, position.y - i, Color.Hint);
				PossibleMoveExist = true;
				break;
			}
			else
			{
				break;
			}
		}

		// diagonal kanan atas
		// if (allDisc[x + 1, y - 1].color == enemyColor && allDisc[x + 2, y - 2].color == Color.Empty)
		// {
		// 	_board.SetDisc(x + 2, y - 2, Color.Hint);
		// 	PossibleMoveExist = true;
		// }
		i = 1;
		isEnemyFound = false;
		int boardLength = allDisc.GetLength(0);
		while (position.x + i <= boardLength - 1 && position.y - i >= 0)
		{
			if (allDisc[position.x + i, position.y - i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x + i, position.y - i].color == Color.Empty)
			{
				_board.SetDisc(position.x + i, position.y - i, Color.Hint);
				PossibleMoveExist = true;
				break;
			}
			else
			{
				break;
			}
		}
		// diagonal kiri bawah
		// if (allDisc[x - 1, y + 1].color == enemyColor && allDisc[x - 2, y + 2].color == Color.Empty)
		// {
		// 	_board.SetDisc(x - 2, y + 2, Color.Hint);
		// 	PossibleMoveExist = true;
		// }
		i = 1;
		isEnemyFound = false;
		while (position.x - i >= 0 && position.y + i <= boardLength - 1)
		{
			if (allDisc[position.x - i, position.y + i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x - i, position.y + i].color == Color.Empty)
			{
				_board.SetDisc(position.x - i, position.y + i, Color.Hint);
				PossibleMoveExist = true;
				break;
			}
			else
			{
				break;
			}
		}
		// diagonal kanan bawah
		// if (allDisc[x + 1, y + 1].color == enemyColor && allDisc[x + 2, y + 2].color == Color.Empty)
		// {
		// 	_board.SetDisc(x + 2, y + 2, Color.Hint);
		// 	PossibleMoveExist = true;
		// }
		i = 1;
		isEnemyFound = false;
		while (position.x + i <= boardLength - 1 && position.y + i <= boardLength - 1)
		{
			if (allDisc[position.x + i, position.y + i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x + i, position.y + i].color == Color.Empty)
			{
				_board.SetDisc(position.x + i, position.y + i, Color.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		// bawah
		// if (allDisc[x, y + 1].color == enemyColor && allDisc[x, y + 2].color == Color.Empty)
		// {
		// 	_board.SetDisc(x, y + 2, Color.Hint);
		// 	PossibleMoveExist = true;
		// }
		i = 1;
		isEnemyFound = false;
		while (position.y + i <= boardLength - 1)
		{
			if (allDisc[position.x, position.y + i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x, position.y + i].color == Color.Empty)
			{
				_board.SetDisc(position.x, position.y + i, Color.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		// atas
		// if (allDisc[x, y - 1].color == enemyColor && allDisc[x, y - 2].color == Color.Empty)
		// {
		// 	_board.SetDisc(x, y - 2, Color.Hint);
		// 	PossibleMoveExist = true;
		// }
		i = 1;
		isEnemyFound = false;
		while (position.y - i >= 0)
		{
			if (allDisc[position.x, position.y - i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x, position.y - i].color == Color.Empty)
			{
				_board.SetDisc(position.x, position.y - i, Color.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		// samping kanan
		// if (allDisc[x + 1, y].color == enemyColor && allDisc[x + 2, y].color == Color.Empty)
		// {
		// 	_board.SetDisc(x + 2, y, Color.Hint);
		// 	PossibleMoveExist = true;
		// }
		i = 1;
		isEnemyFound = false;
		while (position.x + i <= boardLength - 1)
		{
			if (allDisc[position.x + i, position.y].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x + i, position.y].color == Color.Empty)
			{
				_board.SetDisc(position.x + i, position.y, Color.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		// samping kiri
		// if (allDisc[x - 1, y].color == enemyColor && allDisc[x - 2, y].color == Color.Empty)
		// {
		// 	_board.SetDisc(x - 2, y, Color.Hint);
		// 	PossibleMoveExist = true;
		// }
		i = 1;
		isEnemyFound = false;
		while (position.x - i >= 0)
		{
			if (allDisc[position.x - i, position.y].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[position.x - i, position.y].color == Color.Empty)
			{
				_board.SetDisc(position.x - i, position.y, Color.Hint);
				break;
			}
			else
			{
				break;
			}
		}
		return PossibleMoveExist;
	}
	public bool CheckWin(Dictionary<IPlayer, Disc> playerColors)
	{
		if(_board.CheckWinner(playerColors))
		{
			return true;
		}
		return false;
	}

	// FITRI - Getwinner in gamecontroll 
	public IPlayer GetWinner(Dictionary<IPlayer, Disc> playerColors)
	{
		return _board.GetWinner(playerColors);
	}
	// public bool IsGameOver(Board board, IPlayer currentPlayer)
	// {
	// 	if(board.IsFull())
	// 	{
	// 		return true;
	// 	}
	// 	List<Position> possibleMoves = board.GetHints(currentPlayer);
	// 	if(possibleMoves.Count == 0)
	// 	{
	// 		player
	// 	}
	// }
	// public void EndGame()
	// {
	// 	if(IsGameOver())
	// 	{

	// 	}
	// }
	// private void FlipDisc(IPlayer player, IDisc disc, Position position)
	// FITRI - parameternya ganti
	public void FlipDisc(Position positionFlip)
	{

		IPlayer currentPlayer = players[_CurrentPlayerIndex];
		Color currentPlayerColor = _playerColors[currentPlayer].color;

		Color enemyColor = Color.Black;
		if (currentPlayerColor == Color.Black)
		{
			enemyColor = Color.White;
		}
		Disc[,] allDisc = _board.GetAllDisc();

		// bawah
		// if (allDisc[x, y + 1].color == enemyColor && allDisc[x, y + 2].color == currentPlayerColor)
		// {
		// 	_board.SetDisc(x, y + 1, currentPlayerColor);
		// }
		int i = 1;
		bool isEnemyFound = false;
		int boardLength = allDisc.GetLength(0);
		while (positionFlip.y + i <= boardLength - 1)
		{
			if (allDisc[positionFlip.x, positionFlip.y + i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x, positionFlip.y + i].color == currentPlayerColor)
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
		// if (allDisc[x, y - 1].color == enemyColor && allDisc[x, y - 2].color == currentPlayerColor)
		// {
		// 	_board.SetDisc(x, y - 1, currentPlayerColor);
		// }
		i = 1;
		while (positionFlip.y - i >= 0)
		{
			if (allDisc[positionFlip.x, positionFlip.y - i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x, positionFlip.y - i].color == currentPlayerColor)
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
		// if (allDisc[x + 1, y].color == enemyColor && allDisc[x + 2, y].color == currentPlayerColor)
		// {
		// 	_board.SetDisc(x + 1, y, currentPlayerColor);
		// }
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x + i <= boardLength - 1)
		{
			if (allDisc[positionFlip.x + i, positionFlip.y].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x + i, positionFlip.y].color == currentPlayerColor)
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
		// if (allDisc[x - 1, y].color == enemyColor && allDisc[x - 2, y].color == currentPlayerColor)
		// {
		// 	_board.SetDisc(x - 1, y, currentPlayerColor);
		// }
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x - i >= 0)
		{
			if (allDisc[positionFlip.x - i, positionFlip.y].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x - i, positionFlip.y].color == currentPlayerColor)
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
		// if (allDisc[x + 1, y - 1].color == enemyColor && allDisc[x + 2, y - 2].color == currentPlayerColor)
		// {
		// 	_board.SetDisc(x + 1, y - 1, currentPlayerColor);
		// }
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x + i <= boardLength - 1 && positionFlip.y - i >= 0)
		{
			if (allDisc[positionFlip.x + i, positionFlip.y - i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x + i, positionFlip.y - i].color == currentPlayerColor)
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
		// if (allDisc[x - 1, y - 1].color == enemyColor && allDisc[x - 2, y - 2].color == currentPlayerColor)
		// {
		// 	_board.SetDisc(x - 1, y - 1, currentPlayerColor);
		// }
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x - i >= 0 && positionFlip.y - i >= 0)
		{
			if (allDisc[positionFlip.x - i, positionFlip.y - i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x - i, positionFlip.y - i].color == currentPlayerColor)
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
		// if (allDisc[x + 1, y + 1].color == enemyColor && allDisc[x + 2, y + 2].color == currentPlayerColor)
		// {
		// 	_board.SetDisc(x + 1, y + 1, currentPlayerColor);
		// }
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x + i <= boardLength - 1 && positionFlip.y + i <= boardLength - 1)
		{
			if (allDisc[positionFlip.x + i, positionFlip.y + i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x + i, positionFlip.y + i].color == currentPlayerColor)
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
		// if (allDisc[x - 1, y + 1].color == enemyColor && allDisc[x - 2, y + 2].color == currentPlayerColor)
		// {
		// 	_board.SetDisc(x - 1, y + 1, currentPlayerColor);
		// }
		i = 1;
		isEnemyFound = false;
		while (positionFlip.x - i >= 0 && positionFlip.y + i <= boardLength - 1)
		{
			if (allDisc[positionFlip.x - i, positionFlip.y + i].color == enemyColor)
			{
				isEnemyFound = true;
				i++;
			}
			else if (isEnemyFound && allDisc[positionFlip.x - i, positionFlip.y + i].color == currentPlayerColor)
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
    // FITRI - IEnumerable<position> digunakan untuk collection semua posisi
    // private IEnumerable<Position> GetAllPosition (IPlayer player, IDisc disc)
    // {

    // }
}