﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Luc4rts.BezierCurve
{
    [System.Serializable]
    public struct ControlPoint
    {
        [SerializeField]
        private Vector3 m_position;
        [SerializeField]
        private bool m_isActive;// = true;

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
