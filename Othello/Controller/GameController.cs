using Discs;
using Player;
using PositionBoard;
using InterfaceDisc;
using ColorDiscs;
using BoardGame;
using InterfacePlayer;
using System.Security;

namespace GameController;

public class GameController
{
	private int _MaxPlayer = 2;
	private Board _board;
	private Dictionary<IPlayer, Disc> _playerColors; 
	public List<IPlayer> players;
	private int _CurrentPlayerIndex = 0;
	public Action<IPlayer, IDisc> OnPlace;
	public Action<IPlayer, IDisc> OnFlip;
	public Action<IPlayer, IDisc, Position> MovePlayer;

	public GameController(IPlayer player1, IPlayer player2, Board _board, IDisc disc1,IDisc disc2)
	{
	   this._board = _board;
	//    this._playerColors = new Dictionary<IPlayer, Disc>{player1, player2, disc1, disc2};	
	   players = new List<IPlayer>{player1, player2};
	   _playerColors[player1] = (Disc)disc1;
	   _playerColors[player2] = (Disc)disc2;
	}
	public List<IPlayer> GetPlayers()
	{
		return players;
	}
	public bool CheckPlayer(IPlayer player)
	{
		// check player in list player
		if(!players.Contains(player))
		{
			return false;
		}
		return true;	
	}
	public bool StartTurn()
	{
		if() 
		{
			return true;
		}
		return false;
	}
	public bool NextTurn()
	{
		// calculate current player by index in list players
		// player index[0] = 0 + 1 % 2 = 1 % 2 = 1
		// player index[1] = 1 + 1 % 2 = 2 & 2 = 0
		int nextPlayerIndex = (_CurrentPlayerIndex + 1) % players.Count;
		IPlayer nextPlayer = players[nextPlayerIndex];

		if(PossibleMove(nextPlayer))
		{
			_CurrentPlayerIndex = nextPlayerIndex;
			return true;
		}
		else if (PossibleMove(players[_CurrentPlayerIndex]))
		{
			return true;
		}
		return false;
	}
	public void EndTurn(IPlayer player) 
	// end turn current player
	{
		foreach(var _CurrentPlayerIndex in players)
		{
			// check win condition before endturn player
			if(CheckWin())
			{
				GetWinner();
			}
			else if(MakeMove(players[_CurrentPlayerIndex]))
			{
				NextTurn();
			}
		}
	}
	public Position MakeMove(IPlayer player, IDisc disc, Position position1, Position position2)
	{
		// untuk setiap player didalam list player
		// jika player punya kemungkinan berpindah posisi maka make move
		// perpindah dari position1 ke position2
		foreach(var IPlayer in players)
		{
			if(PossibleMove())
			{
				
			}
		}
	}
	public PossibleMove(IPlayer player, IDisc disc, Position position1, Position position2)
	{
		
	}
	public bool CheckWin()
	{
		if(_board.CheckWinner())
		{
			return true;
		}
		return false;
	}
	public IPlayer GetWinner()
	{
		return _board.GetWinner();
	}
	public bool PassTurn(IPlayer player)
	// Give permission for players to pass the turn 
	{
		foreach(player is players[_CurrentPlayerIndex])
		{
			if(_CurrentPlayerIndex)
		}
		if(player != players[_CurrentPlayerIndex])
		// check apakah player sekarang adalah gilirannya
		{
			return false;
		}
	}
	private void FlipDisc(IPlayer player, IDisc disc, Position position)
	{
		
	}
	private IEnumerable<Position> GetAllPosition (IPlayer player, IDisc disc)
	{
		
	}
	private List<Position> Outflanked(IPlayer player, IDisc disc, Position position)
	{
		
	}
}