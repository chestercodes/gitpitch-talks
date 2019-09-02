namespace Downloader.Api.Other
{
    public abstract class Result<TOk, TError>
    {
        private Result()
        {
        }

        public static Result<TOk, TError> Ok(TOk item) => new OkImpl(item);

        public static Result<TOk, TError> Error(TError item) => new ErrorImpl(item);

        public bool IsOk => this is OkImpl;

        public OkImpl AsOk => this as OkImpl;

        public ErrorImpl AsError => this as ErrorImpl;

        public class OkImpl : Result<TOk, TError>
        {
            internal OkImpl(TOk item)
            {
                Item = item;
            }

            public TOk Item { get; }
        }

        public class ErrorImpl : Result<TOk, TError>
        {
            internal ErrorImpl(TError item)
            {
                Item = item;
            }

            public TError Item { get; }
        }
    }
}
