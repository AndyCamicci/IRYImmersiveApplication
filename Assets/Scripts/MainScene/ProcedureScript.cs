using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Leap;
using System;
using IRY;
using SimpleJSON;

public class ProcedureScript : MonoBehaviour {
	
	public List<Procedure> listProcedures = new List<Procedure>();
	public Procedure activeProcedure;
	
	private GameObject ErrorCanvas;
	private GameObject SuccessCanvas;
	
	public AudioClip errorSound;
	public AudioClip successSound;

	public GameObject messagesCanvas;

	public bool readyForAction = true;
		
	private Controller controller;
	void Awake() {
		ErrorCanvas = messagesCanvas.transform.Find("ERROR").gameObject;
		SuccessCanvas = messagesCanvas.transform.Find("SUCCESS").gameObject;
		gameObject.AddComponent<AudioSource> ();
		controller = new Controller ();
	}

	void Start () {
		/*activeProcedure = listProcedures [0];
		activeProcedure.init ();*/

		messagesCanvas.SetActive (true);
		ErrorCanvas.SetActive (false);
		SuccessCanvas.SetActive (false);
	}

	public Procedure createProcedure() {
		Procedure procedure = new Procedure ();
		this.listProcedures.Add(procedure);

		return procedure;
	}

	public void init() {
		if (listProcedures.Count > 0) {
			activeProcedure = listProcedures [0];
			activeProcedure.init ();
		}
	}

	public void restartProcedure() {
		currentProgress = 0.0f;
		activeProcedure = listProcedures [0];
		activeProcedure.init ();
		ErrorCanvas.SetActive (false);
		SuccessCanvas.SetActive (false);
	}

	public void buttonChanged(GameObject button, bool state) {
		if (enabled == false) {
			return;
		}
		//	Debug.Log ("Button pressed : " + button.name + " and next must be : " + activeProcedure.getNextButton ().name);
		
		if (button == activeProcedure.getNextButton()) {
			bool canBeNext = activeProcedure.next();
			if (canBeNext == false) {
				Debug.Log("END OF THE PROCEDURE");
				SuccessCanvas.SetActive(true);
				audio.clip = successSound;
				audio.Play();
				GetComponent<NetworkManager>().sendStepProcedure(
					activeProcedure.buttons[activeProcedure.indexActive], 
					activeProcedure.steps[activeProcedure.indexActive], 
					1);
			} else {
				Debug.Log("NEXT STEP : " + activeProcedure.getNextButton().name);
				GetComponent<NetworkManager>().sendStepProcedure(
					activeProcedure.buttons[activeProcedure.indexActive], 
					activeProcedure.steps[activeProcedure.indexActive], 
					1);
			}
		} else {
			Debug.Log("YOU LOOSE !");
			GetComponent<NetworkManager>().sendStepProcedure(
				activeProcedure.buttons[activeProcedure.indexActive], 
				activeProcedure.steps[activeProcedure.indexActive], 
				0);
			audio.clip = errorSound;
			audio.Play();
			ErrorCanvas.SetActive(true);
			ErrorCanvas.transform.Find("ERRORMESSAGE").GetComponent<Text>().text = "YOU PUSHED THE BUTTON " + button.name + " INSTEAD OF " + activeProcedure.getNextButton ().name;
			//ErrorCanvas.transform.Find("ERRORMESSAGE").GetComponent<Text>().text = "YOU PUSHED THE BUTTON PITOT INSTEAD OF GENE";
		}
		
	}

	private float maxProgressionRestart = 0.0f;
	private float currentProgress = 0.0f;
	private RectTransform progress = null;

	private GameObject palm;
	private Vector3 palmPos;
	private float xPercent;
	private float yPercent;

	public bool call = false;
	public bool restart = false;

	public GameObject callBtn;
	public Sprite callBtnInactive;
	public Sprite callBtnActive;
	public Sprite callBtnWaiting;

	public GameObject callBtnText;
	/*private Vector2 ppos;

	Vector2 percentPosition(GameObject palm) {
		palmPos = Camera.main.WorldToScreenPoint(palm.transform.position);
		xPercent = palmPos.x * 100 / UnityEngine.Screen.width;
		yPercent = palmPos.y * 100 / UnityEngine.Screen.height;

		return new Vector2 (xPercent, yPercent);

	}*/

	bool callingInstructor = false;

	void FixedUpdate() {

		palm = GameObject.Find ("palm");
		
		if (palm != null) {
			palmPos = palm.transform.position;
			//palmPos.y = 0;


			Vector3 callBtnPos = callBtn.transform.position;
			//callBtnPos.y = 0;
			
			call = Vector3.Distance(callBtnPos, palmPos) < 0.2f;
			//Debug.Log(call);
			
		} else {
			call = false;
			restart = false;
		}


		if (ErrorCanvas.activeInHierarchy == true) {
			if (progress == null) {
				progress = ErrorCanvas.transform.Find("Progression").GetComponent<RectTransform>();
			}
			if (maxProgressionRestart == 0.0f) {
				maxProgressionRestart = progress.sizeDelta.x;
			}

			Vector2 newSize = progress.sizeDelta;
			newSize.x = currentProgress;
			progress.sizeDelta = newSize;

			if (restart == true) {
				currentProgress += 3;
			} else {
				currentProgress -= 1;
			}

			if (currentProgress >= maxProgressionRestart) {
				restartProcedure();
			}


			Vector3 restartBtnPos = ErrorCanvas.transform.Find("Progression").transform.position;
			restartBtnPos.y = 0;
			restart = Vector3.Distance(restartBtnPos, palmPos) < 100;
		}

		if (SuccessCanvas.activeInHierarchy == true) {
			if (progress == null) {
				progress = SuccessCanvas.transform.Find("Progression").GetComponent<RectTransform>();
			}
			if (maxProgressionRestart == 0.0f) {
				maxProgressionRestart = progress.sizeDelta.x;
			}
			
			Vector2 newSize = progress.sizeDelta;
			newSize.x = currentProgress;
			progress.sizeDelta = newSize;
			
			if (restart == true) {
				currentProgress += 3;
			} else {
				currentProgress -= 1;
			}
			
			if (currentProgress >= maxProgressionRestart) {
				restartProcedure();
			}
			
			
			Vector3 restartBtnPos = SuccessCanvas.transform.Find("Progression").transform.position;
			restartBtnPos.y = 0;
			restart = Vector3.Distance(restartBtnPos, palmPos) < 100;
		}


		currentProgress = Mathf.Min(currentProgress, maxProgressionRestart);
		currentProgress = Mathf.Max(currentProgress, 0);

		if (callingInstructor == false && call) {
			callBtn.GetComponent<UnityEngine.UI.Image>().sprite = callBtnWaiting;
			callingInstructor = true;
			callBtnText.SetActive(true);
			callInstructor();
		} else if (callingInstructor == false && call == false) {
			callBtn.GetComponent<UnityEngine.UI.Image>().sprite = callBtnInactive;
			callBtnText.SetActive(false);
		}

		GameObject hc = GameObject.Find("HandController");
		
		if (hc != null) {
			palm = GameObject.Find ("palm");
			if (palm != null) {

				/*GameObject[] gos = GameObject.FindGameObjectsWithTag ("Push Button");
				
				for (int i = 0; i < gos.Length; i++) {
					Debug.Log(Vector3.Distance(gos[i].transform.position, palm.transform.position));
				}*/
				//HandList hands = controller.Frame().Hands;
				/*Color transparent = new Color32(255,255,255,1);
				transparent.a = hands[0].Confidence;
				palm.transform.root.renderer.material.color = transparent;*/
				//Debug.Log(Vector3.Distance(hc.transform.position, palm.transf*orm.position));
			}
		}

	}

	void callInstructor() {
		Debug.Log ("Call instructor");

		StartCoroutine (coroutineCallInstructor ());

	}

	IEnumerator coroutineCallInstructor() {
		string url = IRYConfiguration.callInstructorURL ().Replace ("{id}", IRYController.IRYid.ToString());

		WWW www = new WWW (url);

		yield return www;
		if (String.IsNullOrEmpty(www.error) == true) {
			
		} else {
			Debug.LogError("Error when trying to access to " + url);
			Debug.LogError(www.error);
		}

	}
}


[System.Serializable]
public class Procedure
{
	public List<GameObject> buttons = new List<GameObject>();
	public List<int> steps = new List<int>();

	private GameObject nextButton;
	public int indexActive;
	
	public GameObject getNextButton() {
		return this.nextButton;
	}
	
	public bool next () {
		if (indexActive + 1 < buttons.Count) {
			indexActive++;
			nextButton = buttons[indexActive];

			// Special conditions
			if (nextButton.GetComponent<InteractableLookingObject>() != null) {
				nextButton.GetComponent<InteractableLookingObject>().canInteract = true;
			}
			return true;
		}
		return false;
	}
	
	public void init () {
		Debug.Log ("Procedure initialization");
		if (buttons.Count == 0) {
			Debug.LogError("The procedure is empty !");
		} else {
			indexActive = -1;
			next();
		}
	}

	public Procedure addButton(GameObject button) {
		this.buttons.Add (button);
		return this;
	}
	public Procedure addLookingObject(GameObject go) {
		this.buttons.Add (go);
		go.AddComponent<InteractableLookingObject>();
		return this;
	}
	public Procedure addSpecialBehaviour(string name) {
		// command_wait_continuous_engine_speed
		//this.buttons.Add ();
		return this;
	}

	public Procedure addId(int id) {
		this.steps.Add (id);
		return this;
	}
};