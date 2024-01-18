using SharpDX;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace tetris
{
    public class GameAi : Blocks
    {
        public Blocks block;

        private int highestRow = 20;
        public char[,] simBoard;

        public GameAi()
        {
            simBoard = new char[10, 20];
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    simBoard[i, j] = '0';
                }
            }
        }
       
        public override void SetToZero()
        {
            simBoard[M1[0], M1[1]] = '0';
            simBoard[M2[0], M2[1]] = '0';
            simBoard[M3[0], M3[1]] = '0';
            simBoard[M4[0], M4[1]] = '0';
        }
        public override void SetToLetter()
        {
            simBoard[M1[0], M1[1]] = CurrentLetter;
            simBoard[M2[0], M2[1]] = CurrentLetter;
            simBoard[M3[0], M3[1]] = CurrentLetter;
            simBoard[M4[0], M4[1]] = CurrentLetter;
        }
        public void SimulateMove()
        {
            simBoard = board.GetBoard();
            

            //T_block
            
            if (block is T_Block TB)
            {
                CurrentLetter = TB.CurrentLetter;
                
                if (TB.State == 0)
                {
                    for (int i = 1; i <= 9-TB.M3[0]; i++)
                    {
                        TB.Right();
                        if(!TB.GroundCollision() || TB.BlockCollision())
                        {
                            TB.Down();
                        }
                    }
                }
            }
            
        }
        public void Highest()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (simBoard[i, j] != '0')
                    {
                        if (j <= highestRow)
                        {
                            highestRow = j;
                        }
                    }
                }
            }
        }
    }
}
