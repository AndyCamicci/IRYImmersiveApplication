using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	AudioSource motor;
	AudioSource alarm;

	private AudioSource audio;
	public AudioClip clickSound;

	void Awake() {
		audio = gameObject.AddComponent<AudioSource> ();
	}

	void Start () {
		GameObject.Find ("Crashed").SetActive (false);
		motor = GameObject.Find ("HelicopterSound").GetComponent<AudioSource> ();
		alarm = GameObject.Find ("HelicopterAlarmSound").GetComponent<AudioSource> ();
	}
	
	public void setAlarm() {
		alarm.Play ();
	}

	public void crashed() {
		motor.Stop ();
		alarm.Stop ();
	}

	public void incrementPitch() {
		motor.pitch += 0.01f;
	}


}
