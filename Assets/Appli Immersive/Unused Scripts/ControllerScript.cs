using UnityEngine;
using System.Collections;

public class ControllerScript : MonoBehaviour {
	public string buttonTag = "PushButtons";

	private GameObject[] buttons;

	void Awake()
	{
		buttons = GameObject.FindGameObjectsWithTag (buttonTag);

		foreach (GameObject btn in buttons) {
	//		btn.AddComponent("SimpleInteraction");
		}
	}
}
