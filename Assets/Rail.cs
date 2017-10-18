using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rail : MonoBehaviour {

    private List<Transform> m_transforms = new List<Transform>();

	// Use this for initialization
	void Start () {
        foreach (Transform trans in transform)
        {
            m_transforms.Add(trans);
        }
	}
	
	// Update is called once per frame
	void Update () {/*
        for (int i = 0; i < m_transforms.Count-1; i++)
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(m_transforms[i], 0.2f);
            
            Debug.DrawLine(m_transforms[i].position, m_transforms[i + 1].position, Color.green);
        }

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(m_transforms[m_transforms.Count-1], 0.2f);

        Debug.DrawLine(m_transforms[m_transforms.Count - 2].position, m_transforms[m_transforms.Count - 1].position, Color.green);*/
    }


    public Vector3 ProjectionOnRail(Vector3 pos)
    {
        int closestNodeIndex = GetClosestNode(pos);

        if (closestNodeIndex == 0)
        {
            return ProjectOnSegment(m_transforms[0].position, m_transforms[1].position, pos);
        }
        else if (closestNodeIndex == m_transforms.Count-1)
        {
            return ProjectOnSegment(m_transforms[m_transforms.Count - 1].position, m_transforms[1].position, pos);
        }
        else
        {
            Vector3 leftSeg = ProjectOnSegment(m_transforms[closestNodeIndex + 1].position, m_transforms[closestNodeIndex].position, pos);
            Vector3 rightSeg = ProjectOnSegment(m_transforms[closestNodeIndex - 1].position, m_transforms[closestNodeIndex].position, pos);
            Debug.DrawLine(pos, leftSeg, Color.red);
            Debug.DrawLine(pos, rightSeg, Color.yellow);

            if(Vector3.Distance(pos,leftSeg) <= Vector3.Magnitude(pos - rightSeg))
            {
                return leftSeg;
            }
            else
            {
                return rightSeg;
            }
        }
    }

    private Vector3 ProjectOnSegment(Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 segDirection = (v2 - v1).normalized;

        float distanceFromV1 = Vector3.Dot(segDirection, v1ToPos);
        if (distanceFromV1 < 0f)
        {
            return v1;
        }
        else if (Mathf.Sqrt(distanceFromV1) > Vector3.Distance(v1, v2))
        {
            return v2;
        }
        else
        {
            Vector3 fromv1 = segDirection * distanceFromV1;
            return v1 + fromv1;
        }
    }

    private int GetClosestNode(Vector3 pos)
    {
        int closestNodeIndex = -1;
        float shortestDistance = 0.0f;

        for (int i = 0; i < m_transforms.Count; i++)
        {
            float dist = Vector3.Distance(m_transforms[i].position, pos);
            if(shortestDistance == 0f || dist < shortestDistance)
            {
                shortestDistance = dist;
                closestNodeIndex = i;
            }
        }
        return closestNodeIndex;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < m_transforms.Count - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_transforms[i].position, 0.2f);
            //Gizmos.DrawIcon(m_transforms[i].position, m_transforms[i].gameObject.name, true);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(m_transforms[i].position, m_transforms[i + 1].position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_transforms[m_transforms.Count-1].position, 0.2f);
        //Gizmos.DrawIcon(m_transforms[m_transforms.Count - 1].position, m_transforms[m_transforms.Count - 1].gameObject.name, true);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(m_transforms[m_transforms.Count - 2].position, m_transforms[m_transforms.Count - 1].position);

        
    }
}
