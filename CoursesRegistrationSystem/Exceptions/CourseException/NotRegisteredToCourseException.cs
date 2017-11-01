namespace Exceptions.CourseException
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Main User Exception
    /// </summary>
    [Serializable]
    public class NotRegisteredToCourseException : MainApplicationException
    {
        public static new string CustomMessage = "NotRegisteredToCourseException";

        public NotRegisteredToCourseException() : base()
        {
        }

        public NotRegisteredToCourseException(string message)
            : base(message)
        {
        }

        public NotRegisteredToCourseException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public NotRegisteredToCourseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public NotRegisteredToCourseException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected NotRegisteredToCourseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}