using UnityEngine;
using System.Collections;

public class ServerManager : MonoBehaviour {

	public string ip = "127.0.0.1";
	public int port = 25001;

	string gameName = "myNetworkTestName";

	HostData[] hostsData;

	void Start () {
		Application.runInBackground = true;
	}

	public void InitClient()
	{
		Network.Connect(ip, port);
	}

	public void InitServer()
	{
		Network.InitializeServer(10, port, false);
		MasterServer.RegisterHost(gameName, "Game Name => this is a test", "Master Server Sample in Unity ");
	}

	void OnPlayerConnected(NetworkPlayer player)
	{
		Debug.Log ("Player Connected");
	}
	
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}


	void OnServerInitialized()
	{
		Debug.Log("Server initialized and ready");
	}

	void OnMasterServerEvent(MasterServerEvent iEvent)
	{
		if (iEvent == MasterServerEvent.RegistrationSucceeded)
		{
			Debug.Log("Registration is OK");
		}
		if (iEvent == MasterServerEvent.HostListReceived)
		{
			hostsData = MasterServer.PollHostList();
		}
	}



}
