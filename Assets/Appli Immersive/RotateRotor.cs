using UnityEngine;
using System.Collections;

public class RotateRotor : MonoBehaviour {

	public float speedRotation = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (-Vector3.up, speedRotation);
	}
}
