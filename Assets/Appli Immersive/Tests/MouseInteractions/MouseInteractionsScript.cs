using UnityEngine;
using System.Collections;

public class MouseInteractionsScript : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {

		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit)){
			Animator animator = hit.transform.root.GetComponent<Animator>();
			
			if (Input.GetMouseButtonDown(0)){
				//hit.transform.renderer.material.color = Color.red;
				animator.SetTrigger("Activate");
			} else {
				animator.SetTrigger("Hover");
			}
		}
	}
}
