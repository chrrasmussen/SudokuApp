using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace SudokuApplication
{
    // Code from: http://csharptutorial.blogspot.com/2006/05/custom-exceptions.html
    // And: http://msdn.microsoft.com/en-us/library/ms229064.aspx
    class UnsolvableBoardException : SudokuException
    {
        public UnsolvableBoardException() : base() { }

        public UnsolvableBoardException(string errorMessage) : base(errorMessage) { }

        public UnsolvableBoardException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }

        protected UnsolvableBoardException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
