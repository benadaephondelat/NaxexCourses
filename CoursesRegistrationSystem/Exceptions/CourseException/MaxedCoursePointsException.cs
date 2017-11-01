namespace Exceptions.CourseException
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Main User Exception
    /// </summary>
    [Serializable]
    public class MaxedCoursePointsException : MainApplicationException
    {
        public static new string CustomMessage = "MaxedCoursePointsException";

        public MaxedCoursePointsException() : base()
        {
        }

        public MaxedCoursePointsException(string message)
            : base(message)
        {
        }

        public MaxedCoursePointsException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public MaxedCoursePointsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public MaxedCoursePointsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected MaxedCoursePointsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}