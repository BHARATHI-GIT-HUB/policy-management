using System.Runtime.Serialization;

namespace RepositryAssignement.Custome_Exception
{
    [Serializable]
    public class PolicyNotFoundException : Exception
    {
        public PolicyNotFoundException()
        {
        }

        public PolicyNotFoundException(string? message) : base(message)
        {
        }

        public PolicyNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

#pragma warning disable SYSLIB0051 // Type or member is obsolete
        protected PolicyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
#pragma warning restore SYSLIB0051 // Type or member is obsolete
        {
        }
    }
}
