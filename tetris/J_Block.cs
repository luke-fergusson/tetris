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
        /*int[] M1 = new int[] { 4, 0 }; // { x,y } 
        int[] M2 = new int[] { 4, 1 };
        int[] M3 = new int[] { 5, 1 };
        int[] M4 = new int[] { 6, 1 };
        int CurrentM1;
        int CurrentM2;
        int CurrentM3;
        int CurrentM4;*/
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
            M1 = new int[] { 4, 0 }; // { x,y } 
            M2 = new int[] { 4, 1 };
            M3 = new int[] { 5, 1 };
            M4 = new int[] { 6, 1 };

            CurrentLetter = 'j';
        }
        
       
        
        
        public override bool GroundCollision()
        {
            if(State == 0 || State == 1 )
            {
                BottomColumn = M4[0];
                BottomRow = M4[1];
            }
            if(State == 2)
            {
                BottomColumn = M1[0];
                BottomRow = M1[1];
            }
            
            return base.GroundCollision();
        }
        public override bool RWallCollision()
        {
            if (State == 0 || State ==2)
            {
                BottomColumn = M4[0];
                BottomRow = M4[1];
            }
            if(State == 1 || State == 3)
            {
                BottomColumn = M1[0];
                BottomRow = M1[1];
            }
            return base.RWallCollision();
        }
        public override bool LWallCollision()
        {
            
            if (State == 0 || State == 1)
            {
                BottomRow = M2[1];
                BottomColumn = M2[0];
            }
            if (State == 2 || State == 3)
            {
                BottomColumn = M1[0];
                BottomRow = M1[1];
            }
            
            return base.LWallCollision();
        }
        public override bool BlockCollision()
        {
           
            PB = board.GetBoard();

            if (!GroundCollision())
            {
                if(State == 0)
                {
                    if (PB[M2[0], M2[1]+1] != '0'|| PB[M3[0], M3[1]+1] != '0' || PB[M4[0], M4[1] +1] != '0')
                    {
                        return true;
                    }
                }
                if(State == 1 || State  == 3)
                {
                    if (PB[M1[0], M1[1]+1] != '0' || PB[M4[0], M4[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if(State == 2)
                {
                    if (PB[M1[0], M1[1] + 1] != '0' || PB[M4[0], M4[1] + 1] != '0' || PB[M2[0], M2[1] + 1] != '0')
                    {
                        return true;
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

                    M1[1] = M1[1] -1;
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

                    M1[1] = M1[1] +1;
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
                    /*Debug.WriteLine("M1 " + M1[0] + ", " + M1[1]);
                    Debug.WriteLine("M2 " + M2[0] + ", " + M2[1]);
                    Debug.WriteLine("M3 " + M3[0] + ", " + M3[1]);
                    Debug.WriteLine("M4 " + M4[0] + ", " + M4[1]);*/
                    
                    SetToLetter();
                    State = 0;
                    break;
            }
        }
    }
}
