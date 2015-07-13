using System;
using System.Linq.Expressions;

namespace Bardock.Utils.Types
{
    /// <summary>
    /// Stolen from https://github.com/scottksmith95/LINQKit/blob/master/src/Linq.cs
    /// See http://tomasp.net/blog/dynamic-linq-queries.aspx for information on how it's used.
    /// </summary>
    public static class Lambda
    {
        /// <summary>
        /// Returns the given anonymous method as a lambda expression
        /// </summary>
        public static Expression<Func<TResult>> Expr<TResult>(Expression<Func<TResult>> expr)
        {
            return expr;
        }

        /// <summary>
        /// Returns the given anonymous method as a lambda expression
        /// </summary>
        public static Expression<Func<T, TResult>> Expr<T, TResult>(Expression<Func<T, TResult>> expr)
        {
            return expr;
        }

        /// <summary>
        /// Returns the given anonymous method as a lambda expression
        /// </summary>
        public static Expression<Func<T1, T2, TResult>> Expr<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> expr)
        {
            return expr;
        }

        /// <summary>
        /// Returns the given anonymous function as a Func delegate
        /// </summary>
        public static Func<TResult> Func<TResult>(Func<TResult> expr)
        {
            return expr;
        }

        /// <summary>
        /// Returns the given anonymous function as a Func delegate
        /// </summary>
        public static Func<T, TResult> Func<T, TResult>(Func<T, TResult> expr)
        {
            return expr;
        }

        /// <summary>
        /// Returns the given anonymous function as a Func delegate
        /// </summary>
        public static Func<T1, T2, TResult> Func<T1, T2, TResult>(Func<T1, T2, TResult> expr)
        {
            return expr;
        }
    }
}