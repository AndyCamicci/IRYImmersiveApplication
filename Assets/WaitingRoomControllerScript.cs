using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaitingRoomControllerScript : MonoBehaviour {

	public bool change;

	private GameObject waitingButton;
	private Text waitingButtonText;

	private Color activeColor;
	private Color inactiveColor;

	private string waitingText = "Waiting for instructions...";

	private bool isWaiting = true;

	void Awake() {
		waitingButton = GameObject.Find ("InstructionsButton");
		waitingButtonText = waitingButton.transform.Find("Text").GetComponent<Text> ();

		activeColor = new Color (15, 167, 183);
		inactiveColor = Color.gray;
	}

	void Start() {
		setIsWaiting (true);
	}

	void Update() {
		setIsWaiting(change);
	}

	void setIsWaiting(bool wait) {
		isWaiting = wait;

		if (wait == false) {
			waitingButtonText.text = "Starting course...";
			waitingButton.GetComponent<Image> ().color = activeColor;
		} else {
			waitingButtonText.text = waitingText;
			waitingButton.GetComponent<Image> ().color = inactiveColor;
		}

	}

}

