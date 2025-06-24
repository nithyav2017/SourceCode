namespace PharmaClinicalSuite.Utility
{
    public class Result
    {
        public bool IsSucess { get;}
        public string Error { get; }
        
        public bool IsFailure => !IsSucess;
        protected Result(bool isSuccess, string error) {
            IsSucess = isSuccess;
            Error = error;
        }
        public static Result Success() => new Result(true, null);
        public static Result Failure(string error) => new Result(false, error);


    }
}
