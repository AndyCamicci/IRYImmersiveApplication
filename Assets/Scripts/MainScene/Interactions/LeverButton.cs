using UnityEngine;
using System.Collections;

public class LeverButton : MonoBehaviour {

	public IEnumerator Transition() {
		InteractableObject iO = GetComponent<InteractableObject> ();
		if (iO.active == true) {
			transform.Rotate(0, -20.0f, 0);
		} else {
			transform.Rotate(0, 20.0f, 0);
		}
		
		
		if (iO.voyant != null) {
			//iO.voyant.GetComponent<SimpleVoyant>().changeState(active);
		}
		
		if (iO.activeSound != null) {
			if (active == true) {
				audio.clip = iO.activeSound;
				audio.loop = true;
				audio.Play();
			} else {
				audio.Stop();
			}
		}

		yield return new WaitForSeconds(0.5f);
		
		iO.procedureScript.buttonChanged(gameObject, active);
		iO.procedureScript.readyForAction = true;
	}
}
