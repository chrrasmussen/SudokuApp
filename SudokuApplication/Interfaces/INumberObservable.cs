using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    interface INumberObserverable
    {
        void AddNumberObserver(INumberObserver observer);
        void RemoveNumberObserver(INumberObserver observer);
        void NotifyNumberObservers(Coordinate cell);
    }
}
