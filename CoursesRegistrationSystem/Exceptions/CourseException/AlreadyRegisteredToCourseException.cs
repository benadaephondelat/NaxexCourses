namespace Exceptions.CourseException
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Main User Exception
    /// </summary>
    [Serializable]
    public class AlreadyRegisteredToCourseException : MainApplicationException
    {
        public AlreadyRegisteredToCourseException() : base()
        {
        }

        public AlreadyRegisteredToCourseException(string message)
            : base(message)
        {
        }

        public AlreadyRegisteredToCourseException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public AlreadyRegisteredToCourseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public AlreadyRegisteredToCourseException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected AlreadyRegisteredToCourseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}