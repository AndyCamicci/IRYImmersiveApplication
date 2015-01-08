using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProcedureScript : MonoBehaviour {
	
	public Procedure[] listProcedures;
	public Procedure activeProcedure;
	
	private GameObject ErrorCanvas;
	private GameObject SuccessCanvas;
	
	public AudioClip errorSound;

	public GameObject messagesCanvas;

	void Awake() {
		ErrorCanvas = messagesCanvas.transform.Find("ERROR").gameObject;
		SuccessCanvas = messagesCanvas.transform.Find("SUCCESS").gameObject;
		gameObject.AddComponent<AudioSource> ();
	}

	void Start () {
		activeProcedure = listProcedures [0];
		activeProcedure.init ();

		messagesCanvas.SetActive (true);
		ErrorCanvas.SetActive (false);
		SuccessCanvas.SetActive (false);
	}

	public void restartProcedure() {
		activeProcedure = listProcedures [0];
		activeProcedure.init ();
		ErrorCanvas.SetActive (false);
		SuccessCanvas.SetActive (false);
	}

	public void buttonChanged(GameObject button, bool state) {
		if (enabled == false) {
			return;
		}
		Debug.Log ("Button pressed : " + button.name + " and next must be : " + activeProcedure.getNextButton ().name);
		
		if (button == activeProcedure.getNextButton()) {
			bool canBeNext = activeProcedure.next();
			if (canBeNext == false) {
				Debug.Log("END OF THE PROCEDURE");
				SuccessCanvas.SetActive(true);
			} else {
				Debug.Log("NEXT STEP : " + activeProcedure.getNextButton().name);
			}
		} else {
			Debug.Log("YOU LOOSE !");
			audio.clip = errorSound;
			audio.Play();
			ErrorCanvas.SetActive(true);
			ErrorCanvas.transform.Find("ERRORMESSAGE").GetComponent<Text>().text = "YOU PUSHED THE BUTTON " + button.name + " INSTEAD OF " + activeProcedure.getNextButton ().name;
		}
		
	}
}


[System.Serializable]
public class Procedure
{
	public GameObject[] buttons;
	public GameObject nextButton;
	private int indexActive;
	
	public GameObject getNextButton() {
		return this.nextButton;
	}
	
	public bool next () {
		if (indexActive + 1 < buttons.Length) {
			indexActive++;
			nextButton = buttons[indexActive];
			return true;
		}
		return false;
	}
	
	public void init () {
		indexActive = 0;
		nextButton = buttons [indexActive];
	}
};