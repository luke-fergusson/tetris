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
    }
    public void OBlock()
    {
        currentGameBoards[4, 0] = 'o';
    }
}
