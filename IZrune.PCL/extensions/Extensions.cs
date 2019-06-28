using IZrune.PCL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IZrune.PCL.extensions
{
   public static class Extensions
    {
        public static string ConverEnumToInt(this QuezCategory Categor )
        {
            var Result = Convert.ToInt32(Categor);

            return Result.ToString();
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
    }
}
