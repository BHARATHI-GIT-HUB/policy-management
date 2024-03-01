using System.Runtime.Serialization;

namespace RepositryAssignement.Custome_Exception
{
    [Serializable]
    public class PolicyManagementException : Exception
    {
        public PolicyManagementException()
        {
        }

        public PolicyManagementException(string? message) : base(message)
        {
        }

        public PolicyManagementException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

#pragma warning disable SYSLIB0051 // Type or member is obsolete
        protected PolicyManagementException(SerializationInfo info, StreamingContext context) : base(info, context) { }
#pragma warning restore SYSLIB0051 // Type or member is obsolete

    }
}
