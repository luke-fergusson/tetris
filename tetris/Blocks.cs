public class Blocks
{
    public Board board { get; set; }
    public int BottomRow;
    public int BottomColumn;
    public int RRow;
    public int RColumn;
    public char[,] PB;
    public int State;
    public int[] M1;// the coordinates
    public int[] M2;
    public int[] M3;
    public int[] M4;
    public char CurrentLetter;

    public virtual void Down()
    {

        SetToZero();
        M1[1] = M1[1] + 1;
        M2[1] = M2[1] + 1;
        M3[1] = M3[1] + 1;
        M4[1] = M4[1] + 1;
        SetToLetter();
        //sets each current position on the board to 0 moves all the coordinates down and updates there position

    }
    public virtual void StarPosition()
    {
        SetToLetter();// sets the coordinates of the blocks to its assigned letter
    }
    public Blocks()
    {
        board = new Board();
        PB = new char[10, 20];
        board.BlankBoard();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                PB[i, j] = '0';
            }
        }
        // constructor
        // insizalies board and loads the board in as an grid of zeros
    }
    public void Left()
    {
        SetToZero();
        M1[0] = M1[0] - 1;
        M2[0] = M2[0] - 1;
        M3[0] = M3[0] - 1;
        M4[0] = M4[0] - 1;// sets each current position on the board to 0 moves all the coordinates left and updates there position
        SetToLetter();
    }
    public void Right()
    {
        SetToZero();
        M1[0] = M1[0] + 1;
        M2[0] = M2[0] + 1;
        M3[0] = M3[0] + 1;
        M4[0] = M4[0] + 1;
        SetToLetter();
    }
    public virtual bool GroundCollision()
    {
        if (BottomRow == 19)// an inherited method as this needs to be different for each block as the bottom coordinates change with each rotation state and block
        {

            return true;
        }

        return false;
    }
    public virtual bool RWallCollision()// checks for Right wall collisions, stopping blocks going out of bounds in the array 
    {
        if (BottomColumn - 1 == 9)
        {
            return true;
        }
        PB = board.GetBoard();
        if (PB[BottomColumn, BottomRow - 1] != '0')
        {
            return true;
        }


        return false;
    }
    public virtual bool LWallCollision()// checks for left wall collisions, stoppin gblocks going out of bounds in the array
    {
        if (BottomColumn + 1 == 0)
        {
            return true;
        }
        PB = board.GetBoard();
        if (PB[BottomColumn, BottomRow + 1] != '0')
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// an inherited methods as changes for each block and rotation state
    /// checks ground collision is false as not to cause out of bounds error
    /// then checks each coordinate that is on the bottom of the block
    /// and checks if its not 0 as that would indicate one of the blocks
    /// </summary>
    /// <returns></returns>
    public virtual bool BlockCollision()
    {
        if (!GroundCollision())
        {
            if (PB[BottomColumn, BottomRow + 1] != '0')
            {
                return true;
            }
            if (PB[RColumn - 1, RRow + 1] != '0')
            {
                return true;
            }
            return false;
        }
        return false;
    }
    public virtual void RotateClockwise()// empty as each block does differently, but needs to be centralsed as an inherited method
    {

    }
    public virtual void SetToZero()
    {
        board.ChangeBoard(M1[0], M1[1], '0');
        board.ChangeBoard(M2[0], M2[1], '0');
        board.ChangeBoard(M3[0], M3[1], '0');
        board.ChangeBoard(M4[0], M4[1], '0');// replacing previous poistion with 0's 
    }
    public virtual void SetToLetter()
    {
        board.ChangeBoard(M1[0], M1[1], CurrentLetter);
        board.ChangeBoard(M2[0], M2[1], CurrentLetter);
        board.ChangeBoard(M3[0], M3[1], CurrentLetter);
        board.ChangeBoard(M4[0], M4[1], CurrentLetter);// call the board class and change board method to update the board
    }
    public char[,] ReturnCurrentBoard()
    {
        return board.GetBoard();
    }


    public void LineMoveDown(int line)
    {
        int count = 0;

        for (int i = 0; i < 10; i++)
        {
            board.ChangeBoard(i, line, '0');// gets the line where the complete row is and sets all them to zero

            count = line;
            while (count != 0)
            {

                if (count - 1 >= 0)// moves everything down one line as long as your not at the bottom
                {

                    board.ChangeBoard(i, count, board.GetCurrentPos(i, count - 1));
                }
                count--;

            }


        }

    }




}

