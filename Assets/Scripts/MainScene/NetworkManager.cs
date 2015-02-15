using UnityEngine;
using System.Collections;
using System;
using IRY;

public class NetworkManager : MonoBehaviour {


	public void sendStepProcedure (GameObject button, int id, int success)
	{
		Debug.Log (success);
		StartCoroutine ( coroutineSendStepProcedure(button, id, success) );
	}

	IEnumerator coroutineSendStepProcedure(GameObject button, int id, int success)
	{
		Debug.Log (success);
		string url = IRYConfiguration.postStepProcedureURL().Replace ("{stepId}", id.ToString());
		url = url.Replace ("{pilotId}", IRYController.IRYid.ToString());
		url = url.Replace ("{success}", success.ToString());
		Debug.Log ("Send result " + url); 
		WWW www = new WWW (url);
		yield return www;
		
		if (String.IsNullOrEmpty(www.error) == true) {
			Debug.Log("Result saved : " + url);
			Debug.Log(www.text);
		} else {
			Debug.LogError("Error white trying to access url : " + url);
			Debug.LogError(www.error);
		}
	}


}
