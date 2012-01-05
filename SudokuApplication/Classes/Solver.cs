using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    // Description from: http://www.gwerdy.com/products/sudoku_solver/
    class Solver
    {
        /* Fields */

        private AbstractBoard _board;
        List<int>[,] possibleValues;


        /* Constructors */

        public Solver(AbstractBoard board)
        {
            _board = board;
        }


        /* Solver */

        /// <summary>
        /// The SuDoku Solver's algorithm only uses logical arguments to solve the puzzles.
        /// It does not use 'trial and error' methods. For anybody interested in an explanation
        /// of the algorithm it employs to solve your SuDoku then please read on. The Solver
        /// stores two grids (as arrays), one is the actual SuDoku grid itself, and the second
        /// contains all the 'possible values' for each square. Initially all values (1-9) would
        /// be possible for all squares of course. The algorithm then tries a number of steps to
        /// solve the puzzle, stopping when it either solves the SuDoku or all the following steps
        /// fail to find any new values. 
        /// </summary>
        public bool SolveBoard()
        {
            // Initialize fields
            possibleValues = new List<int>[_board.GetBoardSize(), _board.GetBoardSize()];

            // Create a list of all possible values
            for (int i = 0; i < _board.GetBoardSize(); i++)
            {
                for (int j = 0; j < _board.GetBoardSize(); j++)
                {
                    if (_board.IsNumberBlank(i, j))
                        possibleValues[i, j] = new List<int>().GenerateAllPossibleValues(_board.GetBoardSize());
                    else
                        possibleValues[i, j] = new List<int>();
                }
            }


            // Find new numbers until the board is solved
            while (true)
            {
                // Perform step 1, noting any new numbers
                int foundNumbers = SolverStep1();
                if (Validator.ValidateBoard(_board, true))
                    return true;

                // Perform step 2, noting any new numbers
                foundNumbers += SolverStep2();
                if (Validator.ValidateBoard(_board, true))
                    return true;

                // If neither of step 1 nor 2 finds a number, move to step 3 and 4
                if (foundNumbers == 0)
                {
                    // Perform step 3, noting any changes to possible values
                    bool foundInvalidValues = SolverStep3();

                    // Perform step 4, noting any changes to possible values
                    foundInvalidValues |= SolverStep4();

                    // If no new information is found, try brute force
                    if (!foundInvalidValues)
                    {
                        return BruteForce();
                    }
                }
            }
        }

        /* Helper methods */

        public override string ToString()
        {
            return String.Format("<{0}> Board:{1}", GetType(), _board);
        }


        /// <summary>
        /// Step 1: Only one valid value for a square
        /// 
        /// SuDoku Solver checks to see if any of the squares have only one possible value.
        /// If any are found then the value is entered for this square as mentioned above and
        /// Step 1 is repeated until all remaining unsolved squares contain multiple possible
        /// values (or the SuDoku is completely solved!)
        /// </summary>
        private int SolverStep1()
        {
            int foundNumbers = 0;

            // Loop through algorithm until there is no more changes
            bool changes;
            do
            {
                changes = false;

                // Check each empty cell on the board
                for (int i = 0; i < _board.GetBoardSize(); i++)
                {
                    for (int j = 0; j < _board.GetBoardSize(); j++)
                    {
                        if (_board.IsNumberBlank(i, j))
                        {
                            // There may only be one possible value (because of step 3 and 4),
                            // which means that we don't need to run this function
                            if (possibleValues[i, j].Count > 1)
                                RemoveInvalidValuesFromCell(ref possibleValues[i, j], i, j);

                            // If there is only one possible number; insert into board,
                            // remove from possible values and update changes
                            if (possibleValues[i, j].Count == 1)
                            {
                                _board.SetNumber(i, j, possibleValues[i, j][0]);
                                possibleValues[i, j].RemoveAt(0);
                                changes = true;
                                foundNumbers++;
                            }
                        }
                    }
                }
            }
            while (changes == true);

            return foundNumbers;
        }

        private void RemoveInvalidValuesFromCell(ref List<int> possibleValuesInCell, int row, int column)
        {
            // Remove possible values for each section
            RemoveInvalidValuesFromCellForSection(ref possibleValuesInCell, _board.GetRowCells(row));
            RemoveInvalidValuesFromCellForSection(ref possibleValuesInCell, _board.GetColumnCells(column));
            RemoveInvalidValuesFromCellForSection(ref possibleValuesInCell, _board.GetBlockCells(row, column));
        }

        private void RemoveInvalidValuesFromCellForSection(ref List<int> possibleValuesInCell, IEnumerable<Coordinate> cells)
        {
            foreach (Coordinate cell in cells)
            {
                int value = _board.GetNumber(cell.Row, cell.Column);
                possibleValuesInCell.Remove(value);
            }
        }

        /// <summary>
        /// Step 2: Only one valid square for a value in a row / column / block
        /// 
        /// If the above method fails to solve the puzzle then this next method is tested.
        /// Each row / line / block is checked to see if any of the values (1-9) that have
        /// yet to be found for that row / column / block can only be placed in a single square.
        /// If so that value is entered and the algorithm returns to Step 1. 
        /// 
        /// If we still have not solved the puzzle at this point the solver moves onto the
        /// following methods which do not directly try to find the value for a square but
        /// rather reduce the possible values for a square in the hope this leads to Step 1
        /// or 2 being able to then solve another square. 
        /// </summary>
        private int SolverStep2()
        {
            int foundNumbers = 0;

            // Loop through each section
            for (int i = 0; i != _board.GetBoardSize(); i++)
            {
                foundNumbers += SearchSectionForValuesThatOnlyFitInOneCell(_board.GetRowCells(i));
                foundNumbers += SearchSectionForValuesThatOnlyFitInOneCell(_board.GetColumnCells(i));
                foundNumbers += SearchSectionForValuesThatOnlyFitInOneCell(_board.GetBlockCells(i));
            }

            return foundNumbers;
        }

        private int SearchSectionForValuesThatOnlyFitInOneCell(IEnumerable<Coordinate> cells)
        {
            int foundNumbers = 0;

            for (int i = 0; i != _board.GetBoardSize(); i++)
            {
                // Create a list of all possible values and remove the values in this section
                List<int> missingValues = new List<int>().GenerateAllPossibleValues(_board.GetBoardSize());
                RemoveInvalidValuesFromCellForSection(ref missingValues, cells);

                // Loop through each missing value
                foreach (int value in missingValues)
                {
                    // Count the number of locations the current value is allowed to be placed
                    int numberOfPossibleCells = 0;
                    Coordinate lastMatchingCell = null;
                    foreach (Coordinate cell in cells)
                    {
                        if (possibleValues[cell.Row, cell.Column].Contains(value) == true)
                        {
                            numberOfPossibleCells++;
                            lastMatchingCell = cell;
                        }
                    }

                    // If the value is only allowed to be placed at one location; insert the number,
                    // remove possible values from cell and flag new changes
                    if (numberOfPossibleCells == 1)
                    {
                        _board.SetNumber(lastMatchingCell.Row, lastMatchingCell.Column, value);
                        possibleValues[lastMatchingCell.Row, lastMatchingCell.Column].Clear();
                        foundNumbers++;

                        // Step 1 will update the possible values in the affected cells
                    }
                }
            }

            return foundNumbers;
        }

        /// <summary>
        /// Step 3: Value must be here, so can not be there!
        /// 
        /// This one is a bit more complex to explain, if you look in a 3x3 block and all
        /// the places a certain value can be put fall on the same column / row then the
        /// value must lie within these squares so can not possibly lie in the other six
        /// squares in this column / row. The argument can also be used the opposite way
        /// around, looking at lines where all allowed locations are within the same 3x3
        /// block. The following images may make this clearer. 
        /// </summary> 
        private bool SolverStep3()
        {
            bool changes = false;
            for (int i = 0; i != _board.GetBoardSize(); i++)
            {
                changes |= SearchRowForValuesThatOnlyFitInOneBlock(i);
                changes |= SearchColumnForValuesThatOnlyFitInOneBlock(i);
            }
            return changes;
        }

        private bool SearchRowForValuesThatOnlyFitInOneBlock(int row)
        {
            List<int> possibleValuesInRowInBlock;
            bool changes = false;
            for (int i = 0; i != _board.GetBoardSize(); i += _board.GetBlockWidth())
            {
                possibleValuesInRowInBlock = GetPossibleValuesInSectionInBlock(_board.GetRowCells(row), i, _board.GetBlockWidth());

                foreach (int value in possibleValuesInRowInBlock)
                {
                    //Sjekker om det finnes verdier i raden som bare kan puttes i den gitte blokken, og fjerner eventuelt verdiene i blokken som ikke ligger på samme rad
                    bool uniqueValue = CheckIfPossibleValueOnlyExistsInBlockInSelection(_board.GetRowCells(row), value, i, _board.GetBlockWidth());
                    if (uniqueValue == true)
                    {
                        for (int j = 0; j < _board.GetBoardSize(); j++)
                        {
                            if ((row != GetRowFromBlockPosition(row, j)) && (possibleValues[GetRowFromBlockPosition(row, j), GetColumnFromBlockPosition(i, j)].Contains(value) == true))
                            {
                                possibleValues[GetRowFromBlockPosition(row, j), GetColumnFromBlockPosition(i, j)].Remove(value);
                                changes = true;
                            }
                        }
                    }
                    //Sjekker om det finnes verdier i blokken som bare kan puttes i den gitte raden, og fjerner eventuelt verdiene i raden som ikke ligger i samme blokk
                    uniqueValue = CheckIfPossibleValueOnlyExistsInRowInBlock(_board.GetBlockCells(row, i), value, _board.GetBlockWidth(), row);
                    if (uniqueValue == true)
                    {
                        for (int j = 0; j != _board.GetBoardSize(); j++)
                        {
                            if (((j < i) || (j >= (i + _board.GetBlockWidth()))) && (possibleValues[row, j].Contains(value) == true))
                            {
                                possibleValues[row, j].Remove(value);
                                changes = true;
                            }
                        }
                    }
                }

            }
            return changes;
        }

        private bool SearchColumnForValuesThatOnlyFitInOneBlock(int column)
        {
            List<int> possibleValuesInColumnInBlock;
            bool changes = false;
            for (int i = 0; i != _board.GetBoardSize(); i += _board.GetBlockHeight())
            {
                possibleValuesInColumnInBlock = GetPossibleValuesInSectionInBlock(_board.GetColumnCells(column), i, _board.GetBlockHeight());

                foreach (int value in possibleValuesInColumnInBlock)
                {
                    //Sjekker om det finnes verdier i kolonnen som bare kan puttes i den gitte blokken, og fjerner eventuelt verdiene i blokken som ikke ligger på samme kolonne
                    bool uniqueValue = CheckIfPossibleValueOnlyExistsInBlockInSelection(_board.GetColumnCells(column), value, i, _board.GetBlockWidth());
                    if (uniqueValue == true)
                    {
                        for (int j = 0; j != _board.GetBoardSize(); j++)
                        {
                            if ((column != GetColumnFromBlockPosition(column, j)) && (possibleValues[GetRowFromBlockPosition(i, j), GetColumnFromBlockPosition(column, j)].Contains(value) == true))
                            {
                                possibleValues[GetRowFromBlockPosition(i, j), GetColumnFromBlockPosition(column, j)].Remove(value);
                                changes = true;
                            }
                        }
                    }
                    //Sjekker om det finnes verdier i blokken som bare kan puttes i den gitte kolonnen, og fjerner eventuelt verdiene i kolonnen som ikke ligger i samme blokk
                    uniqueValue = CheckIfPossibleValueOnlyExistsInColumnInBlock(_board.GetBlockCells(i, column), value, _board.GetBlockHeight(), column);
                    if (uniqueValue == true)
                    {
                        for (int j = 0; j != _board.GetBoardSize(); j++)
                        {
                            if (((j < i) || (j >= (i + _board.GetBlockWidth()))) && (possibleValues[j, column].Contains(value) == true))
                            {
                                possibleValues[j, column].Remove(value);
                                changes = true;
                            }
                        }
                    }

                }
            }
            return changes;
        }

        private bool CheckIfPossibleValueOnlyExistsInBlockInSelection(IEnumerable<Coordinate> cells, int value, int indexBlockStart, int cellsPrBlock)
        {
            int j = 0;
            bool uniqueValue = true;
            foreach (Coordinate cell in cells)
            {
                if (((j < indexBlockStart) || (j >= (indexBlockStart + cellsPrBlock))) && possibleValues[cell.Row, cell.Column].Contains(value))
                {
                    uniqueValue = false;
                    return uniqueValue;
                }
                j++;
            }
            return uniqueValue;
        }

        private bool CheckIfPossibleValueOnlyExistsInRowInBlock(IEnumerable<Coordinate> cells, int value, int cellsPrBlock, int row)
        {
            int j = 0;
            bool uniqueValue = true;
            foreach (Coordinate cell in cells)
            {
                if ((row != GetRowFromBlockPosition(row, j)) && possibleValues[cell.Row, cell.Column].Contains(value))
                {
                    uniqueValue = false;
                    return uniqueValue;
                }
                j++;
            }
            return uniqueValue;
        }

        private bool CheckIfPossibleValueOnlyExistsInColumnInBlock(IEnumerable<Coordinate> cells, int value, int cellsPrBlock, int column)
        {
            int j = 0;
            bool uniqueValue = true;
            foreach (Coordinate cell in cells)
            {
                if ((column != GetColumnFromBlockPosition(column, j)) && possibleValues[cell.Row, cell.Column].Contains(value))
                {
                    uniqueValue = false;
                    return uniqueValue;
                }
                j++;
            }
            return uniqueValue;
        }

        private List<int> GetPossibleValuesInSectionInBlock(IEnumerable<Coordinate> cells, int indexBlockStart, int cellsPrBlock)
        {
            List<int> possibleValuesInSelectionInBlock = new List<int>();
            int j = 0;
            foreach (Coordinate cell in cells)
            {
                if ((j >= indexBlockStart) && (j < (indexBlockStart + cellsPrBlock)) && _board.IsNumberBlank(cell.Row, cell.Column))
                {
                    foreach (int value in possibleValues[cell.Row, cell.Column])
                    {
                        if (possibleValuesInSelectionInBlock.Contains(value) == false)
                            possibleValuesInSelectionInBlock.Add(value);
                    }
                }
                j++;
            }
            return possibleValuesInSelectionInBlock;
        }

        private int GetRowFromBlockPosition(int blockPositionX, int blockCellPosition)
        {
            return ((blockPositionX / _board.GetBlockHeight()) * _board.GetBlockHeight()) + (blockCellPosition / _board.GetBlockWidth());
        }

        private int GetColumnFromBlockPosition(int blockPositionY, int blockCellPosition)
        {
            return ((blockPositionY / _board.GetBlockWidth()) * _board.GetBlockWidth()) + (blockCellPosition % _board.GetBlockWidth());
        }

        /// <summary>
        /// Step 4: Multi-sets stick together
        /// 
        /// Another more abstract method, if a column / row / block has say two values which
        /// both only have two allowed possible squares, and these two squares are the same
        /// for both values then this pair of values must be in these two squares. All other
        /// values therefore can not be placed in these squares (if any others exist). The
        /// above is repeated for sets of three, four and five values too. 
        /// </summary>
        private bool SolverStep4()
        {
            // We did not implement this step
            return false;
        }

        /// <summary>
        /// Brute Force
        /// </summary>
        private bool BruteForce()
        {
            Coordinate CellsWithMinimumPossibleValues = null;
            int minimumPossibleValues = int.MaxValue;
            for (int i = 0; i != _board.GetBoardSize(); i++)
            {
                for (int j = 0; j != _board.GetBoardSize(); j++)
                {
                    if ((possibleValues[i, j].Count < minimumPossibleValues) && (possibleValues[i, j].Count > 0))
                    {
                        minimumPossibleValues = possibleValues[i, j].Count;
                        CellsWithMinimumPossibleValues = new Coordinate(i, j);
                    }
                }
            }

            if (CellsWithMinimumPossibleValues == null)
                return false;

            foreach (int possibleValue in possibleValues[CellsWithMinimumPossibleValues.Row, CellsWithMinimumPossibleValues.Column])
            {
                AbstractBoard copyOfBoard = (AbstractBoard)_board.Clone();
                copyOfBoard.SetNumber(CellsWithMinimumPossibleValues.Row, CellsWithMinimumPossibleValues.Column, possibleValue);
                Solver newSolver = new Solver(copyOfBoard);
                if (newSolver.SolveBoard())
                {
                    for (int i = 0; i != _board.GetBoardSize(); i++)
                    {
                        for (int j = 0; j != _board.GetBoardSize(); j++)
                        {
                            _board.SetNumber(i, j, copyOfBoard.GetNumber(i, j));
                        }
                    }

                    return true;
                }
            }

            return false;
        }
    }
}