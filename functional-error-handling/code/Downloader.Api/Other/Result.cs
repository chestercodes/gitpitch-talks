namespace Downloader.Api.Other
{
    public abstract class Result<TOk, TError>
    {
        private Result()
        {
        }

        public static Result<TOk, TError> ToOk(TOk item) => new Ok(item);

        public static Result<TOk, TError> ToError(TError item) => new Error(item);

        public bool IsOk => this is Ok;

        public bool IsError => this is Error;

        public Ok AsOk => this as Ok;

        public Error AsError => this as Error;

        public class Ok : Result<TOk, TError>
        {
            internal Ok(TOk item)
            {
                Item = item;
            }

            public TOk Item { get; }

            public static implicit operator TOk(Ok success) => success.Item;
        }

        public class Error : Result<TOk, TError>
        {
            internal Error(TError item)
            {
                Item = item;
            }

            public TError Item { get; }

            public static implicit operator TError(Error failure) => failure.Item;
        }
    }
}
