using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luc4rts.BezierCurve
{
    public class BezierCurveNode : MonoBehaviour
    {

        public enum BezierControlPointMode
        {
            Free,
            Aligned,
            Mirrored
        }

        private Vector3 m_position;
        private ControlPoint[] m_controlPoints = { new ControlPoint(), new ControlPoint() };

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

        public ControlPoint PreviousControlPoint
        {
            get
            {
                return m_controlPoints[0];
            }

            set
            {
                m_controlPoints[0] = value;
                ApplyContolMode(0, 1);
            }
        }

        public ControlPoint NextControlPoint
        {
            get
            {
                return m_controlPoints[1];
            }

            set
            {
                m_controlPoints[1] = value;
                ApplyContolMode(1, 0);
            }
        }

        public BezierControlPointMode BezierControlPointMode1
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

        void ApplyContolMode(int controlerIndex, int controledIndex)
        {
            switch (m_bezierControlPointMode)
            {
                case BezierControlPointMode.Free:
                    break;
                case BezierControlPointMode.Aligned:
                    AligneControlsPoint(controlerIndex, 0);
                    break;
                case BezierControlPointMode.Mirrored:
                    MirrorControlsPoint(1, 0);
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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

