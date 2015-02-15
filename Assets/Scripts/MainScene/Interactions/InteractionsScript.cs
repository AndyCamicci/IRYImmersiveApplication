using UnityEngine;
using System.Collections;
using IRY;
using SimpleJSON;

public class InteractionsScript : MonoBehaviour {

	public GameObject sliderWatch;
	public AudioClip clickSound;

	public void addScripts() {
		JSONArray pushButtons = IRYController.helicopterProperties ["data"] ["buttons"] ["pushButtons"].AsArray;
		JSONArray leverButtons = IRYController.helicopterProperties ["data"] ["buttons"] ["leverButtons"].AsArray;

		for (int i = pushButtons.Count - 1; i >= 0; i--) {
			GameObject obj = GameObject.Find(pushButtons[i]);
			if (obj == null) { Debug.Log(pushButtons[i] + " object not found"); continue; }
			obj.AddComponent<InteractableObject>();
			obj.AddComponent<PushButton>();
			
			obj.GetComponent<InteractableObject>().buttonType = ButtonType.Push;
			obj.GetComponent<InteractableObject>().clickSound = clickSound;
		}

		for (int i = leverButtons.Count - 1; i >= 0; i--) {
			GameObject obj = GameObject.Find(leverButtons[i]);
			if (obj == null) { Debug.Log(leverButtons[i] + " object not found"); continue; }
			obj.AddComponent<InteractableObject>();
			obj.AddComponent<LeverButton>();
			
			obj.GetComponent<InteractableObject>().clickSound = clickSound;
			obj.GetComponent<InteractableObject>().buttonType = ButtonType.Lever;
		}
	}


	public string pushButtonTag = "Push Buttons";
	public string leverButtonTag = "Lever Buttons";
	public void addScriptsUsingTags() {
		GameObject[] pushGos = GameObject.FindGameObjectsWithTag(pushButtonTag) as GameObject[];
		
		for (int i = pushGos.Length - 1; i >= 0; i--) {
			pushGos[i].AddComponent<InteractableObject>();
			pushGos[i].AddComponent<PushButton>();
			
			pushGos[i].GetComponent<InteractableObject>().buttonType = ButtonType.Push;
			pushGos[i].GetComponent<InteractableObject>().clickSound = clickSound;
		}
		
		GameObject[] leverGos = GameObject.FindGameObjectsWithTag(leverButtonTag) as GameObject[];
		for (int i = leverGos.Length - 1; i >= 0; i--) {
			leverGos[i].AddComponent<InteractableObject>();
			leverGos[i].AddComponent<LeverButton>();
			
			leverGos[i].GetComponent<InteractableObject>().buttonType = ButtonType.Lever;
			leverGos[i].GetComponent<InteractableObject>().clickSound = clickSound;
		}
	}

}
