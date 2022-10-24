using UnityEngine;

namespace Utils
{
    public static class MathUtil
    {
        public static Vector3 RoundTo(Vector3 vector, float round)
        {
            return new Vector3(RoundTo(vector.x, round), RoundTo(vector.y, round), RoundTo(vector.z, round));
        }

        public static float RoundTo(float value, float round)
        {
            var intValue = (int)(value / round);
            var rounderPart = value % round > round / 2 ? round : 0;
            return intValue * round + rounderPart;
        }

        public static Vector3 AddToVector3(Vector3 vector, float value)
        {
            return new Vector3(vector.x + value, vector.y + value, vector.z + value);
        }
    }
}