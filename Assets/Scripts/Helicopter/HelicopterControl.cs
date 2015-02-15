using UnityEngine;
using System.Collections;

public class HelicopterControl : MonoBehaviour {

	private GameObject rotor;

	public float pivotSide = 0.0f;
	public float pivotFront = 0.0f;
	public float throttle = 0.0f;

	void Awake() {
		rotor = GameObject.Find ("ROTOR");
	}

	void FixedUpdate () {
		pivotSide = Mathf.Min (2.0f, pivotSide);
		pivotSide = Mathf.Max (-2.0f, pivotSide);

		pivotFront = Mathf.Min (2.0f, pivotFront);
		pivotFront = Mathf.Max (-2.0f, pivotFront);

		throttle = Mathf.Min (15.0f, throttle);
		throttle = Mathf.Max (0.0f, throttle);

		Vector3 force = new Vector3 (pivotSide, 0, -1 * pivotFront);

		rigidbody.AddRelativeTorque(force);
		rigidbody.AddRelativeForce (Vector3.up * throttle);
		rotor.transform.Rotate (Vector3.up * throttle * 2);
	}
}
