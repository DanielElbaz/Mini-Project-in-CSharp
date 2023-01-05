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

    public class IncorrectDataException : Exception
    {
        public IncorrectDataException() : base(" invalid input") { }
        public IncorrectDataException(string message) : base(message) { }
        public IncorrectDataException(string message, Exception inner) : base(message, inner) { }
        protected IncorrectDataException(
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
    public class DuplicateIDExeption : Exception
    {
        public DuplicateIDExeption() : base(" invalid input") { }
        public DuplicateIDExeption(string message) : base(message) { }
        public DuplicateIDExeption(string message, Exception inner) : base(message, inner) { }
        protected DuplicateIDExeption(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

}