using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Luc4rts.BezierCurve
{
    [CustomEditor(typeof(BezierSpline))]
    public class BezierSplineEditor : Editor {

        Vector3 m_previousControl;
        Vector3 m_currentControl;

        Vector3 m_previousPosition;
        Vector3 m_currentPosition;

        ControlPoint m_tpsControlPoint;

        

        private void OnSceneGUI()
        {
            BezierSpline bezierSpline = target as BezierSpline;

            Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            bezierSpline.transform.rotation : Quaternion.identity;

            m_previousPosition = bezierSpline.transform.TransformPoint(bezierSpline.BezierNodes[0].Position);
            m_previousControl = bezierSpline.transform.TransformPoint(bezierSpline.BezierNodes[0].NextControlPoint.Position);

            
            for (int i = 1; i < bezierSpline.BezierNodes.Length; i++)
            {

                m_currentPosition = bezierSpline.transform.TransformPoint(bezierSpline.BezierNodes[i].Position);
                m_currentControl = bezierSpline.transform.TransformPoint(bezierSpline.BezierNodes[i].PreviousControlPoint.Position);

                EditorGUI.BeginChangeCheck();
                m_previousPosition = Handles.DoPositionHandle(m_previousPosition, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    bezierSpline.BezierNodes[i-1].Position = bezierSpline.transform.InverseTransformPoint(m_previousPosition);
                }

                EditorGUI.BeginChangeCheck();
                m_previousControl = Handles.DoPositionHandle(m_previousControl, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    m_tpsControlPoint = bezierSpline.BezierNodes[i - 1].NextControlPoint;


                    m_tpsControlPoint.Position = bezierSpline.transform.InverseTransformPoint(m_previousControl);
                    bezierSpline.BezierNodes[i - 1].NextControlPoint = m_tpsControlPoint;
                }

                EditorGUI.BeginChangeCheck();
                m_currentPosition = Handles.DoPositionHandle(m_currentPosition, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    bezierSpline.BezierNodes[i].Position = bezierSpline.transform.InverseTransformPoint(m_currentPosition);
                }

                EditorGUI.BeginChangeCheck();
                m_currentControl = Handles.DoPositionHandle(m_currentControl, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    m_tpsControlPoint = bezierSpline.BezierNodes[i].PreviousControlPoint;

                    m_tpsControlPoint.Position = bezierSpline.transform.InverseTransformPoint(m_currentControl);
                    bezierSpline.BezierNodes[i].PreviousControlPoint = m_tpsControlPoint;
                }


                Handles.DrawBezier(m_previousPosition, bezierSpline.BezierNodes[i].Position, bezierSpline.BezierNodes[i - 1].NextControlPoint.Position, bezierSpline.BezierNodes[i].PreviousControlPoint.Position, Color.white, null, 2f);
                Handles.color = Color.grey;
                Handles.DrawLine(m_previousPosition, m_previousControl);
                Handles.DrawLine(bezierSpline.BezierNodes[i].Position, bezierSpline.BezierNodes[i].PreviousControlPoint.Position);

                m_previousPosition = m_currentPosition;
                m_previousControl = bezierSpline.transform.TransformPoint(bezierSpline.BezierNodes[i].NextControlPoint.Position);
            }
            
        }
    }
}

