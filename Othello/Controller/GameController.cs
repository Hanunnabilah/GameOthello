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
	private int _MaxPlayer;
	private Board _board;
	private Dictionary<IPlayer, Disc> _playersColors = new(); 
	public List<IPlayer> players;
	private int _CurrentPlayerIndex;
	public Action<IPlayer, IDisc> OnPlace;
	public Action<IPlayer, IDisc> OnFlip;
	public Action<IPlayer, IDisc, Position> MovePlayer;

	public GameController(IPlayer player1, IPlayer player2, Board _board, IDisc disc1,IDisc disc2)
	{
	   this._board = _board;
	   _MaxPlayer = 2;
	   players = new List<IPlayer>{player1, player2};
	   _CurrentPlayerIndex = 0;
	   _playersColors[player1] = (Disc)disc1;
	   _playersColors[player2] = (Disc)disc2;
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
		
		if(players is _MaxPlayer) 
		{
			return true;
		}
		return false;
	}
	public bool NextTurn()
	{
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
	public void EndTurn()
	{
		
	}
	public Position MakeMove(IPlayer player, IDisc disc, Position position1, Position position2)
	{
		if(players = GetPlayers())	
	}
	public PossibleMove(IPlayer Player, IDisc disc, Position position1, Position position2)
	{
		
	}
	public bool CheckWin()
	{
		
	}
	public Player GetWinner()
	{
		
	}
	public bool PassTurn(IPlayer player)
	{
		
	}
	private void FlipDisc(IPlayer player, IDisc disc, Position position)
	{
		
	}
	private IEnumerable<Position> GetAllPosition (IPlayer player, IDisc disc)
	{
		
	}
}