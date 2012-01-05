using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    class Export
    {
        /* Parsers */

        public static string ParseDataFromBoard(AbstractBoard board)
        {
            StringBuilder text = new StringBuilder();

            for (int i = 0; i != board.GetBoardSize(); i++)
            {
                // Add newline separators after the first line
                if (i > 0)
                    text.Append(Environment.NewLine);

                for (int j = 0; j != board.GetBoardSize(); j++)
                {
                    // Add comma separators after the first number
                    if (j > 0)
                        text.Append(",");

                    // Add non-blank numbers
                    if (!board.IsNumberBlank(i, j))
                    {
                        text.Append(board.GetNumber(i, j));
                        text.Append(board.IsNumberPredefined(i, j) ? "T" : "F");
                    }
                }

                
            }

            return text.ToString();
        }
    }
}
