using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    class Import
    {
        /* Parsers */

        public static AbstractBoard ParseDataIntoBoard(string boardData)
        {
            // Split file into lines and create a board that matches
            string[] boardLines = boardData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            // Create a suitable board
            AbstractBoard board = BoardFactory.CreateBoard(boardLines.Length);

            for (int i = 0; i < boardLines.Length; i++)
            {
                string currentLine = boardLines[i];
                string[] cells = currentLine.Split(new string[] { "," }, StringSplitOptions.None);

                for (int j = 0; j < cells.Length; j++)
                {
                    string currentCell = cells[j];
                    if (currentCell.Length >= 2)
                    {
                        // Read values from current cell
                        int value = Convert.ToInt32(currentCell.Substring(0, currentCell.Length - 1));
                        bool predefined = (currentCell[currentCell.Length - 1] == 'T') ? true : false;

                        // Update cells in board
                        board.SetNumber(i, j, value, predefined);
                    }
                }
            }

            return board;
        }
    }
}
