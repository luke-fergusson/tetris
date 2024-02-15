using SharpDX;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
        public string Direction { get; set; }
        public int NumOfGaps { get; set; }
    }

    public class GameAi 
    {
        public Blocks currentBlock;
   
        private int highestRow = 20;

        public Board simBoard = new Board();
        public Board newBoard = new Board();
        public List<BestMove> moves = new List<BestMove>();
        public BestMove HPoint;

        public int[] M1;
        public int[] M2;
        public int[] M3;
        public int[] M4;

        public GameAi()
        {
            newBoard.currentGameBoards = new char[10, 20];
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
          
            simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1]] = currentBlock.CurrentLetter;
            simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1]] = currentBlock.CurrentLetter;
            simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1]] = currentBlock.CurrentLetter;
            simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1]] = currentBlock.CurrentLetter;
              
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
        public bool BlockCollision()
        {


            if (!currentBlock.GroundCollision())
            {
                if (currentBlock.State == 0)
                {
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1]+1] != '0' || simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1] +1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] +1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 1)
                {
                    if (simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 2)
                {
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 3)
                {
                    if (simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0')
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void SimulateMove(Type blockName,char[,] WorkBoard, Blocks blocks)
        {

            simBoard.currentGameBoards = (char[,])blocks.board.GetBoard().Clone();
            newBoard.currentGameBoards = (char[,])simBoard.currentGameBoards.Clone();
           
            //S_block

            if (blockName.FullName == "tetris.S_Block")
            {
                currentBlock = new S_Block();
                M1 = (int[])currentBlock.M1.Clone();   
                M2 = (int[])currentBlock.M2.Clone();
                M3 = (int[])currentBlock.M3.Clone();
                M4 = (int[])currentBlock.M4.Clone();

                for (int k = 0; k < 4; k++)
                {
                    currentBlock.State = k;


                    if (currentBlock.State == 0)
                    {
                        Down();
                        currentBlock.RotateClockwise();
                        CheckLogic();
                    }
                    //if(currentBlock.State == 1)
                    //{
                    //    Down();
                    //    currentBlock.RotateClockwise();
                    //    CheckLogic();
                    //}
                    //if (currentBlock.State == 2)
                    //{
                    //    Down();
                    //    Down();
                    //    currentBlock.RotateClockwise();
                    //    CheckLogic();
                    //}
                    //if (currentBlock.State == 3)
                    //{
                    //    Down();
                    //    currentBlock.RotateClockwise();
                    //    CheckLogic();
                    //}
                }

            }

        }
        public void CheckLogic()
        {
            int count;
            for (int i = 0; i < 4; i++)
            {
                count = 0;
                while (count < i)
                {
                    Right();
                    count++;
                }

                while (!currentBlock.GroundCollision() && !BlockCollision())
                {
                   Down();

                }
                moves.Add(new BestMove { HorizontalMovement = count, RotationState = currentBlock.State, HighestPoint = Highest(), ClosestToWall = DistanceToWall(currentBlock.M4[0], currentBlock.M1[0], "Right"), Direction = "Right", NumOfGaps = CountGaps() });

                //Debug.WriteLine("M1 " + currentBlock.M1[0] + currentBlock.M1[1]);
                //Debug.WriteLine("M2 " + currentBlock.M2[0] + currentBlock.M2[1]);
                //Debug.WriteLine("M3 " + currentBlock.M3[0] + currentBlock.M3[1]);
                //Debug.WriteLine("M4 " + currentBlock.M4[0] + currentBlock.M4[1]);

                //for (int k = 0; k < 10; k++)
                //{
                //    for (int j = 0; j < 20; j++)
                //    {
                //        Debug.Write(simBoard.currentGameBoards[k, j] + " ");
                //    }
                //    Debug.WriteLine(""); // Move to the next row
                //}
                //Debug.WriteLine("");
                simBoard.currentGameBoards = (char[,])newBoard.currentGameBoards.Clone();
                currentBlock.M1 = (int[])M1.Clone();
                currentBlock.M2 = (int[])M2.Clone();
                currentBlock.M3 = (int[])M3.Clone();
                currentBlock.M4 = (int[])M4.Clone();
            }
            for (int i = 0; i < 5; i++)
            {
                count = 0;
                while (count < i)
                {
                    Left();
                    count++;
                }
                //Debug.WriteLine("M1 " + currentBlock.M1[0] + currentBlock.M1[1]);
                //Debug.WriteLine("M2 " + currentBlock.M2[0] + currentBlock.M2[1]);
                //Debug.WriteLine("M3 " + currentBlock.M3[0] + currentBlock.M3[1]);
                //Debug.WriteLine("M4 " + currentBlock.M4[0] + currentBlock.M4[1]);
                while (!currentBlock.GroundCollision() && !BlockCollision())
                {
                    Down();


                }
                moves.Add(new BestMove { HorizontalMovement = count, RotationState = currentBlock.State, HighestPoint = Highest(), ClosestToWall = DistanceToWall(currentBlock.M4[0], currentBlock.M1[0], "Left"), Direction = "Left", NumOfGaps = CountGaps() });
                simBoard.currentGameBoards = (char[,])newBoard.currentGameBoards.Clone();
                currentBlock.M1 = (int[])M1.Clone();
                currentBlock.M2 = (int[])M2.Clone();
                currentBlock.M3 = (int[])M3.Clone();
                currentBlock.M4 = (int[])M4.Clone();
                //    for (int k = 0; k < 10; k++)
                //    {
                //        for (int j = 0; j < 20; j++)
                //        {
                //            Debug.Write(simBoard.currentGameBoards[k, j] + " ");
                //        }
                //        Debug.WriteLine(""); // Move to the next row
                //    }
                //    Debug.WriteLine("");
            }
        }
        public int Highest()
        {
            bool breakloop = false;
            highestRow = 19;
            for (int j = 0; j < 20; j++)
            {
                if (breakloop == false)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (simBoard.currentGameBoards[i, j] != '0' && j != 0 && j != 1)
                        {
                            
                            highestRow = j;
                            Debug.WriteLine(highestRow);
                            breakloop = true;
                            break;
                        }
                    }
                }
            }
            
            return highestRow;
        }
        public int DistanceToWall(int DM4, int DM1, string Dir)
        {
            int RWall = 9;
            int LWall = 0;
            RWall = RWall - DM4;
            LWall = LWall + DM1;


            if (Dir == "Right")
            {
                return RWall;
            }
            else
            {
                return LWall;
            }

        }
        public int CountGaps()
        {
            int CurrentRow = Highest();
            int count = 0;

            for (int i = 0; i < 10; i++)
            {
                if(CurrentRow +1 < 19)
                {
                    if (simBoard.currentGameBoards[i, CurrentRow + 1] == '0')
                    {
                        count++;
                    }
                }
                   
            }
            return count;
        }
        public int Best()
        {
            List<BestMove> HPointList = new List<BestMove>();
            List<BestMove> GapList = new List<BestMove>();
            BestMove TopGap = new BestMove();
            
            HPoint = moves.OrderByDescending(Top => Top.HighestPoint).FirstOrDefault(); //.ThenBy(Top => Top.ClosestToWall).FirstOrDefault();//highest point on the board
            
          
            foreach (BestMove bestMove in moves)
            {
                if (HPoint.HighestPoint == bestMove.HighestPoint)
                {
                    HPointList.Add(bestMove);
                }
                
            }
            TopGap = HPointList.OrderBy(Gap => Gap.NumOfGaps).FirstOrDefault();
            foreach(BestMove bestMove in HPointList)
            {
                if(TopGap.NumOfGaps == bestMove.NumOfGaps)
                {
                    GapList.Add(bestMove);
                }
            }
            HPoint = GapList.OrderBy(Top => Top.ClosestToWall).FirstOrDefault();
            
            Debug.WriteLine(" this" + HPoint.HorizontalMovement);
            Debug.WriteLine(" this" + HPoint.HighestPoint);
            Debug.WriteLine(" this" + HPoint.NumOfGaps);
            
            HPointList.Clear();
            moves.Clear();
            GapList.Clear();
            return HPoint.HorizontalMovement;
        }
       


    }
}