using UnityEngine;
using System.Collections;

public class FistPersonScript : MonoBehaviour {

	public GameObject centerEyeOVR;
	public GameObject leapController;
	public GameObject headModel;

	public Quaternion initialHeadRotation;

	void Start() {
		initialHeadRotation = centerEyeOVR.transform.rotation;
	}

	void FixedUpdate () {
		headModel.transform.rotation = centerEyeOVR.transform.rotation * initialHeadRotation;
	}
}
