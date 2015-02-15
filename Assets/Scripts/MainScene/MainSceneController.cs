using UnityEngine;
using System.Collections;
using IRY;
using System;
using SimpleJSON;

public class MainSceneController : MonoBehaviour {

	public bool loadHelicopterModelOnStartup = false;

	void Awake() {
	}

	void Start() {

		if (IRYController.typeCourse == TypeCourse.PracticalTraining)
		{
			if (loadHelicopterModelOnStartup == true) {
				GetComponent<LoadHelicopterModel> ().instantiateHelicopter();
			} else {
				GetComponent<InteractionsScript>().addScriptsUsingTags();
				GetComponent<PracticalTraining>().startProcedure (IRYController.IRYcourse);
			}

			GetComponent<ProcedureScript>().callBtn.SetActive(true);
			GetComponent<ProcedureScript>().callBtnText = GetComponent<ProcedureScript>().callBtn.transform.Find("Text").gameObject;
			GetComponent<ProcedureScript>().callBtnText.SetActive(false);
		}

		if (IRYController.typeCourse == TypeCourse.DemonstrativeCourse)
		{
			//GetComponent<HighlightItem>().SetActive("btn_OFF1");
		}



		//GetComponent<PracticalTraining>().startProcedure (2);
		//StartCoroutine (coroutineGetInstructions ());
		//StartCoroutine (getCourseInstructions (IRYController.IRYcourse));
	}

	IEnumerator getCourseInstructions(int id) {
		string url = IRYConfiguration.getCourseInstructionsURL().Replace ("{id}", id.ToString());
		WWW www = new WWW (url);

		yield return www;

		if (String.IsNullOrEmpty(www.error) == true) {
			string data = JSON.Parse (www.text);
			Debug.Log(www.text);
		} else {
			Debug.LogError("Error white trying to access url : " + url);
			Debug.LogError(www.error);
		}
	}

}
