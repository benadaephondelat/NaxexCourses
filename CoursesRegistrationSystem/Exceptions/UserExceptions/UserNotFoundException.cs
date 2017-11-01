namespace Exceptions.UserExceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Main User Exception
    /// </summary>
    [Serializable]
    public class UserNotFoundException : MainApplicationException
    {
        public static new string CustomMessage = "UserNotFoundException";

        public UserNotFoundException() : base()
        {
        }

        public UserNotFoundException(string message)
            : base(message)
        {
        }

        public UserNotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public UserNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public UserNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
