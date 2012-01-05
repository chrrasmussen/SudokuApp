using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace SudokuApplication
{
    class Cell : ICloneable
    {
        /* Properties */

        public int Value { get; set; }
        public bool Blank { get; set; }
        public bool Predefined { get; set; }


        /* Constructors */

        public Cell() : this(0, true, false) { }

        public Cell(int value) : this(value, false, true) { }

        public Cell(int value, bool predefined) : this(value, false, predefined) { }

        protected Cell(int value, bool blank, bool predefined)
        {
            Value = value;
            Blank = blank;
            Predefined = predefined;
        }


        /* Helper methods */

        public override string ToString()
        {
            return String.Format("<{0}> Value:{1} Blank:{2} Predefined:{3}", GetType(), Value, Blank, Predefined);
        }


        /* IClonable */

        // Shallow copy
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
