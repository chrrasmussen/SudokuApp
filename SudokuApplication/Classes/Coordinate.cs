using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuApplication
{
    // Code from: http://stackoverflow.com/questions/1939319/defining-two-dimensional-dynamic-array
    public class Coordinate : IEquatable<Coordinate>
    {
        /* Properties */

        public int Row { get; set; }
        public int Column { get; set; }


        /* Constructors */

        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }


        /* Helper methods */

        public override string ToString()
        {
            return String.Format("<{0}> Row:{1} Column:{2}", GetType(), Row, Column);
        }


        /* IEquatable methods */

        public bool Equals(Coordinate coordinate)
        {
            return (this.Row == coordinate.Row) && (this.Column == coordinate.Column);
        }

        // Code from: http://msdn.microsoft.com/en-us/library/system.object.gethashcode.aspx
        public override int GetHashCode()
        {
            return Row ^ Column;
        }
    }
}
