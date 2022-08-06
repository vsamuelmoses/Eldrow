using LanguageExt.Common;
using System;

namespace Eldrow.Extensions
{
    public static class FunctionalExtensions
    {
        public static Result<T> Join<T>(this Result<Result<T>> nested)
            => nested.Match(_ => _, e => new Result<T>(e));

        public static Result<TOut> MapJoin<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> map)
            => result.Map(map).Join();
    }
}