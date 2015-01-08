using UnityEngine;
using System.Collections;
using System.Net;

public class IRYMainControllerScript : MonoBehaviour {

	public int id;
	public string name;

	private IRYMainConfigurationScript CONFIG;

	void Awake()
	{
		CONFIG = GetComponent<IRYMainConfigurationScript>();
		DontDestroyOnLoad (gameObject);
	}

	void OnApplicationQuit() {
		// REMOVE THE PLAYER FROM THE DATABASE
		string deleteUrl = CONFIG.deletePilotURL.Replace ("{id}", id.ToString());
		WebRequest webRequest = WebRequest.Create(deleteUrl);
		webRequest.Method = "DELETE";
		WebResponse response = webRequest.GetResponse ();
		Debug.Log(response);
	}

}
