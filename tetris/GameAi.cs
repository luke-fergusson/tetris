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
    public struct BestMove
    {
        public int HorizontalMovement { get; set; }
        public int RotationState { get; set; }
        public int HighestPoint { get; set; }
        public int ClosestToWall { get; set; }
    }

    public class GameAi : Blocks 
    {
        public Blocks currentBlock;

        private int highestRow = 20;
        public char[,] simBoard;
        public List<BestMove> moves = new List<BestMove>();
        
        

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
        public override bool GroundCollision()
        {
            if (M1[1] +1 == 19)
            {
                return true;
            }
            return false;
        }
        public void newDown()
        {
            SetToZero();
            M1[1] = M1[1] + 1;
            M2[1] = M2[1] + 1;
            M3[1] = M3[1] + 1;
            M4[1] = M4[1] + 1;
            SetToLetter();
        }
        public override void Right()
        {
            SetToZero();
            M1[0] = M1[0] + 1;
            M2[0] = M2[0] + 1;
            M3[0] = M3[0] + 1;
            M4[0] = M4[0] + 1;
            SetToLetter();
        }
        public override void Left()
        {
            SetToZero();
            M1[0] = M1[0] - 1;
            M2[0] = M2[0] - 1;
            M3[0] = M3[0] - 1;
            M4[0] = M4[0] - 1;
            SetToLetter();
        }
        public void SimulateMove(Type block)
        {
            
            simBoard = board.GetBoard();
            int count;
            
            //S_block
            
            if (block.FullName == "tetris.S_Block")
            {
                currentBlock = new S_Block();

                M1 = currentBlock.M1;
                M2 = currentBlock.M2;
                M3 = currentBlock.M3;
                M4 = currentBlock.M4;
                CurrentLetter = currentBlock.CurrentLetter;
                
                if (currentBlock.State == 0)
                {
                    for (int i = 1; i <= 9-currentBlock.M3[0]; i++)
                    {
                        count = 0;
                        while(count < i)
                        {
                            Right();
                            count++;
                        }
                        
                        while(!currentBlock.GroundCollision() && !currentBlock.BlockCollision())
                        {
                            
                            newDown();
                        }

                        moves.Add( new BestMove { HorizontalMovement = count + 1, RotationState = currentBlock.State, HighestPoint = Highest(), ClosestToWall = DistanceToWall() });
                        simBoard = currentBlock.board.GetBoard();
                    }
                }
            }
            
        }
        public int Highest()
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
            return highestRow;
        }
        public int DistanceToWall()
        {
            int RWall = 9;
            int LWall = 0;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (simBoard[i,j] != '0')
                    {
                        RWall = RWall - i;
                        LWall = LWall + i;
                        if(RWall < LWall)
                        {
                            return RWall;
                        }
                        else
                        {
                            return LWall;
                        }
                    }
                    
                }
            }
            return 0;
        }
        public int Best()
        {
            BestMove HPoint = moves.OrderByDescending(Top  => Top.HighestPoint).ThenBy(Top => Top.ClosestToWall).FirstOrDefault();//highest point on the board
            
            return HPoint.HorizontalMovement;
        }
        
        
    }
}
