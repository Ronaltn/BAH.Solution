using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    public static class EnumerableExtension
    {
        public static IList<IEnumerable<TSource>> Paging<TSource>(this IEnumerable<TSource> source, int size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException();

            IList<IEnumerable<TSource>> result = new List<IEnumerable<TSource>>();
            int index = 0, left = source.Count();
            while (left > 0)
            {
                result.Add(source.Skip(index * size).Take(left >= size ? size : left));

                index++;
                left = left - size;
            }//end while
            return result;
        }

        public static IEnumerable<TSource> Paging<TSource>(this IEnumerable<TSource> source, int size, int index)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException();
            if (index < 0) throw new ArgumentOutOfRangeException();

            int has = index * size;
            has = has >= source.Count() ? source.Count() : has;

            int left = source.Count() - has;
            left = left >= 0 ? left : 0;

            IEnumerable<TSource> result = source.Skip(has).Take(left >= size ? size : left);
            return result;
        }

    }//end class
}//end namespace
