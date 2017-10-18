using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMover : MonoBehaviour {

    public Rail rail;
    public Transform lookAt;

    private Transform thisTransform;

	// Use this for initialization
	void Start () {
        thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        thisTransform.position = rail.ProjectionOnRail(lookAt.position);

        thisTransform.LookAt(lookAt.position);
	}
}
