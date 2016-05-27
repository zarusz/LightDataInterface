using System;

namespace LightDataInterface
{
    public class DataAccessException : Exception
    {
        public DataAccessException(string message) 
            : base(message)
        {
        }
    }
}