using UnityEngine;
using System.Collections;

public class CubeInteractions : MonoBehaviour {
	Animator anim;
	
	void Awake() {
		anim = GetComponent<Animator>();
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.transform.name == "bone3" && col.transform.parent.name == "index") {
			if (!anim.GetBool("Active")) {
				anim.SetTrigger("ActivateTrigger");
				anim.SetBool("Active", true);
			} else {
				anim.SetTrigger("DesactivateTrigger");
				anim.SetBool("Active", false);
			}
			
		}
	}

	public void toggleActive() {

	}
}
