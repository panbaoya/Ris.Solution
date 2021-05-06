using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Tools
{

    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }


    public static class ExpressionBuilder
    {

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "x");
            var parameterReplacer = new ParameterReplacer(candidateExpr);
            if (one == null)
            {
                return another;
            }

            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            var body = Expression.OrElse(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        public static Expression<Func<T, bool>> And<T>(
    this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "x");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            if (one == null)
            {
                return another;
            }

            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            var body = Expression.AndAlso(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">表一</typeparam>
        /// <typeparam name="F">表二</typeparam>
        /// <param name="exp1">x=></param>
        /// <param name="exp2">y=></param>
        /// <returns></returns>
        public static Expression<Func<T, F, bool>> And<T, F>(this Expression<Func<T, F, bool>> exp1,
            Expression<Func<T, F, bool>> exp2)
        {
            var param1 = Expression.Parameter(typeof(T), "x");
            var param2 = Expression.Parameter(typeof(F), "y");
            if (exp1 == null)
            {
                return exp2;
            }
            var left = exp1.Body;
            var right = exp2.Body;
            var body = Expression.AndAlso(left, right);
            return Expression.Lambda<Func<T, F, bool>>(body, new ParameterExpression[] { param1, param2 });
        }

        /// <summary>
        /// 表达式连接/三表查询
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        /// <returns></returns>
        public static Expression<Func<A, B, C, bool>> And<A, B, C>(this Expression<Func<A, B, C, bool>> exp1,
            Expression<Func<A, B, C, bool>> exp2)
        {
            var param1 = Expression.Parameter(typeof(A), "a");
            var param2 = Expression.Parameter(typeof(B), "b");
            var param3 = Expression.Parameter(typeof(C), "c");
            if (exp1 == null)
            {
                return exp2;
            }
            var left = exp1.Body;
            var right = exp2.Body;
            var body = Expression.AndAlso(left, right);
            return Expression.Lambda<Func<A, B, C, bool>>(body, new ParameterExpression[] { param1, param2, param3 });
        }
    }
}
