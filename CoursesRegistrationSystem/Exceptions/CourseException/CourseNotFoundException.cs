namespace Exceptions.CourseException
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Main User Exception
    /// </summary>
    [Serializable]
    public class CourseNotFoundException : MainApplicationException
    {
        public CourseNotFoundException() : base()
        {
        }

        public CourseNotFoundException(string message)
            : base(message)
        {
        }

        public CourseNotFoundException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public CourseNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public CourseNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected CourseNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}