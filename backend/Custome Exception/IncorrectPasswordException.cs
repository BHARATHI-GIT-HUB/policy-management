using System.Runtime.Serialization;

namespace RepositryAssignement.Custome_Exception
{
    [Serializable]
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException()
        {
        }

        public IncorrectPasswordException(string? message) : base(message)
        {
        }

        public IncorrectPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

#pragma warning disable SYSLIB0051 // Type or member is obsolete
        protected IncorrectPasswordException(SerializationInfo info, StreamingContext context) : base(info, context) { }
#pragma warning restore SYSLIB0051 // Type or member is obsolete
    }
}
