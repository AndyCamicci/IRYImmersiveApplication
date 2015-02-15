using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using IRY;
using System;
using SimpleJSON;


public class HighlightItem : MonoBehaviour {

	public Material activeShader;
	public GameObject highlightImage;
	public AudioClip clickSound;

	private GameObject currentGo = null;
	private GameObject lastGO = null;
	private Material lastMat = null;

	void Awake() {
		gameObject.AddComponent<AudioSource> ();
	}
	public void Start() {
		StartGetInstructions ();
	}


	public void StartGetInstructions() {
		InvokeRepeating ("getInstructions", 0.0f, 5.0f);
	}

	public void getInstructions() {
		StartCoroutine (CoroutineGetInstructions());
	}

	private int maxConnectionErrors = 5;
	private int currentConnectionErrors = 0;

	IEnumerator CoroutineGetInstructions() {
		string url = IRYConfiguration.getInstructionsURL().Replace ("{id}", IRYController.IRYserie.ToString ());
		WWW www = new WWW (url);
		yield return www;
		
		if (String.IsNullOrEmpty(www.error) == true) {
			JSONNode data = JSON.Parse(www.text);
			string instruction = data["instruction"];
			Debug.Log(instruction);
			if (String.IsNullOrEmpty(instruction) == false) {
				if (instruction.Contains(IRYConfiguration.COMMAND_SHOWBTN)) {
					Debug.Log(instruction);
					int startIndex = IRYConfiguration.COMMAND_SHOWBTN.Length;
					int endIndex = instruction.Length - IRYConfiguration.COMMAND_SHOWBTN.Length;
					string btn = instruction.Substring(startIndex + 1, endIndex - 1);
					SetActive(btn);
				} else if (instruction.Contains(IRYConfiguration.COMMAND_GOTO_WAITING)) {
					Debug.Log("Go back to waiting scene");
					Application.LoadLevel("WaitingRoomScene");
					CancelInvoke("getInstructions");
				} else {
					Debug.Log ("Instruction unknown : " + instruction);
				}
			} else {
				Debug.Log ("No instruction for the serie " + IRYController.IRYserie);
			}
			
		} else {
			Debug.LogError("Error when trying to access to " + url);
			Debug.LogError(www.error);
			currentConnectionErrors++;
			if (currentConnectionErrors > maxConnectionErrors) {
				Debug.Log("Too much failed errors, abording.");
				CancelInvoke("getInstructions");
			}
		}
	}

	public void SetActive(string objectName) {
		currentGo = GameObject.Find(objectName);
		if (currentGo != null) {
			if (lastGO != null) {
				lastGO.renderer.material = lastMat;
			}
			
			lastGO = currentGo;
			lastMat = currentGo.renderer.material;
			
			//currentGo.renderer.material = activeShader;
			//currentGo.renderer.material.SetColor("_Color", new Color32(15,167,183,1));
			//currentGo.renderer.material.SetColor("_OutlineColor", new Color32(15,167,183,255));

			if (highlightImage != null) {
				highlightImage.transform.Find ("Text").GetComponent<Text>().text = objectName;
				highlightImage.transform.root.GetComponent<CanvasScaler>().scaleFactor = 1.5f;
			}

			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = clickSound;
			audio.loop = false;
			audio.Play();

		} else {
			Debug.Log(objectName + " object not found for highlight");
		}
		
	}

	void FixedUpdate() {

		if (currentGo != null && highlightImage != null) {
			//Vector3 screenPoint = Camera.main.WorldToScreenPoint(currentGo.transform.position);
			highlightImage.active = true;
			Vector3 modified = currentGo.transform.position;
			modified.x += 0.02f;
			highlightImage.transform.position = modified;
			/*modified = new Vector3(-1,1,1);
			highlightImage.transform.LookAt (Camera.main.transform);
			highlightImage.transform.localScale = modified;		*/	
			
			//highlightImage.transform.position = screenPoint;
		}
	}



}
