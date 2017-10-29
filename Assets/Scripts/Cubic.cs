using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Luc4rts.BezierCurve
{
    public class Cubic
    {
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                    Mathf.Pow(oneMinusT, 3) * p0 +
                    3 * Mathf.Pow(oneMinusT, 2) * t * p1 +
                    3 * oneMinusT * Mathf.Pow(t, 2) * p2 +
                    Mathf.Pow(t, 3) * p3;
        }
    }
}
