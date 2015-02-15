using UnityEngine;
using System.Collections;

public enum ButtonType {
	Push, Lever
}

public class InteractableObject : MonoBehaviour {

	public GameObject voyant;
	public AudioClip activeSound;
	public AudioClip clickSound;
	
	public bool restrictIndex = true;

	public ButtonType buttonType;
	
	public bool active = false;
	public ProcedureScript procedureScript;

	private GameObject preTouch;

	void Awake()
	{
		gameObject.AddComponent<MeshCollider> ();
		gameObject.AddComponent<AudioSource> ();
		procedureScript = GameObject.Find("CONTROLLER").GetComponent<ProcedureScript>();

		return;
		preTouch = new GameObject ();
		preTouch.transform.SetParent (transform);
		preTouch.AddComponent<MeshCollider> ();
		preTouch.GetComponent<MeshCollider> ().sharedMesh = GetComponent<MeshCollider> ().sharedMesh;
		
		preTouch.transform.localPosition = Vector3.zero;
		preTouch.transform.localRotation = Quaternion.identity;
		preTouch.transform.localScale = new Vector3 (2, 2, 10);

		preTouch.AddComponent<PreInteractionScript> ();
	}

	void makeTransition() {
		if (procedureScript.readyForAction == false) {
			return;
		}
		
		procedureScript.readyForAction = false;
		
		if (clickSound != null) {
			makeClickSound ();
		}
		
		//GameObject.Find ("NETWORK").GetComponent<NetworkHandler> ().SetActive (name);
		switch(buttonType) {
			case ButtonType.Lever:
				StartCoroutine(GetComponent<LeverButton>().Transition());
				break;
			case ButtonType.Push:
				StartCoroutine(GetComponent<PushButton>().Transition());
				break;
			default:
				Debug.LogError(buttonType + " not supported");
				break;
		}
		active = !active;
	}
	
	void OnCollisionEnter (Collision col)
	{ 
		if (col.transform.name == "bone3") {
			if (restrictIndex == false || (restrictIndex == true &&col.transform.parent.name == "index")) {
				makeTransition();
			}
		}
	}
	
	void OnMouseDown() {
		makeTransition ();
	}

	void makeClickSound() {
		audio.clip = clickSound;
		audio.loop = false;
		audio.Play();
	}
}
