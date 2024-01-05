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
    public class J_Block : Blocks
    {
        int[] M1 = new int[] { 4, 0 }; // { x,y } 
        int[] M2 = new int[] { 4, 1 };
        int[] M3 = new int[] { 5, 1 };
        int[] M4 = new int[] { 6, 1 };
        int CurrentM1;
        int CurrentM2;
        int CurrentM3;
        int CurrentM4;
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
        }
        public override void StarPosition()
        {
            board.ChangeBoard(M1[0], M1[1], 'j');
            board.ChangeBoard(M2[0], M2[1], 'j');
            board.ChangeBoard(M3[0], M3[1], 'j');
            board.ChangeBoard(M4[0], M4[1], 'j');
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
            board.ChangeBoard(M1[0], M1[1], 'j');
            board.ChangeBoard(M2[0], M2[1], 'j');
            board.ChangeBoard(M3[0], M3[1], 'j');
            board.ChangeBoard(M4[0], M4[1], 'j');
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
            board.ChangeBoard(M1[0], M1[1], 'j');
            board.ChangeBoard(M2[0], M2[1], 'j');
            board.ChangeBoard(M3[0], M3[1], 'j');
            board.ChangeBoard(M4[0], M4[1], 'j');
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
            board.ChangeBoard(M1[0], M1[1], 'j');
            board.ChangeBoard(M2[0], M2[1], 'j');
            board.ChangeBoard(M3[0], M3[1], 'j');
            board.ChangeBoard(M4[0], M4[1], 'j');
        }
        public override bool GroundCollision()
        {
            BottomColumn = M4[0];// change this based on state for rotation
            BottomRow = M4[1];
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
            BottomRow = M2[1];
            BottomColumn = M2[0];
            return base.LWallCollision();
        }
        public override bool BlockCollision()
        {
            BottomRow = M4[1];
            BottomColumn = M4[0];
            PB = board.GetBoard();

            return base.BlockCollision();

        }
    }
}
