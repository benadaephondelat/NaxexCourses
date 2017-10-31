namespace Exceptions.CourseException
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Main User Exception
    /// </summary>
    [Serializable]
    public class InsufficientCoursePointsException : MainApplicationException
    {
        public InsufficientCoursePointsException() : base()
        {
        }

        public InsufficientCoursePointsException(string message)
            : base(message)
        {
        }

        public InsufficientCoursePointsException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public InsufficientCoursePointsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public InsufficientCoursePointsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected InsufficientCoursePointsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}