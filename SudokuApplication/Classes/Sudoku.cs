using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    class Sudoku
    {
        /* Fields */

        private AbstractBoard _board;
        private AbstractBoard _solvedBoard = null;


        /* Constructors */

        public Sudoku()
        {
            Console.WriteLine("Starting sudoku...");
        }


        /* Helper methods */

        public override string ToString()
        {
            return String.Format("<{0}> Board:{1}", GetType(), _board);
        }


        /* Solve methods */

        // Solves a copy of the current board
        private bool SolveBoard()
        {
            if (_solvedBoard != null)
                return true;

            // Copy the existing board and remove all non-predefined values
            AbstractBoard _copyOfBoard = (AbstractBoard)_board.Clone();
            _copyOfBoard.Clear();

            // Solve the copied board and assign to solved board
            Solver solver = new Solver(_copyOfBoard);
            if (solver.SolveBoard())
            {
                _solvedBoard = _copyOfBoard;
                return true;
            }
            else
                throw new UnsolvableBoardException();
        }
        
        // Gets the correct number from the solved board
        public int GetCorrectNumber(int row, int column)
        {
            if (SolveBoard())
                return _solvedBoard.GetNumber(row, column);
            else
                throw new UnsolvableBoardException();
        }

        // Checks if a section is completed
        public bool IsSectionCompleted(int row, int column)
        {
            if (_board != null)
                return Validator.ValidateCell(_board, row, column, true);
            else
                throw new GameNotStartedException();
        }

        // Checks if the board is completed
        public bool IsBoardCompleted()
        {
            if (_board != null)
                return Validator.ValidateBoard(_board, true);
            else
                throw new GameNotStartedException();
        }

        // Checks if all cells are valid
        public bool IsBoardValid()
        {
            if (_board != null)
                return Validator.ValidateBoard(_board, false);
            else
                throw new GameNotStartedException();
        }


        /* Game */

        // Creates a blank board and generates numbers according to difficulty
        public bool NewGame(int size, int difficulty)
        {
            if (NewGame(size))
            {
                Generator generator = new Generator(_board);
                generator.Generate(difficulty);

                return true;
            }

            return false;
        }

        // Creates a blank board board -- Waits for LockNumbers()
        public bool NewGame(int size)
        {
            try
            {
                // Create a blank board
                _board = BoardFactory.CreateBoard(size);
                _solvedBoard = null;
                return true;
            }
            catch (UnsupportedBoardSize)
            {
                return false;
            }
        }

        // Locks the current numbers and makes them predefined
        public void LockNumbers()
        {
            if (_board != null)
            {
                if (Validator.ValidateBoard(_board))
                    _board.ConvertExistingNumbersToPredefined();
                else
                    throw new InvalidBoardException();
            }
            else
                throw new GameNotStartedException();
        }
            
        // Solves the current board
        public void Surrender()
        {
            if (_board != null)
            {
                if (SolveBoard())
                {
                    // Transfer all correct numbers to the existing board
                    for (int i = 0; i != GetBoardSize(); i++)
                    {
                        for (int j = 0; j != GetBoardSize(); j++)
                        {
                            SetNumber(i, j, GetCorrectNumber(i, j));
                        }
                    }
                }
                else
                    throw new UnsolvableBoardException();
            }
            else
                throw new GameNotStartedException();
        }

        // Resets the current game and imports board from a file
        public bool ImportBoard(string boardData)
        {
            try
            {
                // Import board
                _board = Import.ParseDataIntoBoard(boardData);
                _solvedBoard = null;
                return true;
            }
            catch (UnsupportedBoardSize)
            {
                return false;
            }
        }

        // Saves the current game to a file
        public string ExportBoard()
        {
            return Export.ParseDataFromBoard(_board);
        }

        // Applies a hint to the board
        public void ApplyHint()
        {
            // Create a list of blank numbers
            List<Coordinate> blankNumbers = new List<Coordinate>();
            for (int i = 0; i != GetBoardSize(); i++)
            {
                for (int j = 0; j != GetBoardSize(); j++)
                {
                    if (IsNumberBlank(i, j))
                    {
                        blankNumbers.Add(new Coordinate(i, j));
                    }
                }
            }

            if (blankNumbers.Count > 0)
            {
                // Find a random coordinate and insert into board
                int randomIndex = new Random().Next(blankNumbers.Count);
                Coordinate randomCoordinate = blankNumbers[randomIndex];
                int correctNumber = GetCorrectNumber(randomCoordinate.Row, randomCoordinate.Column);
                SetNumber(randomCoordinate.Row, randomCoordinate.Column, correctNumber);
            }
        }

        
        /* Proxy to AbstractBoard */

        public int GetBoardSize()
        {
            if (_board != null)
                return _board.GetBoardSize();
            else
                throw new GameNotStartedException();
        }

        public int GetBlockWidth()
        {
            if (_board != null)
                return _board.GetBlockWidth();
            else
                throw new GameNotStartedException();
        }

        public int GetBlockHeight()
        {
            if (_board != null)
                return _board.GetBlockHeight();
            else
                throw new GameNotStartedException();
        }

        public int GetNumber(int row, int column)
        {
            if (_board != null)
                return _board.GetNumber(row, column);
            else
                throw new GameNotStartedException();
        }

        public void SetNumber(int row, int column, int value)
        {
            if (_board != null)
                _board.SetNumber(row, column, value);
            else
                throw new GameNotStartedException();
        }

        public void ClearNumber(int row, int column)
        {
            if (_board != null)
                _board.ClearNumber(row, column);
            else
                throw new GameNotStartedException();
        }

        public bool IsNumberBlank(int row, int column)
        {
            if (_board != null)
                return _board.IsNumberBlank(row, column);
            else
                throw new GameNotStartedException();
        }

        public bool IsNumberPredefined(int row, int column)
        {
            if (_board != null)
                return _board.IsNumberPredefined(row, column);
            else
                throw new GameNotStartedException();
        }

        /* Subscriptions */

        public void SubscribeToNumberChanges(INumberObserver observer)
        {
            _board.AddNumberObserver(observer);
        }

        public void UnsubscribeToNumberChanges(INumberObserver observer)
        {
            _board.RemoveNumberObserver(observer);
        }
    }
}
