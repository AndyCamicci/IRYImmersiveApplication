using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IRYWaitingRoomAutoSetNameScript : MonoBehaviour {

	private GameObject MainController;
	private string name = "Pilot name";

	void Awake() {
		MainController = GameObject.Find ("MAINCONTROLLER");
	}

	void Start () {
		if (MainController != null) {
			name = MainController.GetComponent<IRYMainControllerScript>().name;
		}
		string initialMessage = GetComponent<Text> ().text;
		GetComponent<Text> ().text = initialMessage + name;
	}
}
