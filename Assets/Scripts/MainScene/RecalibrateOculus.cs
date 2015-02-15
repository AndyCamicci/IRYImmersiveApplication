using UnityEngine;
using System.Collections;

public class RecalibrateOculus : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel("MainScene");
		}
	}
}
