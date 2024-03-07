public class Board
{
    public char[,] currentGameBoards;
    public char Row;
    public char Col;


    public Board()
    {
        currentGameBoards = new char[10, 20]; // contructor

    }

    public void BlankBoard()// sets all the values in the grid to 0
    {
        currentGameBoards = new char[10, 20];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                currentGameBoards[i, j] = '0';
            }
        }
    }

    public void ChangeBoard(int row, int col, char val)// used to update board
    {
        try
        {
            if (row <= 9 && col <= 19) // checks that changes don't go out of bounds of the array
            {
                currentGameBoards[row, col] = val;
            }
        }
        catch
        {

        }

    }

    public char[,] GetBoard()
    {

        return currentGameBoards;
    }

    public char GetCurrentPos(int col, int row)
    {
        return currentGameBoards[col, row];
    }

    public char[,] RestBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                currentGameBoards[i, j] = '0';
            }
        }
        return currentGameBoards;
    }


}
