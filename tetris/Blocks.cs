using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace tetris
{
    internal class Blocks : Board
    {
        Random random = new Random();
        Board newBoard = new Board();
        public Queue<int> upComingBlocks = new Queue<int>(4);
        public Blocks() 
        {
            bool queueFull = false;
            while (!queueFull)
            {
                int current = random.Next(0, 4);

                upComingBlocks.Enqueue(current);
                if(upComingBlocks.Count == 4)
                {
                    queueFull = true;
                }
            }
            int currrent = upComingBlocks.Dequeue();
            switch (currrent) 
            { 
                case 0:
                    newBoard.currentGameBoards.SetValue('i', 4);
                    newBoard.currentGameBoards.SetValue('i', 5);
                    
                    break;
                case 1:

                    break; 
                case 2:

                    break; 
                case 3:

                    break;
                case 4:

                    break;
                default: break; 
            }
            
        }

    }
}
