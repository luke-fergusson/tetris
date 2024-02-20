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
        public bool FillHole { get; set; }
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

        public void SetToZero()// stay the same
        {
            simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1]] = '0';
            simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1]] = '0';
            simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1]] = '0';
            simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1]] = '0';
        }
        public void SetToLetter()// stay the same 
        {
          
            simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1]] = currentBlock.CurrentLetter;
            simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1]] = currentBlock.CurrentLetter;
            simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1]] = currentBlock.CurrentLetter;
            simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1]] = currentBlock.CurrentLetter;
              
        }
        public bool GroundCollisionSBlock()// different
        {
            if(currentBlock.State == 0 )
            {
                if (currentBlock.M1[1] == 19)
                {
                    return true;
                }
            }
            if(currentBlock.State ==1 || currentBlock.State == 2 )
            {
                if (currentBlock.M4[1] == 19 || currentBlock.M2[1] == 19)
                {
                    return true;
                }
            }
            if(currentBlock.State == 3)
            {
                if ((currentBlock.M3[1])== 19 || currentBlock.M1[1] == 19)
                {
                    return true;
                }
            }
           
                
            return false;
        }
        public void Down()// stay the same 
        {
            SetToZero();
            currentBlock.M1[1] = currentBlock.M1[1] + 1;
            currentBlock.M2[1] = currentBlock.M2[1] + 1;
            currentBlock.M3[1] = currentBlock.M3[1] + 1;
            currentBlock.M4[1] = currentBlock.M4[1] + 1;
            SetToLetter();
        }
        public void Right()// stay the same 
        {
            SetToZero();
            currentBlock.M1[0] = currentBlock.M1[0] + 1;
            currentBlock.M2[0] = currentBlock.M2[0] + 1;
            currentBlock.M3[0] = currentBlock.M3[0] + 1;
            currentBlock.M4[0] = currentBlock.M4[0] + 1;
            SetToLetter();
        }
        public void Left()// stay the same 
        {
            SetToZero();
            currentBlock.M1[0] = currentBlock.M1[0] - 1;
            currentBlock.M2[0] = currentBlock.M2[0] - 1;
            currentBlock.M3[0] = currentBlock.M3[0] - 1;
            currentBlock.M4[0] = currentBlock.M4[0] - 1;
            SetToLetter();
        }
        public bool BlockCollisionSBlock() // different
        {


            if (!GroundCollisionSBlock())
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
        public void RotateClockwiseSBlock() // different
        {
            switch (currentBlock.State)
            {
                case 1:
                    SetToZero();

                    M1[0] = M1[0] + 1;
                    M3[0] = M3[0] + 1;

                    M1[1] = M1[1] - 1;
                    M3[1] = M3[1] + 1;
                    M4[1] = M4[1] + 2;

                    SetToLetter();
                    break;
                case 2:
                    SetToZero();

                    M1[0] = M1[0] + 1;
                    M3[0] = M3[0] - 1;
                    M4[0] = M4[0] - 2;

                    M1[1] = M1[1] + 1;
                    M3[1] = M3[1] + 1;
                    SetToLetter();
                    break;
                case 3:
                    SetToZero();

                    M1[0] = M1[0] - 1;
                    M3[0] = M3[0] - 1;

                    M1[1] = M1[1] + 1;

                    M3[1] = M3[1] - 1;
                    M4[1] = M4[1] - 2;

                    SetToLetter();
                    break;
               
                default:
                    break;
            
            }
        }
    
        public void SimulateMove(Type blockName,char[,] WorkBoard, Blocks blocks) // stay the same 
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
                        RotateClockwiseSBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 1)
                    {
                        Down();
                        Down();
                        RotateClockwiseSBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 2)
                    {
                        Down();
                        Down();// so rotation doesn't go out of bounds
                        RotateClockwiseSBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 3)
                    {
                        Down();
                        RotateClockwiseSBlock();
                        CheckLogicBlock(blockName);
                    }
                   
                }

            }
            //T_block
            if (blockName.FullName == "tetris.T_Block")
            {
                currentBlock = new T_Block();
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
                        RotateClockwiseTBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 1)
                    {

                        Down();
                        RotateClockwiseTBlock();
                        CheckLogicBlock(blockName);
                    }
                    //if (currentBlock.State == 2)
                    //{
                    //    Down();
                    //    Down();
                    //    RotateClockwiseTBlock();
                    //    CheckLogicBlock(blockName);
                    //}
                    //if (currentBlock.State == 3)
                    //{
                    //    Down();
                    //    RotateClockwiseTBlock();
                    //    CheckLogicBlock(blockName);
                    //}

                }

            }

        }


        public void RotateClockwiseTBlock() // different
        {
            switch (currentBlock.State)
            {
                case 1:
                    SetToZero();

                    currentBlock.M1[0] = currentBlock.M1[0] + 1;
                    M3[0] = M3[0] - 1;
                    M4[0] = M4[0] + 1;


                    M1[1] = M1[1] - 2;
                    M2[1] = M2[1] - 1;

                    SetToLetter();
                    break;
                case 2:
                    SetToZero();

                    M1[0] = M1[0] + 1;
                    M3[0] = M3[0] - 1;
                    M4[0] = M4[0] - 1;

                    M1[1] = M1[1] + 1;
                    M3[1] = M3[1] - 1;
                    M4[1] = M4[1] + 1;
                    SetToLetter();
                    break;
                case 3:
                    SetToZero();

                    M1[0] = M1[0] - 1;
                    M3[0] = M3[0] + 1;
                    M4[0] = M4[0] - 1;


                    M1[1] = M1[1] + 1;
                    M3[1] = M3[1] - 1;
                    M4[1] = M4[1] - 1;

                    SetToLetter();
                    break;

                default:
                    break;

            }
        }
        public bool BlockCollisionTBlock() // different
        {
            if (!GroundCollisionSBlock())
            {
                if (currentBlock.State == 0)
                {
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 1)
                {
                    if (simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 2)
                {
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 3)
                {
                    if (simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool GroundCollisionTBlock()// different
        {
            if (currentBlock.State == 0|| currentBlock.State == 3)
            {
                if (currentBlock.M1[1] == 19)
                {
                    return true;
                }
            }
            if (currentBlock.State == 1 )
            {
                if (currentBlock.M3[1] == 19 )
                {
                    return true;
                }
            }
            if (currentBlock.State == 2)
            {
                if (currentBlock.M4[1] == 19)
                {
                    return true;
                }
            }

            return false;
        }
        public void CheckLogicBlock(Type name)//same
        {
            int count;
            int leftNum = 0;
            int rightNum = 0;
            switch (name.FullName)
            {
                case "tetris.S_Block":
                    leftNum = 5;
                    rightNum = 4;
                    break;
                case "tetris.T_Block":
                    leftNum = 5;
                    rightNum = 4;
                    break;


            }
            
            for (int i = 0; i < rightNum; i++)
            {
                count = 0;
                while (count < i)
                {
                    Right();
                    count++;
                }
                if(name.FullName == "tetris.S_Block")
                {
                    while (!GroundCollisionSBlock() && !BlockCollisionSBlock())
                    {
                        Down();
                    }
                }
                if(name.FullName == "tetris.T_Block")
                {
                    while (!GroundCollisionTBlock() && !BlockCollisionTBlock())
                    {
                        Down();
                    }
                }
                
                moves.Add(new BestMove { HorizontalMovement = count, RotationState = currentBlock.State, HighestPoint = Highest(), ClosestToWall = DistanceToWall(currentBlock.M4[0], currentBlock.M1[0], "Right"), Direction = "Right", NumOfGaps = CountGaps(), FillHole = CheckHole() });

                //Debug.WriteLine("M1 " + currentBlock.M1[0] + currentBlock.M1[1]);
                //Debug.WriteLine("M2 " + currentBlock.M2[0] + currentBlock.M2[1]);
                //Debug.WriteLine("M3 " + currentBlock.M3[0] + currentBlock.M3[1]);
                //Debug.WriteLine("M4 " + currentBlock.M4[0] + currentBlock.M4[1]);

                
                if (currentBlock.State == 4)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            Debug.Write(simBoard.currentGameBoards[k, j] + " ");
                        }
                        Debug.WriteLine(""); // Move to the next row
                    }
                    Debug.WriteLine("");
                }
                simBoard.currentGameBoards = (char[,])newBoard.currentGameBoards.Clone();
                currentBlock.M1 = (int[])M1.Clone();
                currentBlock.M2 = (int[])M2.Clone();
                currentBlock.M3 = (int[])M3.Clone();
                currentBlock.M4 = (int[])M4.Clone();
            }
            for (int i = 0; i < leftNum; i++)
            {
                count = 0;
                while (count < i)
                {
                    Left();
                    count++;
                   
                }
                if (name.FullName == "tetris.S_Block")
                {
                    while (!GroundCollisionSBlock() && !BlockCollisionSBlock())
                    {
                        Down();
                    }
                
                }
                if (name.FullName == "tetris.T_Block")
                {
                    while (!GroundCollisionTBlock() && !BlockCollisionTBlock())
                    {
                        Down();
                    }
                }
                moves.Add(new BestMove { HorizontalMovement = count, RotationState = currentBlock.State, HighestPoint = Highest(), ClosestToWall = DistanceToWall(currentBlock.M4[0], currentBlock.M1[0], "Left"), Direction = "Left", NumOfGaps = CountGaps(), FillHole = CheckHole() });
                if (currentBlock.State == 4)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        for (int j = 0; j < 20; j++)
                        {
                            Debug.Write(simBoard.currentGameBoards[k, j] + " ");
                        }
                        Debug.WriteLine(""); // Move to the next row
                    }
                    Debug.WriteLine("");
                }
                simBoard.currentGameBoards = (char[,])newBoard.currentGameBoards.Clone();
                currentBlock.M1 = (int[])M1.Clone();
                currentBlock.M2 = (int[])M2.Clone();
                currentBlock.M3 = (int[])M3.Clone();
                currentBlock.M4 = (int[])M4.Clone();
              
            }
        }
        public int Highest()// checks the highest point of each new point  // stay the same 
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
        public int DistanceToWall(int DM4, int DM1, string Dir)// places block closest to the wall first //???
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
        public int CountGaps()// does the move that creates the fewest number of holes // stay the same 
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
        public bool CheckHole()// checks if a hole has been filled by the new block // stay the same 
        {
            int CurrentRow = Highest();

            for (int i = 0; i < 10; i++)
            {
                if (CurrentRow + 1 < 19)
                {
                    if (simBoard.currentGameBoards[i, CurrentRow] == '0')
                    {
                        if (i + 1 < 10 && i - 1 > 0)
                        {
                            if (simBoard.currentGameBoards[i + 1, CurrentRow] != '0' && simBoard.currentGameBoards[i - 1, CurrentRow] != '0')
                            {
                                return true;
                            }
                        }

                        if (i == 9)
                        {
                            if (simBoard.currentGameBoards[i, CurrentRow] == '0' && simBoard.currentGameBoards[i - 1, CurrentRow] != '0')
                            {
                                return true;
                            }
                        }
                        if (i == 0)
                        {
                            if (simBoard.currentGameBoards[i, CurrentRow] == '0' && simBoard.currentGameBoards[i + 1, CurrentRow] != '0')
                            {
                                return true;
                            }
                        }


                    }
                }
            

            }
            return false;
        }
     
        public int Best()
        {
            List<BestMove> HPointList = new List<BestMove>();
            List<BestMove> GapList = new List<BestMove>();
            BestMove TopGap = new BestMove();
            
            HPoint = moves.OrderByDescending(Top => Top.HighestPoint).FirstOrDefault(); //.ThenBy(Top => Top.ClosestToWall).FirstOrDefault();//highest point on the board
            //foreach (BestMove move in moves)
            //{
            //    if (move.FillHole == true)
            //    {
            //        HPoint = move;
            //        return HPoint.HorizontalMovement;
            //    }
            //}

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
            
            Debug.WriteLine(" HM " + HPoint.HorizontalMovement);
            Debug.WriteLine(" HP " + HPoint.HighestPoint);
            Debug.WriteLine(" RS " + HPoint.RotationState);
            
            HPointList.Clear();
            moves.Clear();
            GapList.Clear();
            return HPoint.HorizontalMovement;
        }
       // to get back to normal remove the foreach loop


    }
}