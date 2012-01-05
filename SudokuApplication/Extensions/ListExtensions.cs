using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    public static class ListExtensions
    {
        public static List<int> GenerateAllPossibleValues(this List<int> list, int size)
        {
            for (int i = 1; i <= size; i++)
            {
                list.Add(i);
            }

            return list;
        }
    }
}
