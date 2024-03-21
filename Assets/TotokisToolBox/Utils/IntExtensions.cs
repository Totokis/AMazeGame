using System.Numerics;

namespace TotokisUtils.Utils
{
    public static class IntExtensions
    {
        public static Vector3 ToVector3X(this int value)
        {
            return new Vector3(value, 0, 0);
        }

        public static Vector3 ToVector3Y(this int value)
        {
            return new Vector3(0, value, 0);
        }

        public static Vector3 ToVector3Z(this int value)
        {
            return new Vector3(0, 0, value);
        }

        public static Vector2 ToVector2X(this int value)
        {
            return new Vector2(value, 0);
        }

        public static Vector2 ToVector2Y(this int value)
        {
            return new Vector2(0, value);
        }
    }
}