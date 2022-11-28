using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class incorrectData : Exception
    {
        public override string Message => "The data is incorrect ";
        public override string ToString()
        {
            return Message;

        }
    }


    /// <summary>
    /// throw when the id is wrong
    /// </summary>
    public class MissingID : Exception
    {
        public override string Message => "The object doesnt exist ";
        public override string ToString()
        {
            return Message;

        }
    }



    /// <summary>
    /// throw when one or more of the data is incorrect
    /// </summary>
    public class DuplicateID : Exception
    {
        public override string Message => "The object is already exists ";
        public override string ToString()
        {
            return Message;

        }
    }