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
	public List<IPlayer> players = new List<IPlayer>();
	private int _CurrenPlayerIndex;
	public Action<IPlayer, IDisc> OnPlace;
	public Action<IPlayer, IDisc> OnFlip;
	public Action<IPlayer, IDisc, Position> MovePlayer;

	public GameController(IPlayer player1, IPlayer player2, Board _board, IDisc disc1,IDisc disc2)
	{
	   this._board = _board;
	   _playersColors[player1] = (Disc)disc1;
	   _playersColors[player2] = (Disc)disc2;
	}
	public List<IPlayer> GetPlayers()
	{
		return new List<IPlayer>(players);
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
		int _MaxPlayer = 2;
		if(players  is _MaxPlayer) 
		{
			return true;
		}
		return false;
	}
	public bool NextTurn()
	{
		if(players is MakeMove())
		{
			return true;
		}
		return false;
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
	public void EndTurn()
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