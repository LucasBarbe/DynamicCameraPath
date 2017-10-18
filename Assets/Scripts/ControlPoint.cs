using System;
using System.Collections.Generic;
using UnityEngine;

namespace Luc4rts.BezierCurve
{
    public class ControlPoint
    {
        private Vector3 m_position;
        private bool m_isActive = true;

        public Vector3 Position
        {
            get
            {
                return m_position;
            }

            set
            {
                m_position = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return m_isActive;
            }

            set
            {
                m_isActive = value;
            }
        }
    }
}
