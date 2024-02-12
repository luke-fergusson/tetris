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

    public class GameAi 
    {
        public Blocks currentBlock;
        public int[] M1;
        public int[] M2;
        public int[] M3;
        public int[] M4;
        public char CurrentLetter;
        private int highestRow = 20;
        //public char[,] simBoard;
        //public char[,] newBoard;
        public Board simBoard = new Board();
        public Board newBoard = new Board();
        public List<BestMove> moves = new List<BestMove>();



        public GameAi()
        {
            //simBoard = new char[10, 20];
            //newBoard = new char[10, 20];
            //simBoard.currentGameBoards = currentBlock.board.GetBoard();
            newBoard.currentGameBoards = new char[10, 20];
            /*for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    simBoard[i, j] = '0';
                }
            }*/
        }

        public void SetToZero()
        {
            simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1]] = '0';
            simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1]] = '0';
            simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1]] = '0';
            simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1]] = '0';
        }
        public void SetToLetter()
        {
          
            simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1]] = CurrentLetter;
            simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1]] = CurrentLetter;
            simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1]] = CurrentLetter;
            simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1]] = CurrentLetter;
              
        }
        public bool GroundCollision()
        {
            if (currentBlock.M1[1] + 1 == 19)
            {
                return true;
            }
            return false;
        }
        public void Down()
        {
            SetToZero();
            currentBlock.M1[1] = currentBlock.M1[1] + 1;
            currentBlock.M2[1] = currentBlock.M2[1] + 1;
            currentBlock.M3[1] = currentBlock.M3[1] + 1;
            currentBlock.M4[1] = currentBlock.M4[1] + 1;
            SetToLetter();
        }
        public void Right()
        {
            SetToZero();
            currentBlock.M1[0] = currentBlock.M1[0] + 1;
            currentBlock.M2[0] = currentBlock.M2[0] + 1;
            currentBlock.M3[0] = currentBlock.M3[0] + 1;
            currentBlock.M4[0] = currentBlock.M4[0] + 1;
            SetToLetter();
        }
        public void Left()
        {
            SetToZero();
            currentBlock.M1[0] = currentBlock.M1[0] - 1;
            currentBlock.M2[0] = currentBlock.M2[0] - 1;
            currentBlock.M3[0] = currentBlock.M3[0] - 1;
            currentBlock.M4[0] = currentBlock.M4[0] - 1;
            SetToLetter();
        }
        public void SimulateMove(Type blockName,char[,] WorkBoard, Blocks blocks)
        {

            simBoard.currentGameBoards = (char[,])blocks.board.GetBoard().Clone();
            newBoard.currentGameBoards = simBoard.currentGameBoards;
            int count;

            //S_block

            if (blockName.FullName == "tetris.S_Block")
            {
                currentBlock = new S_Block();
                

                M1 = currentBlock.M1;
                M2 = currentBlock.M2;
                M3 = currentBlock.M3;
                M4 = currentBlock.M4;
                CurrentLetter = currentBlock.CurrentLetter;

                if (currentBlock.State == 0)
                {
                    for (int i = 1; i <= 9 - currentBlock.M3[0]; i++)
                    {
                        count = 0;
                        while (count < i)
                        {
                            Right();
                            count++;
                        }

                        while (!currentBlock.GroundCollision() && !currentBlock.BlockCollision())
                        {
                            Down();
                        }

                        simBoard.currentGameBoards = newBoard.currentGameBoards;
                        moves.Add(new BestMove { HorizontalMovement = count + 1, RotationState = currentBlock.State, HighestPoint = M1[1], ClosestToWall = DistanceToWall(M4[0], M1[1]) });
                        Debug.WriteLine(M1[1]);
                    }
                    while (!currentBlock.GroundCollision() && !currentBlock.BlockCollision())
                    {
                        Down();
                    }

                    simBoard.currentGameBoards = newBoard.currentGameBoards;
                    moves.Add(new BestMove { HorizontalMovement = 0, RotationState = currentBlock.State, HighestPoint = M1[1], ClosestToWall = DistanceToWall(M4[0], M1[1]) });
                    Debug.WriteLine(M1[1]);
                }

            }

        }
        //public int Highest()
        //{

        //    highestRow = -1;
        //    for (int j = 0; j < 20; j++)
        //    {
        //        for (int i = 0; i < 10; i++)
        //        {
        //            if (simBoard.currentGameBoards[i, j] != '0')
        //            {
        //                if (j >= highestRow)
        //                {
        //                    highestRow = j;
        //                    Debug.WriteLine(highestRow);
        //                }
        //            }
        //        }
        //    }
        //    //for (int i = 0; i < 10; i++)
        //    //{
        //    //    for (int j = 0; j < 20; j++)
        //    //    {
        //    //        Debug.Write(simBoard[i, j] + " ");
        //    //    }
        //    //    Debug.WriteLine(""); // Move to the next row
        //    //}
        //    return highestRow;
        //}
        public int DistanceToWall(int DM4, int DM1)
        {
            int RWall = 9;
            int LWall = 0;
            RWall = RWall - DM4;
            LWall = LWall + DM1;
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 20; j++)
            //    {
            //        Debug.Write(simBoard.currentGameBoards[i, j] + " ");
            //    }
            //    Debug.WriteLine(""); // Move to the next row
            //}
            if (RWall < LWall)
            {
                Debug.WriteLine(RWall);
                return RWall;
            }
            else
            {
                return LWall;
            }
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 20; j++)
            //    {
            //        if (simBoard.currentGameBoards[i, j] != '0')
            //        {
            //            RWall = RWall - i;
            //            LWall = LWall + i;
            //            if (RWall < LWall)
            //            {
            //                Debug.WriteLine(RWall);
            //                return RWall;
            //            }
            //            else
            //            {
            //                return LWall;
            //            }
            //        }

            //    }
            //}
            //return 0;


        }
        public int Best()
        {
            List<BestMove> HPointList = new List<BestMove>();
            BestMove HPoint;
            HPoint = moves.OrderByDescending(Top => Top.HighestPoint).FirstOrDefault(); //.ThenBy(Top => Top.ClosestToWall).FirstOrDefault();//highest point on the board

            foreach (BestMove bestMove in moves)
            {
                if (HPoint.HighestPoint == bestMove.HighestPoint)
                {
                    HPointList.Add(bestMove);
                }
            }
            HPoint = HPointList.OrderBy(Top => Top.ClosestToWall).FirstOrDefault();
            Debug.WriteLine(" this" + HPoint.HorizontalMovement);
            Debug.WriteLine(" this" + HPoint.HighestPoint);
            HPointList.Clear();
            moves.Clear();
            return HPoint.HorizontalMovement;
        }
        


    }
}