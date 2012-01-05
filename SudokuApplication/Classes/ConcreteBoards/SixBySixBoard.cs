using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    class SixBySixBoard : AbstractBoard
    {
        public override int GetBoardSize()
        {
            return 6;
        }

        public override int GetBlockWidth()
        {
            return 3;
        }

        public override int GetBlockHeight()
        {
            return 2;
        }
    }
}
