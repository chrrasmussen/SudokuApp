using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    class NineByNineBoard : AbstractBoard
    {
        public override int GetBoardSize()
        {
            return 9;
        }

        public override int GetBlockWidth()
        {
            return 3;
        }

        public override int GetBlockHeight()
        {
            return 3;
        }
    }
}
