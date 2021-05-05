using Loris.Common.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Loris.Common.Extensions
{
    public static class QueryableExtension
    {
        #region Sorting

        public static IOrderedQueryable<T> Sorting<T>(this IQueryable<T> query, string propertyName, ReqParamSortOrder sortOrder)
        {
            var entityType = typeof(T);

            // Create x=>x.PropName
            var propertyInfo = entityType.GetProperty(propertyName);
            if (propertyInfo.DeclaringType != entityType)
                propertyInfo = propertyInfo.DeclaringType.GetProperty(propertyName);

            // If we try to order by a property that does not exist in the object return the list
            if (propertyInfo == null)
                return (IOrderedQueryable<T>)query;

            // Sorting way
            var pWay = "OrderBy";
            if (sortOrder == ReqParamSortOrder.Desc)
                pWay = "OrderByDescending";

            var arg = Expression.Parameter(entityType, "x");
            var property = Expression.MakeMemberAccess(arg, propertyInfo);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            // Get System.Linq.Queryable.OrderBy() method.
            var method = typeof(Queryable).GetMethods()
                 .Where(m => m.Name == pWay && m.IsGenericMethodDefinition)
                 .Where(m => m.GetParameters().ToList().Count == 2) // ensure selecting the right overload
                 .Single();

            //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /* Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it. */
            return (IOrderedQueryable<T>)genericMethod.Invoke(genericMethod, new object[] { query, selector });
        }

        #endregion

        #region Order

        public static IOrderedQueryable<T> OrderBy<T>(
            this IQueryable<T> source,
            string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(
            this IQueryable<T> source,
            string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> source,
            string property,
            string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        /*
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string ordering)
        {
            var type = typeof(T);
            var property = type.GetProperty(ordering);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, query.Expression, Expression.Quote(orderByExp));
            return query.Provider.CreateQuery<T>(resultExp);
        }
        */

        #endregion
    }
}
