using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luc4rts.BezierCurve
{
    [System.Serializable]
    public class BezierNode
    {
        [System.Serializable]
        public enum BezierControlPointMode
        {
            Free,
            Aligned,
            Mirrored
        }

        [SerializeField]
        private Vector3 m_position;
        [SerializeField]
        private ControlPoint[] m_controlPoints = { new ControlPoint(), new ControlPoint() };
        [SerializeField]
        private BezierControlPointMode m_bezierControlPointMode;

        //public ControlPoint[] ControlPoints
        //{
        //    get
        //    {
        //        return m_controlPoints;
        //    }

        //    set
        //    {
        //        m_controlPoints = value;
        //    }
        //}

        //public ControlPoint PreviousControlPoint
        //{
        //    get
        //    {
        //        return m_controlPoints[0];
        //    }

        //    set
        //    {
        //        m_controlPoints[0] = value;
        //        ApplyContolMode(0, 1);
        //    }
        //}

        //public ControlPoint NextControlPoint
        //{
        //    get
        //    {
        //        return m_controlPoints[1];
        //    }

        //    set
        //    {
        //        m_controlPoints[1] = value;
        //        ApplyContolMode(1, 0);
        //    }
        //}

        public BezierControlPointMode ControlPointMode
        {
            get
            {
                return m_bezierControlPointMode;
            }

            set
            {
                m_bezierControlPointMode = value;
                ApplyContolMode(0, 1);
            }
        }

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

        public void SetControlPointPosition(int id, Vector3 position)
        {
            if(id < 0)
            {
                id = 0;
            }
            else if(id > m_controlPoints.Length - 1)
            {
                id = m_controlPoints.Length - 1;
            }

            m_controlPoints[id].Position = position - m_position;
            ApplyContolMode(id, 1 - id);
        }

        public Vector3 GetControlPointPosition(int id)
        {
            if (id < 0)
            {
                id = 0;
            }
            else if (id > m_controlPoints.Length - 1)
            {
                id = m_controlPoints.Length - 1;
            }

            if (m_controlPoints[id].IsActive)
            {
                return m_position + m_controlPoints[id].Position;
            }
            else
            {
                return m_position;
            }
            
        }

        void ApplyContolMode(int controlerIndex, int controledIndex)
        {
            switch (m_bezierControlPointMode)
            {
                case BezierControlPointMode.Free:
                    break;
                case BezierControlPointMode.Aligned:
                    AligneControlsPoint(controlerIndex, controledIndex);
                    break;
                case BezierControlPointMode.Mirrored:
                    MirrorControlsPoint(controlerIndex, controledIndex);
                    break;
                default:
                    break;
            }
        }

        void AligneControlsPoint(int controlerIndex, int controledIndex)
        {
            if(controlerIndex >= 0 && controlerIndex < m_controlPoints.Length && controledIndex >= 0 && controledIndex < m_controlPoints.Length) //Check Array Value
            {
                m_controlPoints[controledIndex].Position = -m_controlPoints[controlerIndex].Position.normalized * m_controlPoints[controledIndex].Position.magnitude;
            }
            else
            {
                //Error
            }
        }

        void MirrorControlsPoint(int controlerIndex, int controledIndex)
        {
            if (controlerIndex >= 0 && controlerIndex < m_controlPoints.Length && controledIndex >= 0 && controledIndex < m_controlPoints.Length) //Check Array Value
            {
                m_controlPoints[controledIndex].Position = -m_controlPoints[controlerIndex].Position;
            }
            else
            {
                //Error
            }
        }

    }
}

