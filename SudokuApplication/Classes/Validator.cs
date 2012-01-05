using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace SudokuApplication
{
    class Validator
    {
        /* Validate methods */

        public static bool ValidateBoard(AbstractBoard board, bool completeCheck = false)
        {
            bool validation = true;

            for (int i = 0; i != board.GetBoardSize(); i++)
            {
                // Validate row, column and block
                validation &= ValidateSection(board, board.GetRowCells(i), completeCheck);
                validation &= ValidateSection(board, board.GetColumnCells(i), completeCheck);
                validation &= ValidateSection(board, board.GetBlockCells(i), completeCheck);
            }

            return validation;
        }

        public static bool ValidateCell(AbstractBoard board, int row, int column, bool oneSectionIsCompleted = false)
        {
            // Validate row, column and block
            bool rowValidation = ValidateSection(board, board.GetRowCells(row), oneSectionIsCompleted);
            bool columnValidation = ValidateSection(board, board.GetColumnCells(column), oneSectionIsCompleted);
            bool blockValidation = ValidateSection(board, board.GetBlockCells(row, column), oneSectionIsCompleted);

            // Combine result
            bool andResult = (rowValidation && columnValidation && blockValidation);
            bool orResult = (rowValidation || columnValidation || blockValidation);

            return (!oneSectionIsCompleted) ? andResult : orResult;
        }

        public static bool ValidateSection(AbstractBoard board, IEnumerable<Coordinate> cells, bool completeCheck = false)
        {
            HashSet<int> previousValues = new HashSet<int>();
            foreach (Coordinate cell in cells)
            {
                // Get current value
                int value = board.GetNumber(cell.Row, cell.Column);

                // Check if the current value is blank
                if (board.IsNumberBlank(cell.Row, cell.Column))
                {
                    // For a complete check; none of the numbers are allowed to be blank
                    // Otherwise skip to next cell
                    if (completeCheck)
                        return false;
                    else
                        continue;
                }

                // Check if the current value is duplicated
                if (previousValues.Contains(value))
                    return false;

                // Store the current value
                previousValues.Add(value);
            }

            // No duplicates
            return true;
        }
    }
}
