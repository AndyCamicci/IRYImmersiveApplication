using UnityEngine;
using System.Collections;

public class ConnexionScript : MonoBehaviour {
	void Start () {
		string url = "http://localhost:8000/rest/helicopter/1";
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));

	}
	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
			//JSONObject j = new JSONObject(www.data);
			//Debug.Log (j);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
}