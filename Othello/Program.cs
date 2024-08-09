using GameControl;
using BoardGame;
using Discs;
using PieceDiscs;
using Players;
using PositionBoard;
using InterfacePlayer;
// using log4net;
// using log4net.Config;
using NLog;

class Program
{
	static void Main()
	{
		// ILog logger = LogManager.GetLogger(typeof(GameController));
		// XmlConfigurator.Configure(new FileInfo("Log4Net.config"));
		ILogger logger = LogManager.GetCurrentClassLogger();
		Board othelloBoard = new Board();
		othelloBoard.InitializeBoard();
		IPlayer player1 = new Player(1, "PLAYER 1");
		IPlayer player2 = new Player(2, "PLAYER 2");
		GameController controller = new GameController(player1, player2, othelloBoard,logger);
		
		while(!othelloBoard.IsFull())
		{
			controller.StartTurn();
			IPlayer currentPlayer = controller.GetCurrentPlayer();
			List<Position> positionHints = controller.GetHints(currentPlayer);
			bool isNoPossibleTurn = HandlePassTurn(positionHints,currentPlayer);

			if(isNoPossibleTurn)
			{
				controller.PassTurn();
				continue;
			}

			Disc[,] discs = othelloBoard.GetAllDisc();
			
			Console.WriteLine("    ");
			ShowBoard(discs);
			Console.WriteLine("    ");
			ShowCurrentPlayer(currentPlayer);
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
		Console.WriteLine("-------------- GAME OVER --------------");
		DisplayCountDisc(controller, player1, player2);
		GetWinner(controller,player1,player2);
	   
	}

	public static void ShowBoard(Disc[,] _discs)
	{
		for(int y = 0; y < _discs.GetLength(0); y++)
		{
			for(int x = 0; x < _discs.GetLength(1); x++)
			{
				// Console.Write(" " + x + y + "");
				if(_discs[x,y].piece == Piece.Empty)
				{
					Console.Write("   ");
				}
				else if (_discs[x,y].piece == Piece.Black)
				{
					Console.Write(" X ");
				}
				else if (_discs[x,y].piece == Piece.White)
				{
					Console.Write(" O ");
				}
				else if (_discs[x,y].piece == Piece.Hint)
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
		string hintPosition = "PLEASE ENTER THE FOLLOWING COORDINATE --> ";
		for(int hintIndex = 0; hintIndex < positionHints.Count; hintIndex++)
		{
			Position hint = positionHints[hintIndex];
			if(hintIndex == positionHints.Count - 1)
			{
				hintPosition = hintPosition + hint.x + "," + hint.y;
			} else {
				hintPosition = hintPosition + hint.x + "," + hint.y + " OR ";
			}
		}
		Console.WriteLine(hintPosition);
	}
	public static void ShowCurrentPlayer(IPlayer currentPlayer)
	{
		Console.WriteLine($"IT'S YOUT TURN NOW : {currentPlayer.Username}");
	}
	public static bool HandlePassTurn(List<Position> position, IPlayer currentPlayer)
	{
		if(position.Count == 0)
		{
			Console.WriteLine($"{currentPlayer.Username} HAS NO POSSIBLE MOVES AND IS PASSING THEIR TURN.");
			return true;
		}
		return false;
	}
	public static Position GetValidatedPosition(List<Position> positionHints) 
	{        
		bool isValidMove = false;
		while(true)
		{
			Console.WriteLine("SET DISC ON COORDINATE X:" );
			int x = int.Parse(Console.ReadLine());
			Console.WriteLine("SET DISC ON COORDINATE y:");
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
	public static Dictionary<IPlayer, Disc> InitializePlayerColors(IPlayer player1, IPlayer player2)
	{
		return new Dictionary<IPlayer, Disc>
		{
			{ player1, new Disc(1, Piece.Black) },
			{ player2, new Disc(2, Piece.White) }
		};
	}
	public static void GetWinner(GameController controller, IPlayer player1, IPlayer player2)
	{
		var playerColors = InitializePlayerColors(player1, player2);
		IPlayer winner = controller.GetWinner(playerColors);
		
		if(winner != null)
		{
			Console.WriteLine($"CONGRATULATION {winner.Username} IS THE WINNER");
		}
	}
	public static void DisplayCountDisc(GameController controller, IPlayer player1, IPlayer player2)
	{
		var playerColors = InitializePlayerColors(player1, player2);
		var discCounts = controller.CountDisc(playerColors);

		// Menampilkan hasil disk setelah permainan selesai
		Console.WriteLine("DISC COUNTS :");
		foreach (var count in discCounts)
		{
			Console.WriteLine($"{count.Key.Username} HAS {count.Value} DISCS.");
		}
	}
}