using UnityEngine;
using System.Collections;

public class AnimationsManager : MonoBehaviour {

	private AnimationController animController;
	private GameObject helicopterModel;
	//private GameObject voyantsModel;
	private GameObject pilotModel;
	private GameObject rotorModel;
	private GameObject instructorModel;
	private GameObject crashed;

	void Awake() {
		helicopterModel = GameObject.Find ("HELICOPTER");
		pilotModel = GameObject.Find ("vincent");
		instructorModel = GameObject.Find ("justin");
		rotorModel = GameObject.Find ("ROTOR");
		crashed = GameObject.Find ("Crashed");
		//voyantsModel = GameObject.Find ("justin");
		animController = GameObject.Find ("CONTROLLER").GetComponent<AnimationController> ();
	}

	public void InstructeurButtonPressed() {
		Debug.Log ("Panne");
		pilotModel.GetComponent<Animator> ().SetTrigger ("Panne");
		animController.setAlarm();
		click ();
	}

	public void PilotButtonPressed() {
		Debug.Log ("CRASH");
		is_crashing = true;
		helicopterModel.GetComponent<Animator> ().SetTrigger ("Crash");
		click ();
	}

	void click() {
		animController.audio.clip = animController.clickSound;
		animController.audio.loop = false;
		animController.audio.Play();
	}

	public void Crashed() {
		animController.crashed ();
		rotorModel.GetComponent<Animator> ().SetTrigger ("Stop");
		crashed.SetActive (true);
	}

	bool is_crashing = false;
	void FixedUpdate() {
		if (is_crashing) {
			animController.incrementPitch();
		}
	}
}
