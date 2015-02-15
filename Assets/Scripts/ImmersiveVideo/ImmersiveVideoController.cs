using UnityEngine;
using System.Collections;
using IRY;
using System;
using SimpleJSON;
using UnityEngine.UI;

public class ImmersiveVideoController : MonoBehaviour {

	public GameObject schema;

	public void Start() {
		StartGetInstructions ();
	}

	public void StartGetInstructions() {
		InvokeRepeating ("getInstructions", 0.0f, 5.0f);
	}

	public void getInstructions() {
		StartCoroutine (CoroutineGetInstructions());
	}

	private string lastInstruction = "";
	IEnumerator CoroutineGetInstructions() {
		string url = IRYConfiguration.getInstructionsURL().Replace ("{id}", IRYController.IRYserie.ToString ());
		WWW www = new WWW (url);
		yield return www;
		
		if (String.IsNullOrEmpty(www.error) == true) {
			JSONNode data = JSON.Parse(www.text);
			string instruction = data["instruction"];
			if (String.IsNullOrEmpty(instruction) == false) {
				if (instruction == lastInstruction) {
					Debug.Log("Same as last instruction, abording.");
					return false ;
				} else {
					lastInstruction = instruction;
				}
				Debug.Log(instruction);
				
				if (instruction.Contains(IRYConfiguration.COMMAND_SHOWIMAGE)) {
					int startIndex = IRYConfiguration.COMMAND_SHOWIMAGE.Length;
					int endIndex = instruction.Length - IRYConfiguration.COMMAND_SHOWIMAGE.Length;
					string img = instruction.Substring(startIndex + 1, endIndex - 1);
					if (img.StartsWith("http") == false) {
						img = IRYConfiguration.baseUrl + img;
					}
					Debug.Log("Will load the image " + img);
					StartCoroutine(loadImage(img));

				} else if (instruction.Contains(IRYConfiguration.COMMAND_CHANGE_SPEED)) {
					int startIndex = IRYConfiguration.COMMAND_CHANGE_SPEED.Length;
					int endIndex = instruction.Length - IRYConfiguration.COMMAND_CHANGE_SPEED.Length;
					string ts = instruction.Substring(startIndex + 1, endIndex - 1);
					GameObject.Find("NETWORK").GetComponent<WebPlayerMethods>().SetTimeScale(ts);

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
		}
	}

	IEnumerator loadImage(string url) {
		WWW www = new WWW(url);

		yield return www;

		if (String.IsNullOrEmpty(www.error) == true) {
		
			Debug.Log ("OK");
			Debug.Log (www.text);
			schema.SetActive (true);
			schema.GetComponent<RawImage>().texture = www.texture;
		} else {
			Debug.LogError(www.error);
		}

	}
}
