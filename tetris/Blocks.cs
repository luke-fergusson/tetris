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
    public char[,] PB;
    public int State;

    public virtual void Down()
    {
        
    }
    public virtual void StarPosition() 
    {
       
    }   
    public Blocks() 
    {
        board = new Board();
        PB = new char[10, 20];
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
    public virtual bool BlockCollision()
    {
        if (!GroundCollision())
        {
            Debug.WriteLine(PB[BottomColumn, BottomRow + 1]);
            if (PB[BottomColumn, BottomRow+1] == 'i')
            {
                Debug.WriteLine("worked");
                return true;
            }
            /*if (BottomColumn +1 < 10 && PB[BottomColumn +1, BottomRow+1 ] == 'i')
            {
                Debug.WriteLine("hit");
                return true;
            }
            if (BottomColumn - 1 >= 0 && PB[BottomColumn-1, BottomRow + 1] == 'i' )
            {
                Debug.WriteLine("done");
                return true;
            }*/
            return false;
        }
        return false;
    }
    public virtual void RotateClockwise()
    {

    }



}

