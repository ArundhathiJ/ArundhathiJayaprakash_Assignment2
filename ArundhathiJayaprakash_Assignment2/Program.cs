public class Cell
{
    public string Occupant { get; set; }

    public Cell(string occupant = "-")
    {
        Occupant = occupant;
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
}
public class GameHunter
{
    public static void Main(string[] args)
    {
        Board board = new Board();
        board.DisplayGame();
    }
}
