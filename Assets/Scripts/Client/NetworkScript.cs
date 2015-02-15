using UnityEngine;
using System.Collections;

public class NetworkScript : MonoBehaviour {

	private const string typeName = "IRYPracticalTraining";
	private const string gameName = "IdPilot";
	private int port = 25000;

	public HostData[] hostList;
	
	public void StartServer()
	{
		Debug.Log("Creating Server...");
		Network.InitializeServer(4, port, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	public void GetServerList()
	{	
		MasterServer.RequestHostList(typeName);
	}

	public void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
	}
	
	void OnMasterServerEvent(MasterServerEvent mse)
	{
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("Registered Server");
		} else if (mse == MasterServerEvent.HostListReceived) {
			hostList = MasterServer.PollHostList();
			Debug.Log(hostList.Length);
			
		}
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
	}

}
