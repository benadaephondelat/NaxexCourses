namespace Exceptions.UserExceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Main User Exception
    /// </summary>
    [Serializable]
    public class UserNotAuthorizedException : MainApplicationException
    {
        public static new string CustomMessage = "UserNotAuthorizedException";

        public UserNotAuthorizedException() : base()
        {
        }

        public UserNotAuthorizedException(string message)
            : base(message)
        {
        }

        public UserNotAuthorizedException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public UserNotAuthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public UserNotAuthorizedException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected UserNotAuthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}