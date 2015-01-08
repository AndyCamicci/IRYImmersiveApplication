using UnityEngine;
using System.Collections;

public class SimpleInteraction : MonoBehaviour {

	public bool canInteract = true;

	private Vector3 idleStatePosition;
	private Vector3 idleStateRotation;

	public Vector3 activeStatePosition = new Vector3(0.035f, 0.0f ,0.0f);
	public Vector3 activeStateRotation;

	public GameObject voyant;
	public AudioClip activeSound;
	public AudioClip clickSound;

	public float transitionDuration = 0.5f; 
	public bool restrictIndex = false;

	private bool active = false;
	private bool readyForAction = true;
	private MainController Controller;

	void Awake()
	{
		gameObject.AddComponent<MeshCollider> ();
		gameObject.AddComponent<AudioSource> ();
		Controller = GameObject.Find("CONTROLLER").GetComponent<MainController>();
		gameObject.tag = "PushButtons";
	}

	void Start() {
		idleStatePosition = transform.localPosition;
		idleStateRotation = transform.localRotation.eulerAngles;
		activeStatePosition = new Vector3 (idleStatePosition.x + activeStatePosition.x, idleStatePosition.y, idleStatePosition.z);

		if (activeStateRotation == Vector3.zero) {
			activeStateRotation = idleStateRotation;
		}
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.A)) {
			makeTransition();
		}
	}

	void makeTransition() {
		if (canInteract == false || readyForAction == false) {
			return;
		}

		if (clickSound != null) {
			makeClickSound ();
		}

		GameObject.Find ("NETWORK").GetComponent<NetworkHandler> ().SetActive (name);

		if (!active) {
			jump(idleStatePosition, activeStatePosition, idleStateRotation, activeStateRotation);
			active = true;
			readyForAction = false;
		} else {
			jump(activeStatePosition, idleStatePosition, activeStateRotation, idleStateRotation);
			active = false;
			readyForAction = false;
		}
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

	void jump(Vector3 from, Vector3 to, Vector3 fromRot, Vector3 toRot)
	{
		StartCoroutine (Transition (from, to, fromRot, toRot));
	}

	IEnumerator Transition(Vector3 from, Vector3 to, Vector3 fromRot, Vector3 toRot) { 
		float t = 0.0f; 
		while (t < 1.0f) { 
			t += Time.deltaTime * (Time.timeScale/transitionDuration);
			
			transform.localPosition = Vector3.Lerp(from, to, t);

			Quaternion rot = transform.localRotation;
			rot.eulerAngles = Vector3.Lerp(fromRot, toRot, t);
			transform.localRotation = rot;

			yield return 0;
		}
		if (voyant != null) {
			voyant.GetComponent<SimpleVoyant>().changeState(active);
		}
		
		if (activeSound != null) {
			if (active == true) {
				audio.clip = activeSound;
				audio.loop = true;
				audio.Play();
			} else {
				audio.Stop();
			}
		}
		
		Controller.GetComponent<ProcedureScript>().buttonChanged(gameObject, active);
		
		readyForAction = true;
	}

	IEnumerator Transition(Quaternion from, Quaternion to) { 
		float t = 0.0f; 
		while (t < 1.0f) { 
			t += Time.deltaTime * (Time.timeScale/transitionDuration);
			
			transform.localRotation = Quaternion.Lerp(from, to, t);
			yield return 0;
		}
		if (voyant != null) {
			voyant.GetComponent<SimpleVoyant>().changeState(active);
		}
		
		if (activeSound != null) {
			if (active == true) {
				audio.clip = activeSound;
				audio.loop = true;
				audio.Play();
			} else {
				audio.Stop();
			}
		}
		
		Controller.GetComponent<ProcedureScript>().buttonChanged(gameObject, active);
		
		readyForAction = true;
	}

	void makeClickSound() {
		audio.clip = clickSound;
		audio.loop = false;
		audio.Play();
	}


}
