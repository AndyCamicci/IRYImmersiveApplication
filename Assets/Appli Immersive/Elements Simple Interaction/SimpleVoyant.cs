using UnityEngine;
using System.Collections;

public class SimpleVoyant : MonoBehaviour {

	public Material offMaterial;
	public Material onMaterial;

	public void changeState(bool state)
	{
		Debug.Log("State changed to " + state);
		if (offMaterial != null && onMaterial != null) {
			gameObject.renderer.material = (state == true) ? onMaterial : offMaterial;
		} else {
			gameObject.renderer.material.color = (state == true) ? Color.green : Color.red;
		}
	}
}
