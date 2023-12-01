using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace tetris
{
    internal class Blocks : Board 
    {
        public bool work = true;
        Random random = new Random();
        Board newBoard = new Board();
        public Queue<int> upComingBlocks = new Queue<int>(4);
        public Blocks() 
        {

            


        }

        public void setBlock()
        {
            
            newBoard.currentGameBoards.SetValue('o', 4, 0);
            newBoard.currentGameBoards.SetValue('o', 5, 0);
            newBoard.currentGameBoards.SetValue('o', 4, 1);
            newBoard.currentGameBoards.SetValue('o', 4, 2);
            bool queueFull = false;
            while (!queueFull)
            {
                int nextTetroimino = random.Next(0, 4);

                upComingBlocks.Enqueue(nextTetroimino);
                if (upComingBlocks.Count == 4)
                {
                    queueFull = true;
                }
            }
            //int currrent = upComingBlocks.Dequeue();
            int current = 0;
            switch (current)
            {
                case 0:

                    newBoard.currentGameBoards.SetValue('o', 4, 0);
                    newBoard.currentGameBoards.SetValue('o', 5, 0);
                    newBoard.currentGameBoards.SetValue('o', 4, 1);
                    newBoard.currentGameBoards.SetValue('o', 4, 2);

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
