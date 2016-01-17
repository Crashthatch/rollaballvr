using UnityEngine;
using System.Collections;
using VR = UnityEngine.VR;

public class RotateWithVRHead : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localRotation = VR.InputTracking.GetLocalRotation(VR.VRNode.CenterEye);
	}
}
