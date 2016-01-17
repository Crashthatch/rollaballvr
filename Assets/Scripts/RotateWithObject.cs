using UnityEngine;
using System.Collections;

public class RotateWithObject : MonoBehaviour {
	public GameObject sourceObject;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 srcRot = sourceObject.transform.localRotation.eulerAngles;
		srcRot.y = 0;
		//srcRot.x *= -1;
		this.transform.eulerAngles = srcRot;
	}
}
