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
    
    public override void Down()
    {

        /*board.ChangeBoard(L1[0], L1[1], '0');
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
        board.ChangeBoard(R2[0], R2[1], 'o');*/

        /*board.ChangeBoard(M1[0], M1[1], '0');
        board.ChangeBoard(M2[0], M2[1], '0');
        board.ChangeBoard(M3[0], M3[1], '0');
        board.ChangeBoard(M4[0], M4[1], '0');
        M1[1] = M1[1] + 1;
        M2[1] = M2[1] + 1;
        M3[1] = M3[1] + 1;
        M4[1] = M4[1] + 1;*/
        base.Down();
        /*board.ChangeBoard(M1[0], M1[1], 'o');
        board.ChangeBoard(M2[0], M2[1], 'o');
        board.ChangeBoard(M3[0], M3[1], 'o');
        board.ChangeBoard(M4[0], M4[1], 'o');*/

    }
    public O_Block()
    {
        M1 = new int[] { 4, 0 };
        M2 = new int[] { 5, 0 };
        M3 = new int[] { 4, 1 };
        M4 = new int[] { 5, 1 };
        CurrentLetter = 'o';
    }
    public override void StarPosition()
    {
        /*board.ChangeBoard(L1[0], L1[1], 'o');
        board.ChangeBoard(L2[0], L2[1], 'o');
        board.ChangeBoard(R1[0], R1[1], 'o');
        board.ChangeBoard(R2[0], R2[1], 'o');*/

        /*board.ChangeBoard(M1[0], M1[1], 'o');
        board.ChangeBoard(M2[0], M2[1], 'o');
        board.ChangeBoard(M3[0], M3[1], 'o');
        board.ChangeBoard(M4[0], M4[1], 'o');*/

        base.StarPosition();

    }
    public override void Left()
    {
        /*board.ChangeBoard(L1[0], L1[1], '0');
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
        board.ChangeBoard(R2[0], R2[1], 'o');*/

        /*board.ChangeBoard(M1[0], M1[1], '0');
        board.ChangeBoard(M2[0], M2[1], '0');
        board.ChangeBoard(M3[0], M3[1], '0');
        board.ChangeBoard(M4[0], M4[1], '0');
        M1[0] = M1[0] - 1;
        M2[0] = M2[0] - 1;
        M3[0] = M3[0] - 1;
        M4[0] = M4[0] - 1;
        board.ChangeBoard(M1[0], M1[1], 'o');
        board.ChangeBoard(M2[0], M2[1], 'o');
        board.ChangeBoard(M3[0], M3[1], 'o');
        board.ChangeBoard(M4[0], M4[1], 'o');*/
        base.Left();
    }
    public override void Right() 
    {
        /*board.ChangeBoard(L1[0], L1[1], '0');
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
        board.ChangeBoard(R2[0], R2[1], 'o');*/

        /*board.ChangeBoard(M1[0], M1[1], '0');
        board.ChangeBoard(M2[0], M2[1], '0');
        board.ChangeBoard(M3[0], M3[1], '0');
        board.ChangeBoard(M4[0], M4[1], '0');
        M1[0] = M1[0] + 1;
        M2[0] = M2[0] + 1;
        M3[0] = M3[0] + 1;
        M4[0] = M4[0] + 1;
        board.ChangeBoard(M1[0], M1[1], 'o');
        board.ChangeBoard(M2[0], M2[1], 'o');
        board.ChangeBoard(M3[0], M3[1], 'o');
        board.ChangeBoard(M4[0], M4[1], 'o');*/
        base.Right();
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
        BottomRow = M4[1];
        BottomColumn = M4[0];
        PB = board.currentGameBoards;
        return base.RWallCollision();
    }
    public override bool LWallCollision()
    {
        BottomRow = M3[1];
        BottomColumn = M3[0]; 
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

