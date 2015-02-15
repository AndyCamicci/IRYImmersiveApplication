using UnityEngine;
using System.Collections;

public class InstructeurSide : MonoBehaviour {

	void Start() {
		GetComponent<NetworkScript> ().GetServerList ();
	}

	public HostData[] hostList;
	
	void OnMasterServerEvent(MasterServerEvent mse)
	{
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("Registered Server");
		} else if (mse == MasterServerEvent.HostListReceived) {
			hostList = MasterServer.PollHostList();
			if (hostList.Length > 0) {
				GetComponent<NetworkScript> ().JoinServer (hostList[0]);
			}
		}
	}
	
}
