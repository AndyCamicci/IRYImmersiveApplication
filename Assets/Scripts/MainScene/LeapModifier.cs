using UnityEngine;
using System.Collections;
using Leap;

public class LeapModifier : MonoBehaviour {

	private GameObject palm1;
	private GameObject palm2;
	private GameObject vhand;
	public GameObject SimpleHand;

	/*void FixedUpdate () {
		return;
		palm1 = GameObject.Find ("Bip01 R Finger046");
		palm2 = GameObject.Find ("Bip01 R Finger1");
		if (palm1 != null) {
			vhand = palm1;
		} else if (palm2 != null) {
			vhand = palm2;
		} else {
			vhand = null;
		}
		if (vhand != null) {
			SimpleHand.SetActive(true);
			SimpleHand.transform.localPosition = vhand.transform.position;
			SimpleHand.transform.localRotation = vhand.transform.rotation;
		} else {
			SimpleHand.SetActive(false);
		}
	}*/



}
