using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using IRY;
using System;
using SimpleJSON;

public class WaitingRoomControllerScript : MonoBehaviour {

	void Start() {
		setName ();
		InvokeRepeating ("getInstructions", 0.0f, 2.0f);
	}


	void setName() {
		Text user = GameObject.Find ("User").transform.Find ("Text").GetComponent<Text> ();
		user.text = user.text + IRYController.IRYname;
		
	}

	public void getInstructions() {
		StartCoroutine (coroutineGetInstructions ());
	}

	IEnumerator coroutineGetInstructions() {
		string url = IRYConfiguration.getInstructionsURL().Replace ("{id}", IRYController.IRYserie.ToString ());
		WWW www = new WWW (url);
		yield return www;
		
		if (String.IsNullOrEmpty(www.error) == true) {
			JSONNode data = JSON.Parse(www.text);
			string instruction = data["instruction"];
			if (String.IsNullOrEmpty(instruction) == false) {
				if (instruction.Contains(IRYConfiguration.STARTPRACTICALTRAINING)) {
					CancelInvoke("getInstructions");
					int startIndex = IRYConfiguration.STARTPRACTICALTRAINING.Length;
					int endIndex = instruction.Length - IRYConfiguration.STARTPRACTICALTRAINING.Length;
					int course = int.Parse( instruction.Substring(startIndex, endIndex) );
					IRYController.IRYcourse = course;
					IRYController.typeCourse = TypeCourse.PracticalTraining;
					Application.LoadLevel("MainScene");
				
				} else if (instruction.Contains(IRYConfiguration.STARTDEMONSTRATIVECOURSE)) {
					CancelInvoke("getInstructions");
					int startIndex = IRYConfiguration.STARTDEMONSTRATIVECOURSE.Length;
					int endIndex = instruction.Length - IRYConfiguration.STARTDEMONSTRATIVECOURSE.Length;
					int course = int.Parse( instruction.Substring(startIndex, endIndex) );
					IRYController.IRYcourse = course;
					IRYController.typeCourse = TypeCourse.DemonstrativeCourse;
					Application.LoadLevel("MainScene");
				} else if (instruction.Contains(IRYConfiguration.STARTIMMERSIVEMOVIE)) {
					CancelInvoke("getInstructions");
					int startIndex = IRYConfiguration.STARTIMMERSIVEMOVIE.Length;
					int endIndex = instruction.Length - IRYConfiguration.STARTIMMERSIVEMOVIE.Length;
					Debug.Log(instruction);
					Debug.Log(instruction.Substring(startIndex, endIndex));
					int course = int.Parse( instruction.Substring(startIndex, endIndex) );
					IRYController.IRYcourse = course;
					IRYController.typeCourse = TypeCourse.ImmersiveMovie;
					Application.LoadLevel("ImmersiveVideo");
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
}

