using UnityEngine;
using System.Collections;

public class ComplexInteractionLever : MonoBehaviour {

	private LeapHandStateScript leap;

	public Vector3 customWorldUp = Vector3.up;

	public Vector3 minRotation;
	public Vector3 maxRotation;

	private bool interacting = false;

	void Awake() {
		leap = GameObject.Find ("HandController").GetComponent<LeapHandStateScript> ();
		gameObject.AddComponent<MeshCollider> ();
	}

	void FixedUpdate() {
		if (Input.GetMouseButton(0) || interacting && leap.isGrabbing) {
			//Vector3 pp = new Vector3(leap.getHandGrabbing().PalmPosition.x, leap.getHandGrabbing().PalmPosition.y, leap.getHandGrabbing().PalmPosition.z);
			Vector3 pp = GameObject.Find ("HandContainer").transform.position;

			pp.y = 90;
			pp.z = 180;
			//Vector3 _direction = (pp - transform.position).normalized;
			//Quaternion _lookRotation = Quaternion.LookRotation(_direction);
			//transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 3.0f);
			transform.LookAt(pp, customWorldUp);
			
			//Vector3 pp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//transform.LookAt(pp, customWorldUp);
			//transform.localRotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up) * Quaternion.LookRotation(pp);

			//Debug.Log(leap.getHandGrabbing().PalmPosition);
		}
	}
	void OnCollisionEnter (Collision col)
	{ 
		makeTransition ();
	}

	void OnMouseDown() {
		makeTransition ();
	}

	void makeTransition ()
	{
		interacting = true;
	}
}
