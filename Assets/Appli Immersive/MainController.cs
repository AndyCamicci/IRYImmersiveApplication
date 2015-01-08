using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public bool doProcedures = true;
	public bool oculusMode = false;
	public bool leapOnOculus = true;

	void Awake() {
		if (doProcedures == false) {
			GetComponent<ProcedureScript>().enabled = false;
		}

		if (oculusMode) {
			GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>().enabled = true;
			GameObject.Find("OVRCameraRig").GetComponent<OVRManager>().enabled = true;
			Camera.main.gameObject.SetActive(false);
		} else {
			GameObject.Find("LeftEyeAnchor").SetActive(false);
			GameObject.Find("RightEyeAnchor").SetActive(false);
			
		}

		if (leapOnOculus == false) {
			GameObject.Find ("HandController").transform.localPosition = new Vector3(0.0f, -0.3f, 0.3f);
			GameObject.Find ("HandController").transform.localRotation = Quaternion.identity;
		}
	}


	public void restartLevel() {
		Application.LoadLevel (Application.loadedLevel);
	}

}

