using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luc4rts.BezierCurve
{
    public class BezierSpline : MonoBehaviour
    {
        [SerializeField]
        BezierNode[] m_bezierNodes;

        public BezierNode[] BezierNodes
        {
            get
            {
                return m_bezierNodes;
            }

            set
            {
                m_bezierNodes = value;
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
