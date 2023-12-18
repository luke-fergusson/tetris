using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tetris;

public class Blocks 
{
    public Board board { get; set; }
    public int BottomRow;
    public int BottomColumn;
    public char CharacterBottom;
    public virtual void Down()
    {
        
    }
    public virtual void StarPosition() 
    {
       
    }   
    public Blocks() 
    {
        board = new Board();
    }
    public virtual void Left()
    {

    }
    public virtual void Right()
    {

    }
    public virtual bool GroundCollision()
    {
        if (BottomRow == 19)
        {
            return true;
        }
        return false;
    }
    public virtual bool RWallCollision()
    {
        if (BottomColumn == 9)
        {
            return true;
        }
        /*if(CharacterBottom != '0')
        {
            return true;
        }*/
        
        return false;
    }
    public virtual bool LWallCollision()
    {
        if (BottomColumn == 0)
        {
            return true;
        }
        return false;
    }







    /*public bool work = true;
    Random random = new Random();
    Board newBoard = new Board();
    public Queue<int> upComingBlocks = new Queue<int>(4);*/
    /*
    public void setBlock()
    {
        newBoard.initalGameBoard();
        newBoard.currentGameBoards.SetValue('o', 4, 0);
        newBoard.currentGameBoards.SetValue('o', 5, 0);
        newBoard.currentGameBoards.SetValue('o', 4, 1);
        newBoard.currentGameBoards.SetValue('o', 4, 2);
        Debug.WriteLine("working");
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
    }*/

}

