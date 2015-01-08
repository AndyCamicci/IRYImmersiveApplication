using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System;

public class LoginScript : MonoBehaviour {

	private IRYMainControllerScript MAINCONTROLLER;
	private IRYMainConfigurationScript CONFIG;

	public GameObject UIErrorMessage;

	public void Awake()
	{
		MAINCONTROLLER = GameObject.Find ("MAINCONTROLLER").GetComponent<IRYMainControllerScript>();
		CONFIG = GameObject.Find ("MAINCONTROLLER").GetComponent<IRYMainConfigurationScript>();
	
		if (UIErrorMessage != null) {
			UIErrorMessage.SetActive(false);
		}
	}

	public void Connect()
	{
		string username = GameObject.Find ("Username").transform.Find ("Text").GetComponent<Text> ().text;
		hideError ();

		StartCoroutine (PostUsername(username)); //@TODO Uncomment and delete next line
		//TempPostUsername (username);
	}

	private void TempPostUsername(string username)
	{
		MAINCONTROLLER.id = 2;
		MAINCONTROLLER.name = "Andy";
		goToWaitingRoom();
	}


	IEnumerator PostUsername(string username)
	{
		string url = CONFIG.addPilotURL;
		WWWForm form = new WWWForm();
		form.AddField ("pilot[name]", username);
		form.AddField ("_format", "json");
		WWW www = new WWW (url, form);
		yield return www;

		if (String.IsNullOrEmpty(www.error) == true) {
			JSONNode data = JSON.Parse (www.data);
			MAINCONTROLLER.id = data ["id"].AsInt;
			MAINCONTROLLER.name = data ["name"].Value;

			goToWaitingRoom();

		} else {
			showError(www.error);
		}

	}

	public void hideError()
	{
		if (UIErrorMessage != null) {
			UIErrorMessage.SetActive(false);
		}
	}
	public void showError(string error)
	{
		if (UIErrorMessage == null) {
			Debug.LogError(error);
		} else {
			UIErrorMessage.GetComponent<Text>().text = "An error has occured. Please try again later. (" + error + ")";
			UIErrorMessage.SetActive(true);
		}
	}

	public void goToWaitingRoom()
	{
		Application.LoadLevel ("WaitingRoom");
	}

}
