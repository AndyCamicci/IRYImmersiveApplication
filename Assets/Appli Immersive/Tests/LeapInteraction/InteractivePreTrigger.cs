using UnityEngine;
using System.Collections;

public class InteractivePreTrigger : MonoBehaviour {
	/*Animator anim;

	void Awake() {
		anim = transform.root.GetComponent<Animator>();
	}*/

	void OnCollisionEnter (Collision col)
	{
		if (col.transform.name == "bone3" && col.transform.parent.name == "index") {
			
			//transform.parent.FindChild("Object").renderer.material.color = Color.blue;
		}
	}
	void OnCollisionExit (Collision col) {
		if (col.transform.name == "bone3" && col.transform.parent.name == "index") {
			//transform.parent.FindChild("Object").renderer.material.color = Color.white;
		}
	}
}
