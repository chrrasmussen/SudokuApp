using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    class FourByFourBoard : AbstractBoard
    {
        public override int GetBoardSize()
        {
            return 4;
        }

        public override int GetBlockWidth()
        {
            return 2;
        }

        public override int GetBlockHeight()
        {
            return 2;
        }
    }
}
