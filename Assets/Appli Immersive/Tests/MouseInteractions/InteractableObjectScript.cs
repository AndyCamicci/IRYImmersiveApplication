using UnityEngine;
using System.Collections;

public class InteractableObjectScript : MonoBehaviour {

	Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
	}

	void OnMouseOver() {
		Debug.Log("Mouse enter");
		anim.SetBool("Hover", true);
	}

	void OnMouseExit() {
		Debug.Log("Mouse exit");
		anim.SetBool("Hover", false);
	}

	void OnMouseDown() {
		Debug.Log ("Mouse down");
		anim.SetTrigger("Activate");
	}
}
