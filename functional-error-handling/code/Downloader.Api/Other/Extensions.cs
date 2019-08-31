using System;

namespace Downloader.Api.Other
{
    public static class ResultExtensions
    {
        public static Result<TB, TC> Bind<TA, TB, TC>(
            this Result<TA, TC> twoTrackInput,
            Func<TA, Result<TB, TC>> switchFunction)
        {
            return twoTrackInput.IsOk
                ? switchFunction(twoTrackInput.AsOk.Item)
                : Result<TB, TC>.ToError(twoTrackInput.AsError.Item);
        }

        public static Func<TA, Result<TC, TD>> Bind<TA, TB, TC, TD>(
            this Func<TA, Result<TB, TD>> twoTrackInputFunction,
            Func<TB, Result<TC, TD>> switchFunction)
        {
            return input => twoTrackInputFunction.Invoke(input).Bind(switchFunction);
        }

        public static Result<TB, TC> Map<TA, TB, TC>(
            this Result<TA, TC> twoTrackInput,
            Func<TA, TB> oneTrackFunction)
        {
            return twoTrackInput.IsOk
                ? Result<TB, TC>.ToOk(oneTrackFunction.Invoke(twoTrackInput.AsOk.Item))
                : Result<TB, TC>.ToError(twoTrackInput.AsError.Item);
        }


        public static TC Match<TA, TB, TC>(
            this Result<TA, TB> twoTrackInput,
            Func<TA, TC> successFunc,
            Func<TB, TC> failureFunc) =>
            twoTrackInput.IsOk
                ? successFunc(twoTrackInput.AsOk.Item)
                : failureFunc(twoTrackInput.AsError.Item);
    }
}
