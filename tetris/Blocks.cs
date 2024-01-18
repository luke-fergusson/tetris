using System.Diagnostics;

public class Blocks
{
    public Board board { get; set; }
    public int BottomRow;
    public int BottomColumn;
    public int RRow;
    public int RColumn;
    public char[,] PB;
    public int State;
    public int[] M1;
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
    }
    public virtual void StarPosition()
    {
        SetToLetter();
    }
    public Blocks()
    {
        board = new Board();
        PB = new char[10, 20];
        board.blankBoard();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                PB[i, j] = '0';
            }
        }
    }
    public virtual void Left()
    {
        SetToZero();
        M1[0] = M1[0] - 1;
        M2[0] = M2[0] - 1;
        M3[0] = M3[0] - 1;
        M4[0] = M4[0] - 1;
        SetToLetter();
    }
    public virtual void Right()
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
        if (BottomRow == 19)
        {
            return true;
        }
        return false;
    }
    public virtual bool RWallCollision()
    {
        if (BottomColumn-1 == 9)
        {
            return true;
        }
        PB = board.GetBoard();
        if (PB[BottomColumn, BottomRow-1] != '0')
        {
            return true;
        }


        return false;
    }
    public virtual bool LWallCollision()
    {
        if (BottomColumn+1 == 0)
        {
            return true;
        }
        PB = board.GetBoard();
        if (PB[BottomColumn, BottomRow+1] != '0')
        {
            return true;
        }
        return false;
    }
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
    public virtual void RotateClockwise()
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
        board.ChangeBoard(M4[0], M4[1], CurrentLetter);
    }


    public void LineMoveDown(int line)
    {
        int count = 0;

        for (int i = 0; i < 10; i++)
        {
            board.ChangeBoard(i, line, '0');

            count = line;
            while (count != 0)
            {

                if (count - 1 >= 0)
                {
                    Debug.WriteLine("run");
                    Debug.WriteLine("this" + board.GetCurrentPos(i, count - 1));
                    board.ChangeBoard(i, count, board.GetCurrentPos(i, count - 1));
                }
                count--;
                Debug.WriteLine(count);
            }


        }

    }
    



}

