using GameControl;
using BoardGame;
using InterfaceDisc;
using Discs;
using ColorDiscs;
using Players;
using PositionBoard;
using System.Security.Cryptography.X509Certificates;

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
            Disc[,] discs = othelloBoard.GetAllDisc();
            List<Position> positionHints = othelloBoard.GetHints();
            ShowBoard(discs);
            PrintPossibleCoordinate(positionHints);
            Console.WriteLine("Letakkan disc x:" );
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Letakkan disc y:");
            int y = int.Parse(Console.ReadLine());
            // need validation for user input possible move
            Position positionMove = new Position(x,y);
            Position positionFlip = new Position(x,y);
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
                Console.WriteLine("---+---+---+---+---+---+---+---");
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

}