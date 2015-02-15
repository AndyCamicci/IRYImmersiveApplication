using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InteractableLookingObject : MonoBehaviour {

	public ProcedureScript procedureScript;
	public GameObject slider;
	private Slider sliderCp;

	public bool canInteract = true;

	void Awake()
	{
		procedureScript = GameObject.Find("CONTROLLER").GetComponent<ProcedureScript>();
		gameObject.AddComponent<MeshRenderer> ();
		slider = GameObject.Find ("CONTROLLER").GetComponent<InteractionsScript> ().sliderWatch;
		//slider.GetComponent<CanvasScaler> ().scaleFactor = 0.4f;
		sliderCp = slider.transform.Find("Slider").GetComponent<Slider> ();
	}

	public float percent = 0.0f;
	void Update()
	{
		if (canInteract == true) {
			slider.SetActive (true);
		} else {
			slider.SetActive(false);
			return;
		}

		if (gameObject.renderer.isVisible == true) {
			percent += 3f;
		} else {
			percent -= 1f;
		}

		sliderCp.value = percent;

		//Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		//slider.transform.position = screenPoint;
		Vector3 modified = transform.position;
		modified.x += 0.01f;
		slider.transform.position = modified;


		if (percent >= 100) {
			procedureScript.buttonChanged(gameObject, true);
			canInteract = false;
			percent = 0.0f;
		}
	}
}
