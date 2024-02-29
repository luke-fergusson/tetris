public class O_Block : Blocks
{


    /* 
     *          M1  M2
     *          
     *          M3  M4
     *           O block
     */


    public O_Block()
    {
        M1 = new int[] { 4, 1 };
        M2 = new int[] { 5, 1 };
        M3 = new int[] { 4, 2 };
        M4 = new int[] { 5, 2 };
        CurrentLetter = 'o';
    }

    public override bool GroundCollision()
    {
        BottomRow = M4[1];
        BottomColumn = M4[0];
        PB = board.currentGameBoards;

        return base.GroundCollision();
    }
    public override bool RWallCollision()
    {
        BottomRow = M4[1] + 1;
        BottomColumn = M4[0] + 1;
        PB = board.currentGameBoards;
        return base.RWallCollision();
    }
    public override bool LWallCollision()
    {
        BottomRow = M3[1] - 1;
        BottomColumn = M3[0] - 1;
        PB = board.currentGameBoards;
        return base.LWallCollision();
    }
    public override bool BlockCollision()
    {
        BottomRow = M3[1];
        BottomColumn = M3[0];
        RRow = M4[1];
        RColumn = M4[0];
        PB = board.GetBoard();
        return base.BlockCollision();
    }

}

