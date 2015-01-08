using UnityEngine;
using System.Collections;

public class IRYMainConfigurationScript : MonoBehaviour {

	public string addPilotURL = "http://localhost:8000/api/pilots";
	public string deletePilotURL = "http://localhost:8000/api/pilots/{id}";

}

[System.Serializable]
public class ConfigurationValue
{
	public string key;
	public string value;	
};