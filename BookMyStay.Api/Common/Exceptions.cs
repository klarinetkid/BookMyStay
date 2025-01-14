namespace BookMyStay.Api.Common
{
    public class ReservationNotFoundException : Exception
    {
        public ReservationNotFoundException() { }
        public ReservationNotFoundException(string message) : base(message) { }
        public ReservationNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class IdIsNullException : Exception
    {
        public IdIsNullException() { }
        public IdIsNullException(string message) : base(message) { }
        public IdIsNullException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class IdIsNotNullException : Exception
    {
        public IdIsNotNullException() { }
        public IdIsNotNullException(string message) : base(message) { }
        public IdIsNotNullException(string message, Exception innerException) : base(message, innerException) { }
    }
}
