using DO;
//using Dal;

namespace DO
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

    public class incorrectData : Exception
    {
        public incorrectData() : base(" invalid input") { }
        public incorrectData(string message) : base(message) { }
        public incorrectData(string message, Exception inner) : base(message, inner) { }
        protected incorrectData(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }



    /// <summary>
    /// throw when the id is wrong
    /// </summary>
    public class MissingID : Exception
    {
        public MissingID() : base(" invalid input") { }
        public MissingID(string message) : base(message) { }
        public MissingID(string message, Exception inner) : base(message, inner) { }
        protected MissingID(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }



    /// <summary>
    /// throw when one or more of the data is incorrect
    /// </summary>
    public class DuplicateID : Exception
    {
        public DuplicateID() : base(" invalid input") { }
        public DuplicateID(string message) : base(message) { }
        public DuplicateID(string message, Exception inner) : base(message, inner) { }
        protected DuplicateID(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}