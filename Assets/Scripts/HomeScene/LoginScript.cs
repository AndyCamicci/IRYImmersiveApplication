using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;
using System;
using IRY;

public class LoginScript : MonoBehaviour {

	public GameObject Options;
	public GameObject Option;
	public GameObject UIErrorMessage;

	public void Awake()
	{
		//Application.LoadLevel ("PracticalTraining");
		if (UIErrorMessage != null) {
			UIErrorMessage.SetActive(false);
		}
	}

	public void Start()
	{
		//StartCoroutine (getSeries());
	}

	public void GetSeriesOptions() {
		StartCoroutine (getSeries());
	}

	private void SetSerie(GameObject obj, JSONNode serie) { 
		obj.GetComponent<Button>().onClick.AddListener(() => {
			IRYController.IRYserie = serie["id"].AsInt;
			Options.SetActive(false);
			GameObject.Find ("Select").transform.Find ("Text").GetComponent<Text>().text = serie["name"];
		});
	}

	IEnumerator getSeries() {
		string url = IRYConfiguration.getSeriesURL();
		Debug.Log (url);
		WWW www = new WWW (url);
		yield return www;
		if (String.IsNullOrEmpty(www.error) == true) {
			JSONNode data = JSON.Parse (www.text);
			JSONArray series = data.AsArray;

			int childIndex = 0;
			foreach(Transform child in Options.transform) {
				if (childIndex > 0) {
					GameObject.Destroy(child.gameObject);
				}
				childIndex++;
			}

			for(int i=0; i < series.Count; i++) {
				GameObject obj = Instantiate(Option) as GameObject;
				obj.SetActive(true);
				obj.transform.SetParent(Options.transform);
				obj.transform.Find("Text").GetComponent<Text>().text = series[i]["name"];
				SetSerie(obj, series[i]);
			}
			if (series.Count > 0) {
				Options.SetActive(true);
			} else {
				showError("No active serie has been found");
			}

		} else {
			showError(www.error);
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
		IRYController.IRYid = 2;
		IRYController.IRYname = "Andy";
		goToWaitingRoom();
	}


	IEnumerator PostUsername(string username)
	{
		string url = IRYConfiguration.addPilotURL();

		WWWForm form = new WWWForm();
		form.AddField ("pilot[name]", username);
		form.AddField ("pilot[serie]", IRYController.IRYserie);
		form.AddField ("_format", "json");
		WWW www = new WWW (url, form);

		yield return www;

		if (String.IsNullOrEmpty(www.error) == true) {
			JSONNode data = JSON.Parse (www.text);
			IRYController.IRYid = data ["id"].AsInt;
			IRYController.IRYname = data ["name"].Value;

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
		if (UIErrorMessage != null) {
			UIErrorMessage.GetComponent<Text>().text = "An error has occured. Please try again later. (" + error + ")";
			UIErrorMessage.SetActive(true);
		}
		Debug.LogError(error);
	}

	public void goToWaitingRoom()
	{
		Application.LoadLevel ("WaitingRoomScene");
	}

}
