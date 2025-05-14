using System.Runtime.Serialization;

namespace BikeMatrixTest.Exceptions
{
    public class BikeMatirxValidationExceptions : Exception
    {
        public List<string> Errors { get; set; }

        public BikeMatirxValidationExceptions()
        {
        }

        public BikeMatirxValidationExceptions(string? message) : base(message)
        {
        }

        public BikeMatirxValidationExceptions(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BikeMatirxValidationExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
