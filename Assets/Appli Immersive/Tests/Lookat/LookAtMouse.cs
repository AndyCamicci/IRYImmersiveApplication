using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

	public Transform target;
	void Update() {
		transform.LookAt(target);
	}

}