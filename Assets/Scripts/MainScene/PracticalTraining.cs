using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using IRY;
using System.IO;

public class PracticalTraining : MonoBehaviour {

	private JSONNode practicalTrainingProperties;

	void Awake () {
	}

	void Start() {
	}

	public void startProcedure(int id) {
		string procedurePath = "Procedures/" + id;
		/*
		#if !WEBPLAYER
		try {
			string procedure = System.IO.File.ReadAllText(Application.dataPath + "/Resources/Procedures/" + id + ".txt");
			
			if (String.IsNullOrEmpty(procedure) == false) {
				Debug.Log("File for procedure found");
				practicalTrainingProperties = JSON.Parse (procedure);
				initProcedure();
				return;
			}
		} catch(FileNotFoundException) {
			Debug.LogWarning("File not found, will download procedure informations.");
		}
		#endif
		*/
		Debug.LogWarning("Will download procedure informations.");
		StartCoroutine (getProcedure(id));
	}

	IEnumerator getProcedure(int id) {
		string url = IRYConfiguration.getProcedureURL().Replace ("{courseId}", id.ToString());
		
		WWW www = new WWW (url);
		
		yield return www;

		if (String.IsNullOrEmpty(www.error) == true) {
			practicalTrainingProperties = JSON.Parse (www.text);
			/*#if !WEBPLAYER
			System.IO.File.WriteAllText(Application.dataPath + "/Resources/Procedures/" + id + ".txt", www.text);
			#endif*/
			initProcedure();
		} else {
			Debug.LogError("Error white trying to access url : " + url);
			Debug.LogError(www.error);
		}
	}

	private GameObject go;
	void initProcedure() {
		ProcedureScript procedureScript = GetComponent<ProcedureScript> ();
		Procedure procedure = procedureScript.createProcedure ();

		foreach(JSONNode step in practicalTrainingProperties["steps"].AsArray) {
			string goName = step["btn_name"].ToString().Replace('"', ' ').Trim (); // Remove double quotes
			go = GameObject.Find(goName);
			if (go == null) {

				/* Try special commands */
				Debug.Log(goName);

				if (goName.StartsWith("command_see_") == true) {
					Debug.Log(goName + " - " + goName.Substring(12, goName.Length - 12));
					go = GameObject.Find(goName.Substring(12, goName.Length - 12));
					if (go != null) {
						procedure.addLookingObject(go);
						procedure.addId(step["id"].AsInt);
						continue;
					}
				} else if (goName == "command_wait_continuous_engine_speed") {
					procedure.addSpecialBehaviour(goName);
					procedure.addId(step["id"].AsInt);
				}

				Debug.Log(goName + " object not found");
				continue;
			}
			procedure.addButton (go);
			procedure.addId(step["id"].AsInt);
		}
		procedureScript.init ();
		//procedure.init ();
	}
}
