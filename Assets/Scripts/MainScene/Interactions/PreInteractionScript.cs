using UnityEngine;
using System.Collections;

public class PreInteractionScript : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{ 
		if (col.transform.name == "bone3") {
				makeTransition();
				StartCoroutine(fadeOut());
		}
	}

	IEnumerator fadeOut() {
		yield return new WaitForSeconds (2);
		gameObject.transform.parent.gameObject.renderer.material = lastMat;
		
	}


	private Material lastMat;
	private Material activeShader;

	void Awake() {
		activeShader = GameObject.Find ("CONTROLLER").GetComponent<HighlightItem> ().activeShader;
		lastMat = gameObject.transform.parent.gameObject.renderer.material;
	}

	void makeTransition() {
		gameObject.transform.parent.gameObject.renderer.material = activeShader;
		gameObject.transform.parent.gameObject.renderer.material.SetColor("_Color", new Color32(15,167,183,1));
		gameObject.transform.parent.gameObject.renderer.material.SetColor("_OutlineColor", new Color32(15,167,183,255));
	}


}
