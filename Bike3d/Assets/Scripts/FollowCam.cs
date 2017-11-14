using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
	public Transform camPosition = null;
	public Vector3 lookOffset = new Vector3();
	public Vector3 offset = new Vector3();
	public float springFactor = 10.0f;
	public GameObject followObject = null;

	void Start () {
		camPosition = gameObject.transform;
		if (!followObject) {
			Debug.LogError ("FollowObject not set");
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!followObject) {
			Debug.LogError ("FollowObject not set");
			return;
		}

		// reposition the camera
		Transform chasedObject = followObject.transform;
		Transform chasingObject = gameObject.transform;

		// calculate the worldspace offset positions.
		Vector3 cameraPos = chasingObject.position;
		Vector3 transformedOffset = chasedObject.TransformPoint (offset);
		Vector3 transformedLookOffset = chasedObject.TransformPoint (lookOffset);

		// calculate the distance between the world offset and camera.
		Vector3 offDiff = transformedOffset - cameraPos;
		float dt = Time.deltaTime;

		if (offDiff.magnitude > 0.0001f) {
			if (dt > 0.03f) {
				dt = 0.03f;
			}
			// pull the camera towards the world offset on each step.
			Vector3 newPos = cameraPos + offDiff * springFactor * dt;
			chasingObject.SetPositionAndRotation (newPos, chasingObject.rotation);
		}
		chasingObject.LookAt(transformedLookOffset);
	}
}
