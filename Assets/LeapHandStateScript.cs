using UnityEngine;
using System.Collections;
using Leap;
public class LeapHandStateScript : MonoBehaviour {

	public float grabMinValue = 0.6f;
	public bool isGrabbing = false;
	private Hand handGrabbing;

	private Controller LeapController;
	// Use this for initialization
	void Start () {
		LeapController = GetComponent<HandController> ().GetLeapController ();
	}
	
	// Update is called once per frame
	void Update () {
		HandList hands = LeapController.Frame ().Hands;
		foreach (Hand hand in hands) {
			//Debug.Log (hand.GrabStrength);
			isGrabbing = (hand.GrabStrength >= grabMinValue);
			handGrabbing = hand;
		}
	
	}

	public Controller getController() {
		return LeapController;
	}

	public Hand getHandGrabbing() {
		return isGrabbing ? handGrabbing : null;
	}
}
