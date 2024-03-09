using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Utils
{
    public static class ListExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
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