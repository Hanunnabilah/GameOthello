using InterfacePlayer;

namespace Player;

public class Player: IPlayer
{
	public int IdPlayer {get; private set;}
	public string Username {get; private set;}
	public Player(int idPlayer, string username) 
	{
		this.IdPlayer = idPlayer;
		this.Username = username;
	}
}