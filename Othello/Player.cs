namespace Player;
public class IPlayer
{
    public int IdPlayer {get;}
    public string Username {get;}
}
public class Player{
    public int Id {get; private set;}
    public string Username {get; private set;}
    public Player(int idPlayer, string username) 
    {
        this.Id = idPlayer;
        this.Username = username;
    }
}