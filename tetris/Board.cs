using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using tetris;

public class Board
{
    public char[,] currentGameBoards;
    public char Row;
    public char Col;
    

    public Board()
	{       
        currentGameBoards = new char[10, 20];
        
    }
    public void blankBoard()
    {
        currentGameBoards = new char[10,20];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                currentGameBoards[i, j] = '0';
            }
        }
    }
    public void ChangeBoard(int row, int col, char val)
    {
        if(row <=9 && col <=19) 
        {
            currentGameBoards[row, col] = val;
        }
        
    }
    
    public char[,] GetBoard()
    {

        return currentGameBoards;
    }
    public char GetCurrentPos(int col, int row)
    {
        return currentGameBoards[col, row];
    }
  

}
