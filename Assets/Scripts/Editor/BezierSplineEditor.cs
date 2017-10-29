using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Luc4rts.BezierCurve
{
    [CustomEditor(typeof(BezierSpline))]
    public class BezierSplineEditor : Editor {

        BezierSpline m_;

        Vector3 m_vector3;

        ControlPoint m_tpsControlPoint;

        Quaternion m_handleRotation;

        public void UndoCB()
        {
            m_.CalculateIntrepolatedPoints();
        }

        private void OnEnable()
        {
            m_ = target as BezierSpline;
            m_.CalculateIntrepolatedPoints();
            Undo.undoRedoPerformed += UndoCB;
        }

        private void OnDisable()
        {
            Undo.undoRedoPerformed -= UndoCB;
        }

        private void OnSceneGUI()
        {
            m_ = target as BezierSpline;

            m_handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            m_.transform.rotation : Quaternion.identity;

            
            for (int i = 1; i < m_.BezierNodes.Length; i++)
            {
                Handles.color = Color.grey;
                DrawControlTangent(i - 1, 1);
                DrawControlTangent(i, 0);

                DrawBezier(i - 1, i, Color.white);

                Handles.color = Color.white;
                DrawPositionHandle(i - 1);
                DrawPositionHandle(i -1, 1);
                //DrawPositionHandle(i);
                DrawPositionHandle(i, 0);
                //float size = HandleUtility.GetHandleSize(m_vector3);
                /*for (int j = 1; j < m_.Steps; j++)
                {
                    //Handles.DrawLine(Cubic.GetPoint(m_.GetNodePosition(i - 1), m_.GetNodeControlPointPosition(i - 1, 1), m_.GetNodeControlPointPosition(i, 0), m_.GetNodePosition(i), (1 / (float)steps) * (float)j), Cubic.GetPoint(m_.GetNodePosition(i - 1), m_.GetNodeControlPointPosition(i - 1, 1), m_.GetNodeControlPointPosition(i, 0), m_.GetNodePosition(i), (1 / (float)steps) * (float)j) + Vector3.up);
                    Handles.color = Color.red;
                    Handles.DotHandleCap(0, Cubic.GetPoint(m_.GetNodePosition(i - 1), m_.GetNodeControlPointPosition(i - 1, 1), m_.GetNodeControlPointPosition(i, 0), m_.GetNodePosition(i), (1 / (float)m_.Steps) * (float)j), Quaternion.identity, size * 0.025f, EventType.Repaint);
                }*/

            }

            float size;
            Handles.color = Color.red;
            for (int i = 0; i < m_.InterpolatedPoints.GetLength(0); i++)
            {
                for (int j = 1; j < m_.InterpolatedPoints.GetLength(1); j++)
                {
                    size = HandleUtility.GetHandleSize(m_.InterpolatedPoints[i, j]);
                    Handles.DotHandleCap(0, m_.GetInterpolatedPointPosition(i, j), m_handleRotation, size * 0.025f, EventType.Repaint);
                }
            }
            Handles.color = Color.white;
            DrawPositionHandle(m_.BezierNodes.Length-1);
        }

        #region DrawTools

        int m_selectedNode = -1;
        int m_selectedControlPoint = -1;

        void DrawBezier(BezierNode nodeA, BezierNode nodeB, Color color)
        {
            Handles.DrawBezier(nodeA.Position, nodeB.Position, nodeA.GetControlPointPosition(1), nodeB.GetControlPointPosition(0), color, null, 2f);
        }
        
        void DrawBezier(int nodeAIndex, int nodeBIndex, Color color)
        {
            Handles.DrawBezier(
                m_.GetNodePosition(nodeAIndex),
                m_.GetNodePosition(nodeBIndex),
                m_.GetNodeControlPointPosition(nodeAIndex,1), 
                m_.GetNodeControlPointPosition(nodeBIndex, 0), 
                color, null, 2f);
        }

        void DrawControlTangent(BezierNode node, int controlPointIndex)
        {
            Handles.DrawLine(node.Position, node.GetControlPointPosition(controlPointIndex));
        }

        void DrawControlTangent(int nodeIndex, int controlPointIndex)
        {
            Handles.DrawLine(m_.GetNodePosition(nodeIndex), m_.GetNodeControlPointPosition(nodeIndex, controlPointIndex));
        }

        private const float handleSize = 0.04f;
        private const float pickSize = 0.06f;

        void DrawPositionHandle(int nodeIndex, int controlPointIndex = -1)
        {

            if(controlPointIndex < 0)
            {
                m_vector3 = m_.GetNodePosition(nodeIndex);
            }
            else
            {
                m_vector3 = m_.GetNodeControlPointPosition(nodeIndex, controlPointIndex);
            }
            float size = HandleUtility.GetHandleSize(m_vector3);
            if (Handles.Button(m_vector3, m_handleRotation, size * handleSize, size * pickSize, Handles.DotHandleCap))
            {
                m_selectedNode = nodeIndex;
                m_selectedControlPoint = controlPointIndex;
            }

            if (m_selectedNode == nodeIndex && m_selectedControlPoint == controlPointIndex)
            {
                EditorGUI.BeginChangeCheck();
                m_vector3 = Handles.DoPositionHandle(m_vector3, m_handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(m_, "Move Point");
                    EditorUtility.SetDirty(m_);
                    m_.SetPosition(m_vector3, nodeIndex, controlPointIndex);
                }
            }
        }
#endregion
    }
}

