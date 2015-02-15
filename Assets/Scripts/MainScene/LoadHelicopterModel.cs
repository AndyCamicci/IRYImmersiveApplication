using UnityEngine;
using System.Collections;
using System.Net;
using System;
using SimpleJSON;
using IRYUtils;
using System.IO;
using Ionic.Zip;
using IRY;
using System.Text;
using System.ComponentModel;

public class LoadHelicopterModel : MonoBehaviour {

	private GameObject helicopter;
	bool canDownloadHelicopter = true;
	bool helicopterZipDownloaded = false;
	
	public void instantiateHelicopter() {
		if (IRYController.helicopterProperties == null) {

			StartCoroutine (getHelicopterInfos ());

		} else {

			if (helicopter == null) {
				Debug.LogError("Error when generating GameObject");
			} else {
				GameObject obj = (GameObject)Instantiate(helicopter);
				obj.transform.SetParent(GameObject.Find("HELICOPTER").transform);

				GetComponent<InteractionsScript>().addScripts();
				GetComponent<PracticalTraining>().startProcedure(2);
			}

		}
	}

	/* Used to get the model of the helicopter in this course */
	IEnumerator getHelicopterInfos() {
		string heliInfosUrl = IRYConfiguration.getHelicopterURL().Replace ("{id}", "3");//IRYController.IRYserie.ToString());
		WWW www = new WWW (heliInfosUrl);
		
		yield return www;
		if (String.IsNullOrEmpty(www.error) == true) {
			IRYController.helicopterProperties = JSON.Parse (www.text);
			Debug.Log(IRYController.helicopterProperties);
			LoadHelicopter();
			
		} else {
			Debug.LogError("Error when trying to access to " + heliInfosUrl);
			Debug.LogError(www.error);
		}
	}

	/* Load helicopter if model already exists, download it if not. */
	void LoadHelicopter() {
		string helicopterPath = "Helicopters/" + IRYController.helicopterProperties ["name"] + "/" + IRYController.helicopterProperties ["data"]["fileName"];

		helicopter = (GameObject)Resources.Load(helicopterPath, typeof(GameObject));

		if (canDownloadHelicopter == true && helicopter == null) {

			downloadHelicopter();

		} else {
			instantiateHelicopter();
		}
	}


	private void downloadHelicopter() {

		string url = IRYController.helicopterProperties["data"]["downloadLink"];
		WebClient webClient = new WebClient();
		webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
		webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
		
		string zipFilePath = Application.dataPath + "/temp/" + IRYController.helicopterProperties["name"] + ".zip";
		webClient.DownloadFileAsync(new Uri(url), @zipFilePath);
	}


	private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
	{
		//Debug.Log (e.ProgressPercentage);
	}
	
	private void Completed(object sender, AsyncCompletedEventArgs e)
	{
		Debug.Log ("Download completed!");
		helicopterZipDownloaded = true;
	}

	private void unzipHelicopter() {

		string zipFilePath = Application.dataPath + "/temp/" + IRYController.helicopterProperties["name"] + ".zip";
		ZipFile zip = new ZipFile (zipFilePath);
		zip.ExtractAll(Application.dataPath + "/Resources/Helicopters/" + IRYController.helicopterProperties["name"] + "/", ExtractExistingFileAction.OverwriteSilently);
		Debug.Log ("Extracted");
	}

	void Update() {
		if (canDownloadHelicopter == true && helicopterZipDownloaded == true) {
			unzipHelicopter();
			LoadHelicopter();

			canDownloadHelicopter = false;
		}
	}


}
