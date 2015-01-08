using UnityEngine;
using System.Collections;

public class DebugCamera : MonoBehaviour {
	public Camera cam;
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				Debug.Log ("Name = " + hit.collider.name);
				Debug.Log ("Tag = " + hit.collider.tag);
				Debug.Log ("Hit Point = " + hit.point);
				Debug.Log ("Object position = " + hit.collider.gameObject.transform.position);
				Debug.Log ("--------------");
			}
		}
	}
}