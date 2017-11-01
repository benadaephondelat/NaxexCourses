namespace Exceptions.CourseException
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Main User Exception
    /// </summary>
    [Serializable]
    public class CourseAlreadyExistsException : MainApplicationException
    {
        public static new string CustomMessage = "CourseAlreadyExistsException";

        public CourseAlreadyExistsException() : base()
        {
        }

        public CourseAlreadyExistsException(string message)
            : base(message)
        {
        }

        public CourseAlreadyExistsException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public CourseAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public CourseAlreadyExistsException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        protected CourseAlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}