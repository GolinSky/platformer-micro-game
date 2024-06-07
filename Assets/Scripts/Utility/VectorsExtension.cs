using UnityEngine;
using Utilities.ScriptUtils.Math;

namespace Mario.Utility
{
    public static class VectorsExtension
    {
        private const float MinTolerance = 0.05f;

        public static bool IsEqual(this Vector2 a, Vector2 b, float minTolerance = MinTolerance)
        {
            return a.x.IsEqual(b.x, minTolerance) && a.y.IsEqual(b.y, minTolerance);
        }
    }
}