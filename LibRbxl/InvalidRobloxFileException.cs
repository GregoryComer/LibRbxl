using System;

namespace LibRbxl
{
    public class InvalidRobloxFileException : Exception
    {
        public InvalidRobloxFileException()
        {
        }

        public InvalidRobloxFileException(string message) : base(message)
        {
        }

        public InvalidRobloxFileException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}