using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    public class I_Block : Blocks
    {
        public Board board { get; set; }
        int[] M1 = new int[] {4, 0};
        int[] M2 = new int[] {4, 1};
        int[] M3 = new int[] {4, 2};
        int[] M4 = new int[] {4, 3};
        public override void StarPosition()
        {
            board.ChangeBoard(M1[0], M1[1], 'i');
            board.ChangeBoard(M2[0], M2[1], 'i');
            board.ChangeBoard(M3[0], M3[1], 'i');
            board.ChangeBoard(M4[0], M4[1], 'i');
        }
    }
}
