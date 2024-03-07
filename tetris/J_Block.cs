namespace tetris
{
    public class J_Block : Blocks
    {

        /*
         *  M1
         *  
         *  M2  M3  M4
         * 
         * 
         */
        public J_Block()
        {
            State = 0;
            M1 = new int[] { 4, 1 }; // { x,y } 
            M2 = new int[] { 4, 2 };
            M3 = new int[] { 5, 2 };
            M4 = new int[] { 6, 2 };

            CurrentLetter = 'j';
        }




        public override bool GroundCollision()
        {
            if (State == 0 || State == 1 || State == 3)
            {
                BottomColumn = M4[0];
                BottomRow = M4[1];
            }
            if (State == 2)
            {
                BottomColumn = M1[0];
                BottomRow = M1[1];
            }

            return base.GroundCollision();
        }
        public override bool RWallCollision()
        {
            if (State == 0 || State == 2)
            {
                BottomColumn = M4[0] + 1;
                BottomRow = M4[1] + 1;
            }
            if (State == 1 || State == 3)
            {
                BottomColumn = M1[0] + 1;
                BottomRow = M1[1] + 1;
            }
            return base.RWallCollision();
        }
        public override bool LWallCollision()
        {

            if (State == 0 || State == 1)
            {
                BottomRow = M2[1] - 1;
                BottomColumn = M2[0] - 1;
            }
            if (State == 2 || State == 3)
            {
                BottomColumn = M1[0] - 1;
                BottomRow = M1[1] - 1;
            }

            return base.LWallCollision();
        }
        public override bool BlockCollision()
        {

            PB = board.GetBoard();

            if (!GroundCollision())
            {
                if (State == 0)
                {
                    try
                    {
                        if (PB[M2[0], M2[1] + 1] != '0' || PB[M3[0], M3[1] + 1] != '0' || PB[M4[0], M4[1] + 1] != '0')
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        SetToZero();
                        M1[0] = M1[0] - 1;
                        M2[0] = M2[0] - 1;
                        M3[0] = M3[0] - 1;
                        M4[0] = M4[0] - 1;
                        SetToLetter();
                    }
                }
                if (State == 1 || State == 3)
                {
                    try
                    {
                        if (PB[M1[0], M1[1] + 1] != '0' || PB[M4[0], M4[1] + 1] != '0')
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        SetToZero();
                        M1[0] = M1[0] - 1;
                        M2[0] = M2[0] - 1;
                        M3[0] = M3[0] - 1;
                        M4[0] = M4[0] - 1;
                        SetToLetter();
                    }
                }
                if (State == 2)
                {
                    try
                    { 
                        if (PB[M1[0], M1[1] + 1] != '0' || PB[M4[0], M4[1] + 1] != '0' || PB[M2[0], M2[1] + 1] != '0')
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        SetToZero();
                        M1[0] = M1[0] + 1;
                        M2[0] = M2[0] + 1;
                        M3[0] = M3[0] + 1;
                        M4[0] = M4[0] + 1;
                        SetToLetter();
                    }
                }

            }
            return false;
        }
        public override void RotateClockwise()
        {
            switch (State)
            {
                case 0:
                    SetToZero();


                    M1[0] = M1[0] + 2;
                    M2[0] = M2[0] + 1;
                    M4[0] = M4[0] - 1;

                    M1[1] = M1[1] - 1;
                    M2[1] = M2[1] - 2;
                    M3[1] = M3[1] - 1;



                    SetToLetter();
                    State = 1;
                    break;
                case 1:
                    SetToZero();

                    M1[0] = M1[0] - 2;
                    M3[0] = M3[0] + 1;
                    M4[0] = M4[0] + 1;

                    M1[1] = M1[1] + 1;
                    M2[1] = M2[1] + 1;

                    SetToLetter();
                    State = 2;
                    break;
                case 2:
                    SetToZero();

                    M3[0] = M3[0] - 1;
                    M4[0] = M4[0] - 1;

                    M1[1] = M1[1] + 1;
                    M2[1] = M2[1] - 1;


                    SetToLetter();

                    State = 3;
                    break;
                case 3:
                    SetToZero();

                    M2[0] = M2[0] - 1;
                    M4[0] = M4[0] + 1;

                    M1[1] = M1[1] - 1;
                    M2[1] = M2[1] + 2;
                    M3[1] = M3[1] + 1;


                    SetToLetter();
                    State = 0;
                    break;
            }
        }
    }
}
