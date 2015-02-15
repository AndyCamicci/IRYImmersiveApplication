using UnityEngine;
using System.Collections;

public class PushButton : MonoBehaviour {

	public IEnumerator Transition() { 
		InteractableObject iO = GetComponent<InteractableObject> ();
		if (iO.active == true) {
			transform.Translate(0, 0, 0.002f);
		} else {
			transform.Translate(0, 0, -0.002f);
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
		Debug.Log ("1");
		Debug.Log (gameObject);
		iO.procedureScript.buttonChanged(gameObject, active);
		iO.procedureScript.readyForAction = true;
	}




}
