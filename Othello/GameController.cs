namespace GameController;
using Board;
using Discs;
using Player;
using PositionBoard;
using ColorDisc;

public class GameController
{
    private int _MaxPlayer;
    private Board _board;
    private Dictionary<IPlayer, Disc> _playersColors = new(); 
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
    public List<IPlayer> GetPlayers(); 
}