namespace ITI.HMS.Services
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string? Error { get; private set; }

        public Result(bool isSuccess, string? error = null)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }

        public Result(bool isSuccess, T value, string? error = null)
            : base(isSuccess, error) 
        {
            Value = value;
        }
    }
}
