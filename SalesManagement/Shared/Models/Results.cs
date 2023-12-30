namespace SalesManagement.Server
{
    public class Result<T>
    {
        public T Value { get; }
        public string ErrorMessage { get; }

        public bool IsSuccess => ErrorMessage == null;

        private Result(T value)
        {
            Value = value;
        }

        private Result(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(string errorMessage) => new Result<T>(errorMessage);
    }
}
