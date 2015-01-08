using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkHandler : MonoBehaviour {

	public Material activeShader;
	public bool autoAjustCamera = true;
	public GameObject highlight;

	private GameObject lastGO = null;
	private Material lastMat = null;

	private GameObject currentGo = null;


	public void SetActive(string objectName) {
		currentGo = GameObject.Find(objectName);
		if (currentGo != null) {
			if (lastGO != null) {
				lastGO.renderer.material = lastMat;
			}

			lastGO = currentGo;
			lastMat = currentGo.renderer.material;

			currentGo.renderer.material = activeShader;
			currentGo.renderer.material.SetColor("_OutlineColor", Color.red);
			if (highlight != null) {
				highlight.transform.Find ("Image/Text").GetComponent<Text>().text = objectName;
			}
		}

	}

	void FixedUpdate() {
		if (autoAjustCamera == true && currentGo != null) {
			Camera.main.gameObject.GetComponent<SmoothLookAt>().target = currentGo.transform;

			if (highlight != null) {
				Vector3 screenPoint = Camera.main.WorldToScreenPoint(currentGo.transform.position);
				highlight.active = true;

				highlight.transform.position = screenPoint;
			}

		}
	}

	public void SetAsInstructor() {
		GameObject.Find ("CONTROLLER").GetComponent<MainController>().doProcedures = false;
	
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("PushButtons")) {
			SimpleInteraction si = go.GetComponent<SimpleInteraction>();
			if (si != null) {
				si.canInteract = false;
			}
		}
		
	}

}
