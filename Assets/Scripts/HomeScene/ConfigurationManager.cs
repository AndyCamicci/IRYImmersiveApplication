using UnityEngine;
using System.Collections;
using IRY;

public class ConfigurationManager : MonoBehaviour {

	void Start() {
		startTime = Time.time;
	}

	private float startTime = 0.0f;
	private float maxTime = 1.0f;

	void Update() {
		if (Input.GetKey(KeyCode.F9) == true || Time.time < maxTime + startTime && Input.GetKey(KeyCode.LeftShift) == true) {
			Application.LoadLevel("ConfigurationScene");
			return;
		} 
	}

	void GetConfigFromFile() {
		TextAsset textFile = (TextAsset)Resources.Load("Config", typeof(TextAsset));
		if (textFile == null) {
			Debug.Log("No configuration file found");
			return;
		}

		System.IO.StringReader textStream = new System.IO.StringReader(textFile.text);
		string line;
		while((line = textStream.ReadLine()) != null) {
			if (line.Contains("baseUrl") == true) {
				line = line.Replace(" ", "");
				IRYConfiguration.baseUrl = line.Substring(8, line.Length - 8);
				Debug.Log("Modified ! value : " + IRYConfiguration.baseUrl);
				break;
			}
		}
	}
}
