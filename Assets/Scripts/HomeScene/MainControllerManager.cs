using UnityEngine;
using System.Collections;
using IRY; 
public class MainControllerManager : MonoBehaviour {

	void Awake () {
		if (GameObject.Find("MAINCONTROLLER") == null) {
			GameObject go = new GameObject();
			go.name = "MAINCONTROLLER";
			go.AddComponent<IRYConfiguration>();
			go.AddComponent<IRYController>();
			go.AddComponent<DontDestroyOnLoad>();
		}
	}

}
