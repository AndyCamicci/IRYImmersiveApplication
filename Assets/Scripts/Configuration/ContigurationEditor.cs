using UnityEngine;
using System.Collections;
using IRY;
using UnityEngine.UI;

public class ContigurationEditor : MonoBehaviour {

	public GameObject baseUrlInputGO;
	private GameObject baseUrlValidGO;
	public string baseUrlValue;

	void Start() {
		baseUrlValue = IRYConfiguration.baseUrl;
		baseUrlValidGO = baseUrlInputGO.transform.parent.transform.Find ("Confirm").gameObject;
		baseUrlValidGO.SetActive (false);
		baseUrlInputGO.GetComponent<InputField> ().text = baseUrlValue;

	}

	public void SaveBaseUrl() {
		baseUrlValue = baseUrlInputGO.GetComponent<InputField> ().text;
		IRYConfiguration.baseUrl = baseUrlValue;
		PlayerPrefs.SetString ("baseUrl", baseUrlValue);
		baseUrlValidGO.SetActive (true);
	}

	public void GoToLogin() {
		Application.LoadLevel ("HomeScene");
	}
}
