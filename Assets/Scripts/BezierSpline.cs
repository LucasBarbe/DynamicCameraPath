using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luc4rts.BezierCurve
{
    public class BezierSpline : MonoBehaviour
    {
        [SerializeField]
        BezierNode[] m_bezierNodes;

        [SerializeField]
        int m_steps = 20;

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

        public int Steps
        {
            get
            {
                return m_steps;
            }

            set
            {
                m_steps = value;
            }
        }

        public Vector3[,] InterpolatedPoints
        {
            get
            {
                return m_interpolatedPoints;
            }
        }

        private Vector3[,] m_interpolatedPoints = new Vector3[0,0];

        public void CalculateIntrepolatedPoints()
        {
            m_interpolatedPoints = new Vector3[m_bezierNodes.Length -1, m_steps];
            //Debug.Log("Lenght: " + m_interpolatedPoints.GetLength(0) + " | " + m_interpolatedPoints.GetLength(1));
            for (int i = 0; i < m_interpolatedPoints.GetLength(0) ; i++)
            {
                for (int j = 0; j < m_steps; j++)
                {
                    //Debug.Log(i + " | " + j);
                    m_interpolatedPoints[i,j] = Cubic.GetPoint(
                        m_bezierNodes[i].Position, 
                        m_bezierNodes[i].GetControlPointPosition(1), 
                        m_bezierNodes[i+1].GetControlPointPosition(0), 
                        m_bezierNodes[i+1].Position,
                        (1 / (float)m_steps) * (float)(j));
                }
            }
        }

        public Vector3 GetInterpolatedPointPosition(int i, int j)
        {
            if(i < 0)
            {
                return transform.TransformPoint(m_interpolatedPoints[0, 0]);
            }
            else if (j < 0)
            {
                return transform.TransformPoint(m_interpolatedPoints[i, 0]);
            }
            else if (i >= m_interpolatedPoints.GetLength(0))
            {
                return transform.TransformPoint(m_bezierNodes[m_bezierNodes.Length - 1].Position);
            }
            else if(j >= m_interpolatedPoints.GetLength(1))
            {
                transform.TransformPoint(m_interpolatedPoints[i, m_interpolatedPoints.GetLength(1)-1]);
            }
            return transform.TransformPoint(m_interpolatedPoints[i, j]);
        }

        public Vector3 GetNodeControlPointPosition(int nodeIndex, int controlPointIndex)
        {
            return transform.TransformPoint(m_bezierNodes[nodeIndex].GetControlPointPosition(controlPointIndex));
        }

        public Vector3 GetNodePosition(int nodeIndex)
        {
            return transform.TransformPoint(m_bezierNodes[nodeIndex].Position);
        }

        public void SetPosition(Vector3 position, int nodeIndex, int controlPointIndex = -1)
        {
            if(controlPointIndex == -1)
            {
                m_bezierNodes[nodeIndex].Position = position;
            }
            else
            {
                m_bezierNodes[nodeIndex].SetControlPointPosition(controlPointIndex, position);
            }

            CalculateIntrepolatedPoints();
            
        }

        public int GetClosestNodeIndex(Vector3 position)
        {
            int closestNodeIndex = -1;
            float shortestDistance = -1.0f;
            float dist;
            for (int i = 0; i < m_bezierNodes.Length; i++)
            {
                dist = Vector3.Distance(GetNodePosition(i), position);
                if(shortestDistance == -1f || dist < shortestDistance)
                {
                    shortestDistance = dist;
                    closestNodeIndex = i;
                }

            }

            return closestNodeIndex;
        }

        public int[] GetClosestInterpolatedPointsIndex(Vector3 position, int closestNodeIndex)
        {
            int[] closestPointIndex = { -1, -1 };
            float shortestDistance = -1;
            float dist;
            for (int i = 0; i < m_interpolatedPoints.GetLength(1); i++)
            {
                if(closestNodeIndex + 1 < m_bezierNodes.Length - 1)
                {
                    dist = Vector3.Distance(GetInterpolatedPointPosition(closestNodeIndex,i), position);
                    if (shortestDistance == -1f || dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        closestPointIndex[0] = closestNodeIndex;
                        closestPointIndex[1] = i;
                    }
                }
                
                if (closestNodeIndex - 1 >= 0)
                {
                    dist = Vector3.Distance(GetInterpolatedPointPosition(closestNodeIndex - 1, m_steps - i - 1), position);
                    if (shortestDistance == -1f || dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        closestPointIndex[0] = closestNodeIndex -1;
                        closestPointIndex[1] = m_steps - i - 1;
                    }
                }
                
            }
            return closestPointIndex; //not imp
        }
        public int[] GetClosestInterpolatedPointsIndex(Vector3 position)
        {
            int[] closestPointIndex = { -1, -1 };
            float shortestDistance = -1;
            float dist;
            for (int i = 0; i < m_interpolatedPoints.GetLength(0); i++)
            {
                for (int j = 0; j < m_interpolatedPoints.GetLength(1); j++)
                {
                    dist = Vector3.Distance(GetInterpolatedPointPosition(i, j), position);
                    if (shortestDistance == -1f || dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        closestPointIndex[0] = i;
                        closestPointIndex[1] = j;
                    }
                }
            }

            dist = Vector3.Distance(GetNodePosition(m_bezierNodes.Length-1), position);
            if (shortestDistance == -1f || dist < shortestDistance)
            {
                shortestDistance = dist;
                closestPointIndex[0] = m_bezierNodes.Length - 1;
                closestPointIndex[1] = 0;
            }
            return closestPointIndex;


        }

        // Use this for initialization
        void Start()
        {
            CalculateIntrepolatedPoints();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
