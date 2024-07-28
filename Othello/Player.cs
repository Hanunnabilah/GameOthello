namespace Player;
public class IPlayer
{
    internal int MaxPlayer;

    public int IdPlayer {get;}
    public string Username {get;}
}
public class Player{
    public int id {get; private set;}
    public string Username {get; private set;}
    public Player(int IdPlayer, string Username) 
    {
        this.id = IdPlayer;
        this.Username = Username;
    }
}