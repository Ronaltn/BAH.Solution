using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    public static class EnumerableExtension
    {
        public static bool AnyOrNull<TSource>(this IEnumerable<TSource> source)
        {
            return source == null ? false : source.Any();
        }//end method

        public static bool AnyOrNull<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source == null ? false : source.Any(predicate);
        }//end method

        public static TSource FirstOrNullDefault<TSource>(this IEnumerable<TSource> source)
        {
            return AnyOrNull<TSource>(source) ? source.FirstOrDefault() : default(TSource);
        }//end method

        public static TSource FirstOrNullDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return AnyOrNull<TSource>(source) ? source.FirstOrDefault(predicate) : default(TSource);
        }//end method

        public static TSource SingleOrNullDefault<TSource>(this IEnumerable<TSource> source)
        {
            return AnyOrNull<TSource>(source) ? source.SingleOrDefault() : default(TSource);
        }//end method

        public static TSource SingleOrNullDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return AnyOrNull<TSource>(source) ? source.SingleOrDefault(predicate) : default(TSource);
        }//end method

        public static IList<IEnumerable<TSource>> Paging<TSource>(this IEnumerable<TSource> source, int limit)
        {
            IList<IEnumerable<TSource>> result = new List<IEnumerable<TSource>>();
            int num = 1, left = source.Count();
            while (left > 0)
            {
                result.Add(source.Skip((num - 1) * limit).Take(left >= limit ? limit : left));

                num++;
                left = left - limit;
            }//end while
            return result;
        }//end method

    }//end class
}//end namespace
