using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class I_Block : Blocks
    {     
        int[] M1 = new int[] {4, 0};
        int[] M2 = new int[] {4, 1};
        int[] M3 = new int[] {4, 2};
        int[] M4 = new int[] {4, 3};
        int CurrentM1;
        int CurrentM2;
        int CurrentM3;
        int CurrentM4;
        /*
         *          M1
         *          M2
         *          M3
         *          M4
         * 
         * I block
         */
        public I_Block()
        {
            State = 0;

        }
        public override void StarPosition()
        {
            board.ChangeBoard(M1[0], M1[1], 'i');
            board.ChangeBoard(M2[0], M2[1], 'i');
            board.ChangeBoard(M3[0], M3[1], 'i');
            board.ChangeBoard(M4[0], M4[1], 'i');
        }
        public override void Down()
        {
            board.ChangeBoard(M1[0], M1[1], '0');
            board.ChangeBoard(M2[0], M2[1], '0');
            board.ChangeBoard(M3[0], M3[1], '0');
            board.ChangeBoard(M4[0], M4[1], '0');
            M1[1] = M1[1] + 1;
            M2[1] = M2[1] + 1;
            M3[1] = M3[1] + 1;
            M4[1] = M4[1] + 1;
            board.ChangeBoard(M1[0], M1[1], 'i');
            board.ChangeBoard(M2[0], M2[1], 'i');
            board.ChangeBoard(M3[0], M3[1], 'i');
            board.ChangeBoard(M4[0], M4[1], 'i');
        }
        public override void Right()
        {
            board.ChangeBoard(M1[0], M1[1], '0');
            board.ChangeBoard(M2[0], M2[1], '0');
            board.ChangeBoard(M3[0], M3[1], '0');
            board.ChangeBoard(M4[0], M4[1], '0');
            M1[0] = M1[0] + 1;
            M2[0] = M2[0] + 1;
            M3[0] = M3[0] + 1;
            M4[0] = M4[0] + 1;
            board.ChangeBoard(M1[0], M1[1], 'i');
            board.ChangeBoard(M2[0], M2[1], 'i');
            board.ChangeBoard(M3[0], M3[1], 'i');
            board.ChangeBoard(M4[0], M4[1], 'i');
        }
        public override void Left()
        {
            board.ChangeBoard(M1[0], M1[1], '0');
            board.ChangeBoard(M2[0], M2[1], '0');
            board.ChangeBoard(M3[0], M3[1], '0');
            board.ChangeBoard(M4[0], M4[1], '0');
            M1[0] = M1[0] - 1;
            M2[0] = M2[0] - 1;
            M3[0] = M3[0] - 1;
            M4[0] = M4[0] - 1;
            board.ChangeBoard(M1[0], M1[1], 'i');
            board.ChangeBoard(M2[0], M2[1], 'i');
            board.ChangeBoard(M3[0], M3[1], 'i');
            board.ChangeBoard(M4[0], M4[1], 'i');
        }
        public override bool GroundCollision()
        {
            BottomRow = M4[1];
            BottomColumn = M4[0];
            return base.GroundCollision();
        }
        public override bool RWallCollision()
        {
            BottomRow = M4[1];
            BottomColumn = M4[0];
            return base.RWallCollision();
        }
        public override bool LWallCollision()
        {
            BottomRow = M4[1];
            BottomColumn = M4[0];
            return base.LWallCollision();
        }
        public override bool BlockCollision()
        {
            BottomRow = M4[1];
            BottomColumn = M4[0];
            PB = board.GetBoard();
            return base.BlockCollision();
            
        }
        public override void RotateClockwise()
        {
            
            switch (State)
            {
                case 0:
       
                    board.ChangeBoard(M1[0], M1[1], '0');
                    board.ChangeBoard(M2[0], M2[1], '0');
                    board.ChangeBoard(M3[0], M3[1], '0');
                    board.ChangeBoard(M4[0], M4[1], '0');// replacing previous poistion with 0's 

                    M1[0] = M2[0] - 1;
                    M3[0] = M2[0] + 1;
                    M4[0] = M2[0] + 2;// rotating clockwise

                    M1[1] = M2[1];
                    M3[1] = M2[1];
                    M4[1] = M2[1];

                    board.ChangeBoard(M1[0], M1[1], 'i');
                    board.ChangeBoard(M2[0], M2[1], 'i');
                    board.ChangeBoard(M3[0], M3[1], 'i');
                    board.ChangeBoard(M4[0], M4[1], 'i');// set new position with i's
                    State = 1;
                    break;
                case 1:
                    board.ChangeBoard(M1[0], M1[1], '0');
                    board.ChangeBoard(M2[0], M2[1], '0');
                    board.ChangeBoard(M3[0], M3[1], '0');
                    board.ChangeBoard(M4[0], M4[1], '0');
                    Debug.WriteLine(M1[0] + ", " + M1[1]);
                    Debug.WriteLine(M2[0] + ", " + M2[1]);
                    Debug.WriteLine(M3[0] + ", " + M3[1]);
                    Debug.WriteLine(M4[0] + ", " + M4[1]);

                    M1[0] = M3[0];
                    M2[0] = M3[0];
                    M4[0] = M3[0];

                    M1[1] = M3[1] - 1;
                    M3[1] = M3[1] + 1;
                    M4[1] = M3[1] + 1;

                    Debug.WriteLine(M1[0] + ", " + M1[1]);
                    Debug.WriteLine(M2[0] + ", " + M2[1]);
                    Debug.WriteLine(M3[0] + ", " + M3[1]);
                    Debug.WriteLine(M4[0] + ", " + M4[1]);
                    board.ChangeBoard(M1[0], M1[1], 'i');
                    board.ChangeBoard(M2[0], M2[1], 'i');
                    board.ChangeBoard(M3[0], M3[1], 'i');
                    board.ChangeBoard(M4[0], M4[1], 'i');
                    State = 2;
                    break;
                case 2:
                    board.ChangeBoard(M1[0], M1[1], '0');
                    board.ChangeBoard(M2[0], M2[1], '0');
                    board.ChangeBoard(M3[0], M3[1], '0');
                    board.ChangeBoard(M4[0], M4[1], '0');
                    Debug.WriteLine(M1[0] + ", " + M1[1]);
                    Debug.WriteLine(M2[0] + ", " + M2[1]);
                    Debug.WriteLine(M3[0] + ", " + M3[1]);
                    Debug.WriteLine(M4[0] + ", " + M4[1]);

                    M1[0] = M3[0] -2;
                    M2[0] = M3[0] - 1;
                    M4[0] = M3[0]+1;

                    M1[1] = M3[1];
                    M2[1] = M3[1];
                    M4[1] = M3[1];

                    Debug.WriteLine(M1[0] + ", " + M1[1]);
                    Debug.WriteLine(M2[0] + ", " + M2[1]);
                    Debug.WriteLine(M3[0] + ", " + M3[1]);
                    Debug.WriteLine(M4[0] + ", " + M4[1]);
                    board.ChangeBoard(M1[0], M1[1], 'i');
                    board.ChangeBoard(M2[0], M2[1], 'i');
                    board.ChangeBoard(M3[0], M3[1], 'i');
                    board.ChangeBoard(M4[0], M4[1], 'i');
                    State = 0;
                    break;
                
            }

            
        }
    }
}
