using Luc4rts.BezierCurve;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSplineCam : MonoBehaviour {

    [SerializeField]
    private Transform m_character;
    [SerializeField]
    private BezierSpline m_spline;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        if (m_character == null || m_spline == null)
            return;
        //int[] a = m_spline.GetClosestInterpolatedPointsIndex(m_character.position, m_spline.GetClosestNodeIndex(m_character.position));
        int[] a = m_spline.GetClosestInterpolatedPointsIndex(m_character.position);
        Debug.Log(m_spline.GetClosestNodeIndex(m_character.position) + " || " + a[0] + " | " + a[1]);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
                m_character.position,
                m_spline.GetNodePosition(m_spline.GetClosestNodeIndex(m_character.position))
            );

        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            m_character.position,
            //m_spline.GetNodePosition(m_spline.GetClosestNodeIndex(m_character.position))
            m_spline.GetInterpolatedPointPosition(a[0], a[1])
            );
        
    }
}
