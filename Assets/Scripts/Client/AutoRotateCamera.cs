using UnityEngine;
using System.Collections;

public class AutoRotateCamera : MonoBehaviour {

	// Use this for initialization
	public Vector3 v3;
	void Start () {
		v3 = new Vector3 (0, 1, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (v3);
	
	}
}
