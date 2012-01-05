using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    class TwelveByTwelveBoard : AbstractBoard
    {
        public override int GetBoardSize()
        {
            return 12;
        }

        public override int GetBlockWidth()
        {
            return 4;
        }

        public override int GetBlockHeight()
        {
            return 3;
        }
    }
}
