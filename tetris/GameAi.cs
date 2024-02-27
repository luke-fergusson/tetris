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
        public bool CompleteRows { get; set; }
    }
    /// <summary>
    /// A stucture is used to store all the information about each block
    /// 
    /// HorizontalMovement - how far its been moved left or right
    /// RotationState - what rotation state it is for the optium move
    /// Highestpoint - the highest point after each block has been placed
    /// ClosestToWall - Distance to wall
    /// NumOfGaps - each move creates and do the one with the lowest amount
    /// FillHole - does it fill a hole
    /// CompleteRows - does it fill a hole
    /// </summary>
    public class GameAi 
    {
        public Blocks currentBlock;
   
        private int highestRow = 20;

        public Board simBoard = new Board();// generates a new gameboard to simulate moves
        public Board newBoard = new Board();// to store current game board
        public List<BestMove> moves = new List<BestMove>();// list of the struct of all moves played
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

        public void SimulateMove(Type blockName, Blocks blocks) // stay the same 
        {

            simBoard.currentGameBoards = (char[,])blocks.board.GetBoard().Clone();// .Clone() is used as arrays in c# are path traversed and doesn't automaticly copy the contentence from one to another, it links one to another
            newBoard.currentGameBoards = (char[,])simBoard.currentGameBoards.Clone();

            //S_block

            if (blockName.FullName == "tetris.S_Block")// checks for each block
            {
                currentBlock = new S_Block();
                M1 = (int[])currentBlock.M1.Clone();
                M2 = (int[])currentBlock.M2.Clone();
                M3 = (int[])currentBlock.M3.Clone();
                M4 = (int[])currentBlock.M4.Clone();

                for (int k = 0; k < 4; k++)
                {
                    currentBlock.State = k;// goes through each rotation state


                    if (currentBlock.State == 0)
                    {
                        Down();
                        RotateClockwiseSBlock();// rotates block based on rotation state
                        CheckLogicBlock(blockName); // simulates each move
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
                    if (currentBlock.State == 2)
                    {
                        Down();
                        Down();
                        RotateClockwiseTBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 3)
                    {
                        Down();
                        RotateClockwiseTBlock();
                        CheckLogicBlock(blockName);
                    }

                }

            }
            //I_block
            if (blockName.FullName == "tetris.I_Block")
            {
                currentBlock = new I_Block();
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
                        RotateClockwiseIBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 1)
                    {
                        Down();
                        Down();
                        RotateClockwiseIBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 2)
                    {
                        Down();
                        Down();
                        RotateClockwiseIBlock();
                        CheckLogicBlock(blockName);
                    }
                    

                }


            }
            //L_block
            if (blockName.FullName == "tetris.L_Block")
            {
                currentBlock = new L_Block();
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
                        RotateClockwiseLBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 1)
                    {
                        Down();
                        RotateClockwiseLBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 2)
                    {
                        Down();
                        Down();
                        RotateClockwiseLBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 3)
                    {
                        Down();
                        RotateClockwiseLBlock();
                        CheckLogicBlock(blockName);
                    }

                }

            }

            // O_block

            if (blockName.FullName == "O_Block")// tetrsi. doesn't work
            {
                currentBlock = new O_Block();
                M1 = (int[])currentBlock.M1.Clone();
                M2 = (int[])currentBlock.M2.Clone();
                M3 = (int[])currentBlock.M3.Clone();
                M4 = (int[])currentBlock.M4.Clone();

                Down();
                CheckLogicBlock(blockName);

            }
            //Z_block
            if (blockName.FullName == "tetris.Z_Block")
            {
                currentBlock = new Z_Block();
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
                        RotateClockwiseZBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 1)
                    {
                        Down();
                        Down();
                        RotateClockwiseZBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 2)
                    {
                        Down();
                        Down();
                        RotateClockwiseZBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 3)
                    {
                        Down();
                        RotateClockwiseZBlock();
                        CheckLogicBlock(blockName);
                    }

                }

            }
            //J_block
            if (blockName.FullName == "tetris.J_Block")
            {
                currentBlock = new J_Block();
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
                        RotateClockwiseJBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 1)
                    {
                        Down();
                        RotateClockwiseJBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 2)
                    {
                        Down();
                        Down();
                        RotateClockwiseJBlock();
                        CheckLogicBlock(blockName);
                    }
                    if (currentBlock.State == 3)
                    {
                        Down();
                        RotateClockwiseJBlock();
                        CheckLogicBlock(blockName);
                    }

                }
            }
        }
        /*
         * 
         * 
         *              J block
         * 
         * 
         * 
         */


        public void RotateClockwiseJBlock() // different
        {
            switch (currentBlock.State)
            {
                case 1:
                    SetToZero();
                    M1[1] = M1[1] + 1;
                    M2[1] = M2[1] + 1;
                    M3[1] = M3[1] + 1;
                    M4[1] = M4[1] + 1;

                    M1[0] = M1[0] + 2;
                    M2[0] = M2[0] + 1;
                    M4[0] = M4[0] - 1;

                    M1[1] = M1[1] - 1;
                    M2[1] = M2[1] - 2;
                    M3[1] = M3[1] - 1;

                    SetToLetter();
                    break;
                case 2:
                    SetToZero();

                    M1[0] = M1[0] - 2;
                    M3[0] = M3[0] + 1;
                    M4[0] = M4[0] + 1;

                    M1[1] = M1[1] + 1;
                    M2[1] = M2[1] + 1;
                    SetToLetter();
                    break;
                case 3:
                    SetToZero();

                    M3[0] = M3[0] - 1;
                    M4[0] = M4[0] - 1;

                    M1[1] = M1[1] + 1;
                    M2[1] = M2[1] - 1;

                    SetToLetter();
                    break;

                default:
                    break;

            }
        }
        public bool BlockCollisionJBlock() // different
        {
            if (!GroundCollisionJBlock())
            {
                if (currentBlock.State == 0)
                {
                    if (simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 1 || currentBlock.State == 3)
                {                
                    if (/*simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || */simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0')
                    {
                        return true;
                    }
                    
                }
                if (currentBlock.State == 2)
                {
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                
            }
            return false;
        }

        public bool GroundCollisionJBlock()// different
        {
            if (currentBlock.State == 0 || currentBlock.State == 3 || currentBlock.State == 1)
            {
                if (currentBlock.M4[1] == 19 || currentBlock.M2[1] == 19 || currentBlock.M3[1] == 19)
                {
                    return true;
                }
            }
            if (currentBlock.State == 2)
            {
                if (currentBlock.M1[1] == 19 || currentBlock.M4[1] == 19)
                {
                    return true;
                }
            }

            return false;
        }

        /*
         * 
         * 
         *          Z block
         * 
         * 
         * 
         * 
         */


        public void RotateClockwiseZBlock() // different
        {
            switch (currentBlock.State)
            {
                case 1:
                    SetToZero();
                    M1[1] = M1[1] + 2;
                    M2[1] = M2[1] + 2;
                    M3[1] = M3[1] + 2;
                    M4[1] = M4[1] + 2;


                    M1[0] = M1[0] + 2;
                    M2[0] = M2[0] + 1;
                    M4[0] = M4[0] - 1;

                    M2[1] = M2[1] + 1;
                    M4[1] = M4[1] + 1;
                    SetToLetter();
                    break;
                case 2:
                    SetToZero();

                    M2[0] = M2[0] - 1;
                    M4[0] = M4[0] - 1;

                    M1[1] = M1[1] + 2;
                    M2[1] = M2[1] + 1;
                    M4[1] = M4[1] - 1;

                    SetToLetter();
                    break;
                case 3:
                    SetToZero();

                    M1[0] = M1[0] - 2;
                    M2[0] = M2[0] - 1;
                    M4[0] = M4[0] + 1;

                    M2[1] = M2[1] - 1;
                    M4[1] = M4[1] - 1;

                    SetToLetter();
                    break;

                default:
                    break;

            }
        }
        public bool BlockCollisionZBlock() // different
        {
            if (!GroundCollisionZBlock())
            {
                if (currentBlock.State == 0 )
                {
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 1)
                {
                    if (simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 2)
                {
                    if (simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 3)
                {
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool GroundCollisionZBlock()// different
        {
            if (currentBlock.State == 0 || currentBlock.State == 1)
            {
                if (currentBlock.M4[1] == 19)
                {
                    return true;
                }
            }
            if (currentBlock.State == 2 || currentBlock.State == 3)
            {
                if (currentBlock.M1[1] == 19)
                {
                    return true;
                }              
            }


            return false;
        }

        /*
         * 
         *      O block
         * 
         * 
         * 
         */
        public bool BlockCollisionOBlock() // different
        {
            if (!GroundCollisionOBlock())
            {
                if (currentBlock.State == 0)
                {
                    if (simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0')
                    {
                        return true;
                    }
                }
               

            }
            return false;
        }

        public bool GroundCollisionOBlock()// different
        {
            if (currentBlock.State == 0)
            {
                if (currentBlock.M4[1] == 19)
                {
                    return true;
                }
            }
            
            return false;
        }
        /*
         * 
         * 
         *          I block
         * 
         * 
         * 
         * 
         */


        public void RotateClockwiseIBlock() // different
        {
            switch (currentBlock.State)
            {
                case 1:
                    SetToZero();
                    M1[1] = M1[1] + 4;
                    M2[1] = M2[1] + 4;
                    M3[1] = M3[1] + 4;
                    M4[1] = M4[1] + 4;

                 
                    M1[0] = M2[0] - 1;
                    M3[0] = M2[0] + 1;
                    M4[0] = M2[0] + 2;// rotating clockwise

                    M1[1] = M2[1];
                    M3[1] = M2[1];
                    M4[1] = M2[1];
                    SetToLetter();
                    break;
                case 2:
                    SetToZero();

                    M1[0] = M3[0];
                    M2[0] = M3[0];
                    M4[0] = M3[0];

                    M1[1] = M3[1] - 1;
                    M3[1] = M3[1] + 1;
                    M4[1] = M3[1] + 1;

                    SetToLetter();
                    break;
                case 3:
                    SetToZero();

                    M1[0] = M3[0] - 2;
                    M2[0] = M3[0] - 1;
                    M4[0] = M3[0] + 1;

                    M1[1] = M3[1];
                    M2[1] = M3[1];
                    M4[1] = M3[1];

                    SetToLetter();
                    break;

                default:
                    break;

            }
        }
        public bool BlockCollisionIBlock() // different
        {
            if (!GroundCollisionIBlock())
            {
                if (currentBlock.State == 0 || currentBlock.State == 2)
                {
                    if (simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' )
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 1)
                {
                    if (simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0')
                    {
                        return true;
                    }
                }
               
            }
            return false;
        }

        public bool GroundCollisionIBlock()// different
        {
            if (currentBlock.State == 0 || currentBlock.State == 1 || currentBlock.State == 3)
            {
                if (currentBlock.M4[1] == 19)
                {
                    return true;
                }
            }
            if(currentBlock.State == 2 )
            {
                if (currentBlock.M1[1] == 19)
                {
                    return true;
                }
                if (currentBlock.M4[1] == 19)
                {
                    return true;
                }
            }


            return false;
        }


        /*
         * 
         * 
         *              T block
         * 
         * 
         * 
         */


        public void RotateClockwiseTBlock() // different
        {
            switch (currentBlock.State)
            {
                case 1:
                    SetToZero();
                    M1[1] = M1[1] +3;
                    M2[1] = M2[1] + 3;
                    M3[1] = M3[1] + 3;
                    M4[1] = M4[1] + 3;

                    M1[0] = M1[0] + 1;
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
            if (!GroundCollisionTBlock())
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
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0')
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
            if(currentBlock.State == 3)
            {
                if (currentBlock.M4[1] == 19)
                {
                    return true;
                }
            }

            return false;
        }
        /*
         * 
         * 
         *              L block
         * 
         * 
         * 
         */


        public void RotateClockwiseLBlock() // different
        {
            switch (currentBlock.State)
            {
                case 1:
                    SetToZero();
                    M1[1] = M1[1] + 1;
                    M2[1] = M2[1] + 1;
                    M3[1] = M3[1] + 1;
                    M4[1] = M4[1] + 1;

                    M1[0] = M1[0] + 1;
                    M3[0] = M3[0] - 1;

                    M1[1] = M1[1] - 2;
                    M2[1] = M2[1] - 1;
                    M4[1] = M4[1] + 1;

                    SetToLetter();
                    break;
                case 2:
                    SetToZero();

                    M1[0] = M1[0] + 1;
                    M3[0] = M3[0] - 1;
                    M4[0] = M4[0] - 2;

                    M1[1] = M1[1] + 1;
                    M3[1] = M3[1] - 1;
                    SetToLetter();
                    break;
                case 3:
                    SetToZero();

                    M1[0] = M1[0] - 1;
                    M3[0] = M3[0] + 1;

                    M1[1] = M1[1] + 1;
                    M3[1] = M3[1] - 1;
                    M4[1] = M4[1] - 2;

                    SetToLetter();
                    break;

                default:
                    break;

            }
        }
        public bool BlockCollisionLBlock() // different
        {
            if (!GroundCollisionLBlock())
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
                    if (currentBlock.M1[1] > 6)
                    {
                        if (simBoard.currentGameBoards[currentBlock.M3[0], currentBlock.M3[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0')
                        {
                            return true;
                        }
                    }
                }
                if (currentBlock.State == 2)
                {
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M2[0], currentBlock.M2[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0')
                    {
                        return true;
                    }
                }
                if (currentBlock.State == 3)
                {
                    if (simBoard.currentGameBoards[currentBlock.M1[0], currentBlock.M1[1] + 1] != '0' || simBoard.currentGameBoards[currentBlock.M4[0], currentBlock.M4[1] + 1] != '0')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool GroundCollisionLBlock()// different
        {
            if (currentBlock.State == 0 || currentBlock.State == 3)
            {
                if (currentBlock.M1[1] == 19)
                {
                    return true;
                }
            }
            if (currentBlock.State == 1 || currentBlock.State == 2)
            {
                if (currentBlock.M4[1] == 19 || currentBlock.M3[1] == 19 || currentBlock.M1[1] == 19 || currentBlock.M2[1] == 19)
                {
                    return true;
                }
            }
            if ( currentBlock.State == 3)
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
            int original = FillGap();// count the number of holes before simulations
            int count;
            int leftNum = 0;
            int rightNum = 0;
            switch (name.FullName)
            {
                case "tetris.S_Block":
                    leftNum = 5;// each block and rotation needs it own different travsal number
                    rightNum = 4;
                    break;
                case "tetris.T_Block":
                    if(currentBlock.State == 3)
                    {
                        leftNum = 5;
                        rightNum = 5;
                    }
                    else
                    {
                        leftNum = 5;
                        rightNum = 4;
                    }
                    
                    break;
                case "tetris.I_Block":
                    if(currentBlock.State == 0)
                    {
                        leftNum = 5;
                        rightNum = 6;
                    }
                    if(currentBlock.State == 1 || currentBlock.State == 3)
                    {
                        leftNum = 4;
                        rightNum = 4;
                    }
                    if (currentBlock.State == 2)
                    {
                        leftNum = 4;
                        rightNum = 3;
                    }
                    break;
                case "tetris.L_Block":
                    if(currentBlock.State == 0 || currentBlock.State == 2 || currentBlock.State == 3)
                    {
                        leftNum = 5;
                        rightNum = 4;
                    }
                    if (currentBlock.State == 1)
                    {
                        leftNum = 5;
                        rightNum = 4;
                    }
                    break;
                case "O_Block":
                    leftNum = 5;
                    rightNum = 5;
                    break;
                case "tetris.Z_Block":
                    if(currentBlock.State == 0 || currentBlock.State == 2)
                    {
                        leftNum = 5;
                        rightNum = 4;
                    }
                    if (currentBlock.State == 1 || currentBlock.State == 3)
                    {
                        leftNum = 5;
                        rightNum = 4;
                    }

                    break;
                case "tetris.J_Block":
                    if (currentBlock.State == 0 || currentBlock.State == 2 || currentBlock.State == 3)
                    {
                        leftNum = 5;
                        rightNum = 4;
                    }
                    if (currentBlock.State == 1)
                    {
                        leftNum = 5;
                        rightNum = 4;
                    }
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
                if (name.FullName == "tetris.S_Block")
                {
                    while (!GroundCollisionSBlock() && !BlockCollisionSBlock())
                    {
                        Down();// moves as far down and then store all that information in the stuct
                    }
                }
                if (name.FullName == "tetris.T_Block")
                {
                    while (!GroundCollisionTBlock() && !BlockCollisionTBlock())
                    {
                        Down();
                    }
                }
                if (name.FullName == "tetris.I_Block")
                {
                    while (!GroundCollisionIBlock() && !BlockCollisionIBlock())
                    {
                        Down();
                    }
                }
                if (name.FullName == "tetris.L_Block")
                {
                    while (!GroundCollisionLBlock() && !BlockCollisionLBlock())
                    {
                        Down();
                    }
                }
                if (name.FullName == "O_Block")
                {
                    while (!GroundCollisionOBlock() && !BlockCollisionOBlock())
                    {
                        Down();
                    }
                }
                if (name.FullName == "tetris.Z_Block")
                {
                    while (!GroundCollisionZBlock() && !BlockCollisionZBlock())
                    {
                        Down();
                    }
                }
                if (name.FullName == "tetris.J_Block")
                {
                    while (!GroundCollisionJBlock() && !BlockCollisionJBlock())
                    {
                        Down();
                    }
                }
                moves.Add(new BestMove { HorizontalMovement = count, RotationState = currentBlock.State, HighestPoint = Highest(), ClosestToWall = DistanceToWall(currentBlock.M4[0], currentBlock.M1[0], "Right"), Direction = "Right", NumOfGaps = CountGaps(), CompleteRows = CompleteRow(), FillHole = FillGapCheck(original) });
                // list of moves
       


                if (currentBlock.State == 5)
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
                if (name.FullName == "tetris.I_Block")
                {
                    while (!GroundCollisionIBlock() && !BlockCollisionIBlock())
                    {
                        Down();
                    }
                }
                if (name.FullName == "tetris.L_Block")
                {
                    while (!GroundCollisionLBlock() && !BlockCollisionLBlock())
                    {
                        Down();
                    }
                }
                if (name.FullName == "O_Block")
                {
                    while (!GroundCollisionOBlock() && !BlockCollisionOBlock())
                    {
                        Down();
                    }
                }
                if (name.FullName == "tetris.Z_Block")
                {
                    while (!GroundCollisionZBlock() && !BlockCollisionZBlock())
                    {
                        Down();
                    }
                }
                if (name.FullName == "tetris.J_Block")
                {
                    while (!GroundCollisionJBlock() && !BlockCollisionJBlock())
                    {
                        Down();
                    }
                }
                moves.Add(new BestMove { HorizontalMovement = count, RotationState = currentBlock.State, HighestPoint = Highest(), ClosestToWall = DistanceToWall(currentBlock.M4[0], currentBlock.M1[0], "Left"), Direction = "Left", NumOfGaps = CountGaps(), CompleteRows = CompleteRow(), FillHole = FillGapCheck(original) });
                if (currentBlock.State == 5)
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
            highestRow = 0;
            for (int j = 0; j < 20; j++)
            {
                if (breakloop == false)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (simBoard.currentGameBoards[i, j] != '0' && j != 0 && j != 1 && j != 2 && j != 3)// so blocks generating don't cause a false positive
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
        public int DistanceToWall(int DM4, int DM1, string Dir)// places block closest to the wall first 
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
                if (CurrentRow + 1 < 19)
                {
                    if (simBoard.currentGameBoards[i, CurrentRow + 1] == '0')
                    {
                        count++;
                    }
                }

            }

            return count;
        }
        
        public bool CompleteRow()// if block complete a row play this
        {
            int LineCount = 0;
            
            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (simBoard.currentGameBoards[i, j] != '0')
                    {
                        LineCount++;
                    }
                }
                if (LineCount == 10)
                {

                    return true;
                }
                LineCount = 0;
            }
            return false;
        }

        public int FillGap()// if it fills in a gap
        {
           
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 19; j++)
                {

                    if (simBoard.currentGameBoards[i, j] == '0')
                    {
                        if (i + 1 < 10 && i - 1 > 0)
                        {
                            if (simBoard.currentGameBoards[i + 1, j] != '0' && simBoard.currentGameBoards[i - 1, j] != '0')
                            {
                                count++;
                            }
                        }

                        if (i == 9)
                        {
                            if (simBoard.currentGameBoards[i, j] == '0' && simBoard.currentGameBoards[i - 1, j] != '0')
                            {
                                count++;
                            }
                        }
                        if (i == 0)
                        {
                            if (simBoard.currentGameBoards[i, j] == '0' && simBoard.currentGameBoards[i + 1, j] != '0')
                            {
                                count++;
                            }
                        }

                    }
                    
                }


            }
            return count;
        }
        public bool FillGapCheck(int original)
        {
            if(original > FillGap())
            {
                return true;
            }
            return false;
        }
        
     
        public int Best()
        {
            List<BestMove> HPointList = new List<BestMove>();// a list of the Lowest down block
            List<BestMove> GapList = new List<BestMove>();// list of the moves with the fewest number of gaps created
            List<BestMove> MovesToRemove = new List<BestMove>();
            BestMove TopGap = new BestMove();
            
            HPoint = moves.OrderByDescending(Top => Top.HighestPoint).FirstOrDefault(); //highest point on the board
            foreach (BestMove move in moves)
            {
                if (move.CompleteRows == true)
                {
                    HPointList.Add(move);
                    
                    HPoint = move;
                    
                }

            }
            if(currentBlock.CurrentLetter == 'j')// fills gaps giving false positives so needs to be removed
            {
                foreach (BestMove move in moves)
                {
                    if (move.RotationState == 1 )
                    {
                       MovesToRemove.Add(move);
                    }

                }
            }
            if (currentBlock.CurrentLetter == 'l')
            {
                foreach (BestMove move in moves)
                {
                    if (move.RotationState == 3)
                    {
                        MovesToRemove.Add(move);
                    }

                }
            }
            foreach (BestMove moveToRem in MovesToRemove)
            {
                HPointList.Remove(moveToRem);
            }

            if (currentBlock.CurrentLetter == 'i')
            {

                foreach (BestMove move in HPointList)// filles in a gap for i blocks
                {

                    if (move.FillHole == true)
                    {
                        HPointList.Add(move);

                        HPoint = move;
                        Debug.WriteLine(" HM " + HPoint.HorizontalMovement);
                        Debug.WriteLine(" HP " + HPoint.HighestPoint);
                        Debug.WriteLine(" RS " + HPoint.RotationState);
                        HPointList.Clear();
                        moves.Clear();
                        GapList.Clear();
                        return HPoint.HorizontalMovement;
                    }

                }
            }
            //HPoint = HPointList.OrderByDescending(Top => Top.HighestPoint).FirstOrDefault();
            if (HPoint.CompleteRows == true)// checks if a completed row is found and plays that
            {
               
                HPoint = HPointList.OrderByDescending(Top => Top.HighestPoint).FirstOrDefault();
                Debug.WriteLine(" HM " + HPoint.HorizontalMovement);
                Debug.WriteLine(" HP " + HPoint.HighestPoint);
                Debug.WriteLine(" RS " + HPoint.RotationState);
                HPointList.Clear();
                moves.Clear();
                GapList.Clear();
                return HPoint.HorizontalMovement;
            }
            foreach (BestMove bestMove in moves)
            {
                
                if (HPoint.HighestPoint == bestMove.HighestPoint)
                {
                    HPointList.Add(bestMove);// makes a list of all lowest moves 
                }
                
            }
            TopGap = HPointList.OrderBy(Gap => Gap.NumOfGaps).FirstOrDefault();
            foreach (BestMove bestMove in HPointList)
            {
                if (TopGap.NumOfGaps == bestMove.NumOfGaps)
                {
                    GapList.Add(bestMove);// sort to find the move with the fewest gaps
                }
            }
            HPoint = GapList.OrderBy(Top => Top.ClosestToWall).FirstOrDefault();

            Debug.WriteLine(" current letter: " + currentBlock.CurrentLetter);
            Debug.WriteLine(" HM " + HPoint.HorizontalMovement);
            Debug.WriteLine(" HP " + HPoint.HighestPoint);
            Debug.WriteLine(" RS " + HPoint.RotationState);
            
            HPointList.Clear();
            moves.Clear();
            GapList.Clear();
            return HPoint.HorizontalMovement;
        }


    }
}