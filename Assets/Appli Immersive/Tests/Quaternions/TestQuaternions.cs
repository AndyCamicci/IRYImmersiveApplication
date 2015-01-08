using UnityEngine;
using System.Collections;

public class TestQuaternions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (transform.rotation);
		Debug.Log (transform.rotation.eulerAngles);
	}
}
