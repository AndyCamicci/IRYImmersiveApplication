using UnityEngine;
using System.Collections;
using IRY;

public class PilotSide : MonoBehaviour {

	void Start () {
		GetComponent<NetworkScript> ().StartServer ();
	}

}
