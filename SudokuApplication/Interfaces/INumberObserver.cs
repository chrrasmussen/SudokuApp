using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    interface INumberObserver
    {
        void UpdateNumber(Coordinate cell);
    }
}
