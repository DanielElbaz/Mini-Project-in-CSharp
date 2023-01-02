using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{


    [Serializable]
    public class invalidInputException : Exception
    {
        public invalidInputException() : base(" invalid input") { }
        public invalidInputException(string message) : base(message) { }
        public invalidInputException(string message, Exception inner) : base(message, inner) { }
        protected invalidInputException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class incorrectDataException : Exception
    {
        public incorrectDataException() : base(" invalid input") { }
        public incorrectDataException(string message) : base(message) { }
        public incorrectDataException(string message, Exception inner) : base(message, inner) { }
        protected incorrectDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }



    /// <summary>
    /// throw when the id is wrong
    /// </summary>
    public class MissingIDException : Exception
    {
        public MissingIDException() : base(" invalid input") { }
        public MissingIDException(string message) : base(message) { }
        public MissingIDException(string message, Exception inner) : base(message, inner) { }
        protected MissingIDException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }



    /// <summary>
    /// throw when one or more of the data is incorrect
    /// </summary>
    public class DuplicateIDException : Exception
    {
        public DuplicateIDException() : base(" invalid input") { }
        public DuplicateIDException(string message) : base(message) { }
        public DuplicateIDException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateIDException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}