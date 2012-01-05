using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    class BoardFactory
    {
        public static AbstractBoard CreateBoard(int size)
        {
            switch (size)
            {
                case 4:
                    return new FourByFourBoard();

                case 6:
                    return new SixBySixBoard();

                case 9:
                    return new NineByNineBoard();

                case 12:
                    return new TwelveByTwelveBoard();

                case 16:
                    return new SixteenBySixteenBoard();

                default:
                    throw new UnsupportedBoardSize();
            }
        }
    }
}
