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


public class O_Block: Blocks
{
    
    /*int[] L1 = new int[] { 4, 0 };
    int[] L2 = new int[] { 5, 0 };
    int[] R1 = new int[] { 4, 1 };
    int[] R2 = new int[] { 5, 1 };*/

    /* 
     *          L1  L2
     *          
     *          R1  R2
     * O block
     */
    
    
    public O_Block()
    {
        M1 = new int[] { 4, 0 };
        M2 = new int[] { 5, 0 };
        M3 = new int[] { 4, 1 };
        M4 = new int[] { 5, 1 };
        CurrentLetter = 'o';
    }
    
    public override bool GroundCollision()
    {
        BottomRow = M4[1];
        BottomColumn = M4[0];
        //CharacterBottom = board.currentGameBoards[BottomColumn, BottomRow];
        PB = board.currentGameBoards;
        
        return base.GroundCollision();
    }
    public override bool RWallCollision()
    {
        BottomRow = M4[1]+1;
        BottomColumn = M4[0]+1;
        PB = board.currentGameBoards;
        return base.RWallCollision();
    }
    public override bool LWallCollision()
    {
        BottomRow = M3[1]-1;
        BottomColumn = M3[0]-1; 
        PB = board.currentGameBoards;
        return base.LWallCollision();
    }
    public override bool BlockCollision()
    {
        BottomRow = M3[1];
        BottomColumn = M3[0];
        RRow = M4[1];
        RColumn = M4[0];
        //Debug.Write(M3[1].ToString()+" "+ M3[0].ToString()+ " "+ M4[1].ToString()+ " " + M4[0].ToString());
        PB = board.GetBoard();
        return base.BlockCollision();
    }
    
}

