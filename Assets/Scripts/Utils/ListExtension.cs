namespace Utils
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;

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
}