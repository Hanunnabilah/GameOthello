using GameControl;
using BoardGame;
using InterfaceDisc;
using Discs;
using ColorDiscs;
using Players;
using PositionBoard;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using InterfacePlayer;

class Program
{
    static void Main(string[] args)
    {
        Board othelloBoard = new Board();
        othelloBoard.InitializeBoard();
        Player player1 = new Player(1, "Player1");
        Player player2 = new Player(2, "Player2");
        GameController controller = new GameController(player1, player2, othelloBoard);
        
        while(!othelloBoard.IsFull())
        {
            controller.StartTurn();
            HandlePassTurn(controller, othelloBoard);
            IPlayer currentPlayer = controller.GetCurrentPlayer();
            Disc[,] discs = othelloBoard.GetAllDisc();
            List<Position> positionHints = othelloBoard.GetHints(currentPlayer);
            ShowCurrentPlayer(currentPlayer);
            ShowBoard(discs);
            PrintPossibleCoordinate(positionHints);

            // need validation for user input possible move
            Position inputPlayerCoord = GetValidatedPosition(positionHints);

            Position positionMove = new Position(inputPlayerCoord.x,inputPlayerCoord.y);
            Position positionFlip = new Position(inputPlayerCoord.x,inputPlayerCoord.y);
            controller.MakeMove(positionMove);
            controller.FlipDisc(positionFlip);
            controller.EndTurn();   
            controller.NextTurn();
        }
        

        // controller.StartTurn();
        // discs = othelloBoard.GetAllDisc();
        // ShowBoard(discs);
        // PrintPossibleCoordinate(discs);

        // Console.WriteLine("Letakkan x:");
        // x =  int.Parse(Console.ReadLine());
        // Console.WriteLine("Letakkan y:");
        // y = int.Parse(Console.ReadLine());

        // controller.MakeMove(x,y);
        // controller.FlipDisc(x,y);
        // controller.EndTurn();
        // controller.NextTurn();
        // controller.StartTurn();
        // discs = othelloBoard.GetAllDisc();
        // ShowBoard(discs);
       
    }

    public static void ShowBoard(Disc[,] _discs)
    {
        for(int y = 0; y < _discs.GetLength(0); y++)
        {
            for(int x = 0; x < _discs.GetLength(1); x++)
            {
                // Console.Write(" " + x + y + "");
                if(_discs[x,y].color == Color.Empty)
                {
                    Console.Write("   ");
                }
                else if (_discs[x,y].color == Color.Black)
                {
                    Console.Write(" X ");
                }
                else if (_discs[x,y].color == Color.White)
                {
                    Console.Write(" O ");
                }
                else if (_discs[x,y].color == Color.Hint)
                {
                    Console.Write(" H ");
                }

                if (x < _discs.GetLength(1) - 1){
                    Console.Write("|");
                }
            }
            Console.WriteLine();
            if (y < _discs.GetLength(0) - 1) 
            {
                Console.WriteLine("---+---+---+---+---+---+---+---");
            }
        }
    }

    public static void PrintPossibleCoordinate(List<Position> positionHints)
    {
        string hintPosition = "Silahkan Masukkan Koordinat brikut: ";
        for(int hintIndex = 0; hintIndex < positionHints.Count; hintIndex++)
        {
            Position hint = positionHints[hintIndex];
            if(hintIndex == positionHints.Count - 1)
            {
                hintPosition = hintPosition + hint.x + "," + hint.y;
            } else {
                hintPosition = hintPosition + hint.x + "," + hint.y + " atau ";
            }
        }
        Console.WriteLine(hintPosition);
    }
    public static void ShowCurrentPlayer(IPlayer currentPlayer)
    {
        Console.WriteLine($"IT'S YOUT TURN NOW : {currentPlayer.Username}");
    }
    public static void HandlePassTurn(GameController controller, Board othelloBoard)
    {
        IPlayer currentPlayer = controller.GetCurrentPlayer();
        List<Position> possibleMoves = othelloBoard.GetHints(currentPlayer);

        if (possibleMoves.Count == 0)
        {
            Console.WriteLine($"{currentPlayer.Username} has no possible moves and is passing their turn.");
            controller.PassTurn();
            // Ensure that the next turn is started after passing
            controller.StartTurn();
        }
    }
    public static Position GetValidatedPosition(List<Position> positionHints) 
    {        
        bool isValidMove = false;
        while(true)
        {
            Console.WriteLine("Set disc on coordinate x:" );
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Set disc on coordinate y:");
            int y = int.Parse(Console.ReadLine());

            Position inputPlayerCoord = new Position(x, y);
            // bool isValidMove = positionHints.Any(hint => hint.x == inputPlayerCoord.x && hint.y == inputPlayerCoord.y);
            foreach(Position hint in positionHints)
            {
                if(inputPlayerCoord.x == hint.x && inputPlayerCoord.y == hint.y)
                {
                    isValidMove = true;
                    break;
                }
            }
            if(isValidMove)
            {
                return inputPlayerCoord;
            }
            else
            {
                Console.WriteLine("INVALID INPUT COORDINATE, PLEASE TRY AGAIN!");
            }
        }
    }

}

