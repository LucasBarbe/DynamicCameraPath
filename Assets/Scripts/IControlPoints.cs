using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Luc4rts.BezierCurve
{
    interface IControlPoints
    {
        Vector3 this[int index]
        {
            get;
            set;
        }
    }
}
