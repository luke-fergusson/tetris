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
    
    int[] L1 = new int[] { 4, 0 };
    int[] L2 = new int[] { 5, 0 };
    int[] R1 = new int[] { 4, 1 };
    int[] R2 = new int[] { 5, 1 };

    /* 
     *          L1  L2
     *          
     *          R1  R2
     * O block
     */
    
    public override void Down()
    {

        board.ChangeBoard(L1[0], L1[1], '0');
        board.ChangeBoard(L2[0], L2[1], '0');
        board.ChangeBoard(R1[0], R1[1], '0');
        board.ChangeBoard(R2[0], R2[1], '0');
        L1[1] = L1[1] + 1;
        L2[1] = L2[1] + 1;
        R1[1] = R1[1] + 1;
        R2[1] = R2[1] + 1;
        board.ChangeBoard(L1[0], L1[1], 'o');
        board.ChangeBoard(L2[0], L2[1], 'o');
        board.ChangeBoard(R1[0], R1[1], 'o');
        board.ChangeBoard(R2[0], R2[1], 'o');
        
    }
    public O_Block()
    {
        
    }
    public override void StarPosition()
    {
        board.ChangeBoard(L1[0], L1[1], 'o');
        board.ChangeBoard(L2[0], L2[1], 'o');
        board.ChangeBoard(R1[0], R1[1], 'o');
        board.ChangeBoard(R2[0], R2[1], 'o');
        
    }
    public override void Left()
    {
        board.ChangeBoard(L1[0], L1[1], '0');
        board.ChangeBoard(L2[0], L2[1], '0');
        board.ChangeBoard(R1[0], R1[1], '0');
        board.ChangeBoard(R2[0], R2[1], '0');
        L1[0] = L1[0] - 1;
        L2[0] = L2[0] - 1;
        R1[0] = R1[0] - 1;
        R2[0] = R2[0] - 1;
        board.ChangeBoard(L1[0], L1[1], 'o');
        board.ChangeBoard(L2[0], L2[1], 'o');
        board.ChangeBoard(R1[0], R1[1], 'o');
        board.ChangeBoard(R2[0], R2[1], 'o');
    }
    public override void Right() 
    {
        board.ChangeBoard(L1[0], L1[1], '0');
        board.ChangeBoard(L2[0], L2[1], '0');
        board.ChangeBoard(R1[0], R1[1], '0');
        board.ChangeBoard(R2[0], R2[1], '0');
        L1[0] = L1[0] + 1;
        L2[0] = L2[0] + 1;
        R1[0] = R1[0] + 1;
        R2[0] = R2[0] + 1;
        board.ChangeBoard(L1[0], L1[1], 'o');
        board.ChangeBoard(L2[0], L2[1], 'o');
        board.ChangeBoard(R1[0], R1[1], 'o');
        board.ChangeBoard(R2[0], R2[1], 'o');
    }
    public override bool GroundCollision()
    {
        BottomRow = R1[1];
        BottomColumn = R1[0];
        //CharacterBottom = board.currentGameBoards[BottomColumn, BottomRow];
        
        return base.GroundCollision();
    }
    public override bool RWallCollision()
    {
        BottomRow = R2[1];
        BottomColumn = R2[0];
        return base.RWallCollision();
    }
    public override bool LWallCollision()
    {
        BottomRow = R1[1];
        BottomColumn = R1[0];
        return base.LWallCollision();
    }
}

