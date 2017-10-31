namespace Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Main Exception Class
    /// </summary>
    [Serializable]
    public abstract class MainApplicationException : Exception
    {
        public static string CustomMessage = "TicTacToe Main Application Exception.";

        public MainApplicationException() : base()
        {
        }

        public MainApplicationException(string message)
            : base(message)
        {
        }

        public MainApplicationException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public MainApplicationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public MainApplicationException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected MainApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}