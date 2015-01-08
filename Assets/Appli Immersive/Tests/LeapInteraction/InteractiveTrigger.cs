using UnityEngine;
using System.Collections;

public class InteractiveTrigger : MonoBehaviour {
	private Animator anim;
	
	void Awake() {
		anim = transform.root.GetComponent<Animator>();
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.transform.name == "bone3" && col.transform.parent.name == "index") {
			if (!anim.GetBool("Activated")) {
				anim.SetTrigger("Activate");
				anim.SetBool("Activated", !anim.GetBool("Activated"));

			}
			//transform.parent.FindChild("Object").renderer.material.color = Color.red;
			//Destroy(transform.parent.gameObject);
		}
	}

	public void endActivated()
	{

	}
}
