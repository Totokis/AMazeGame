using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace TotokisToolBox.Utils
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var provider = new RNGCryptoServiceProvider();
            var n = list.Count;
            while (n > 1)
            {
                var box = new byte[1];
                do
                {
                    provider.GetBytes(box);
                } while (!(box[0] < n * (byte.MaxValue / n)));

                var k = box[0] % n;
                n--;
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }

    public static class CollectionExtension
    {
        private static readonly Random rng = new();

        public static T RandomElementOrDefault<T>(this IList<T> list)
        {
            if (list != null && list.Count != 0)
            {
                return list[rng.Next(list.Count)];
            }

            return default;
        }
    }
}