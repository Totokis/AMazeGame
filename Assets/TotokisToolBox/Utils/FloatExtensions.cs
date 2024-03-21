using UnityEngine;

namespace TotokisUtils.Utils
{
    public static class FloatExtensions
    {
        public static Vector3 ToVector3X(this float value)
        {
            return new Vector3(value, 0, 0);
        }

        public static Vector3 ToVector3Y(this float value)
        {
            return new Vector3(0, value, 0);
        }

        public static Vector3 ToVector3Z(this float value)
        {
            return new Vector3(0, 0, value);
        }

        public static Vector2 ToVector2X(this float value)
        {
            return new Vector2(value, 0);
        }

        public static Vector2 ToVector2Y(this float value)
        {
            return new Vector2(0, value);
        }
    }
}