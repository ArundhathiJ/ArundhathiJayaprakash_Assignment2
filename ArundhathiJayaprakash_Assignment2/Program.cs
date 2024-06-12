public class Cell
{
    public string Occupant { get; set; }

    public Cell(string occupant = "-")
    {
        Occupant = occupant;
    }
}
public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Player
{
    public string Name { get; }
    public Position Position { get; set; }
    public int GemCount { get; set; }

    public Player(string name, Position position)
    {
        Name = name;
        Position = position;
        GemCount = 0;
    }

    public void Move(char  direction)
    {
        switch (direction)
        {
            case 'U':
                Position.X = Position.X-1;
                break;
            case 'D':
                Position.X = Position.X+1;
                break;
            case 'L':
                Position.Y = Position.Y-1;
                break;
            case 'R':
                Position.Y= Position.Y +1;
                break;
        }
    }
}

public class Board
{
    int Size = 6;
    public Cell[,] Grid { get; }

    public Board()
    {
        Grid = new Cell[6, 6];

        // put first element as p1
        Grid[0, 0] = new Cell("P1");

        // put last element as p2
        Grid[5, 5] = new Cell("P2");

        // remaining elements will be G and O
        Random random = new Random();
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (Grid[i, j] == null)
                {
                    if (random.Next(3) == 0)
                    {
                        Grid[i, j] = new Cell("O");
                    }
                    else
                    {
                        Grid[i, j] = new Cell("G");
                    }
                }
            }
        }
    }

    public void DisplayGame()
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Console.Write(Grid[i, j].Occupant + " ");
            }
            Console.WriteLine();
        }
    }
    //GAME logic for valid movements
    public bool IsValidMove(Player player, char direction)
    {
        int newPosX = player.Position.X;
        int newPosY = player.Position.Y;

        switch (direction)
        {
            case 'U':
                newPosX= newPosX-1;
                break;
            case 'D':
                newPosX= newPosX+1;
                break;
            case 'L':
                newPosY= newPosY-1;
                break;
            case 'R':
                newPosY= newPosY+1;
                break;
        }

        if (newPosX < 0 || newPosX >= Size || newPosY < 0 || newPosY >= Size)
            return false;

        if (Grid[newPosX, newPosY].Occupant == "O")
            return false;

        return true;
    }
    //If players collect gems, the gems count will increase by one
    public void CollectGem(Player player)
    {
        Position pos = player.Position;
        if (Grid[pos.X, pos.Y].Occupant == "G")
        {
            player.GemCount++;
            Grid[pos.X, pos.Y].Occupant = player.Name;
        }
    }

    public void UpdatePlayerPosition(Player player, Position newPosition)
    {
        Grid[player.Position.X, player.Position.Y].Occupant = "-";
        player.Position = newPosition;
        Grid[newPosition.X, newPosition.Y].Occupant = player.Name;
    }
}

public class Game
{
    public Board Board { get; }
    public Player Player1 { get; }
    public Player Player2 { get; }
    private Player currentPlayer;
    private int totalTurns;

    public Game()
    {
        Board = new Board();
        Player1 = new Player("P1", new Position(0, 0));
        Player2 = new Player("P2", new Position(Board.Grid.GetLength(0) - 1, Board.Grid.GetLength(1) - 1));
        currentPlayer = Player1;
        totalTurns = 0;
    }

    public void Start()
    {
        while (!IsGameOver())
        {
            Console.Clear();
            Board.Display();
            Console.WriteLine($"{Player1.Name} Gems: {Player1.GemCount}, {Player2.Name} Gems: {Player2.GemCount}");
            Console.WriteLine($"{currentPlayer.Name}'s turn. Moves left: {15 - totalTurns / 2}");

            char move;
            do
            {
                Console.Write($"{currentPlayer.Name}, enter move (U/D/L/R): ");
                move = Console.ReadLine().ToUpper()[0];
            } while (!Board.IsValidMove(currentPlayer, move));

            // Update player's position
            Position oldPosition = new Position(currentPlayer.Position.X, currentPlayer.Position.Y);
            currentPlayer.Move(move);
            Board.CollectGem(currentPlayer);
            Board.UpdatePlayerPosition(currentPlayer, currentPlayer.Position);
            Board.Grid[oldPosition.X, oldPosition.Y].Occupant = "-";

            totalTurns++;
            SwitchTurn();
        }

        AnnounceWinner();
    }

    private void SwitchTurn()
    {
        currentPlayer = (currentPlayer == Player1) ? Player2 : Player1;
    }

    private bool IsGameOver()
    {
        return totalTurns >= 30;
    }

    private void AnnounceWinner()
    {
        Console.Clear();
        Board.Display();
        Console.WriteLine("Game Over!");
        Console.WriteLine($"{Player1.Name} collected {Player1.GemCount} gems.");
        Console.WriteLine($"{Player2.Name} collected {Player2.GemCount} gems.");
        if (Player1.GemCount > Player2.GemCount)
        {
            Console.WriteLine($"{Player1.Name} wins!");
        }
        else if (Player2.GemCount > Player1.GemCount)
        {
            Console.WriteLine($"{Player2.Name} wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }
}




public class GameHunter
{
    public static void Main(string[] args)
    {
        Board board = new Board();
        board.DisplayGame();
    }
}
