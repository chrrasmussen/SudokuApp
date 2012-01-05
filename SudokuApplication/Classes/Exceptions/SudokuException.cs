using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace SudokuApplication
{
    // Code from: http://csharptutorial.blogspot.com/2006/05/custom-exceptions.html
    // And: http://msdn.microsoft.com/en-us/library/ms229064.aspx
    class SudokuException : Exception, ISerializable
    {
        public SudokuException() : base() { }

        public SudokuException(string errorMessage) : base(errorMessage) { }

        public SudokuException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }

        protected SudokuException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
