using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eldrow.Extensions
{
    public static class EnumerableExtensions
    {
        private static Random random = new Random();
        public static T Random<T>(this IEnumerable<T> items)
        {
            var index = random.Next(items.Count() - 1);
            return items.Skip(index).First();
        }
    }
}
