using UnityEngine;
using System.Collections;

public class VoyantsController : MonoBehaviour {

	public ButtonToVoyant[] associations;

	void Start()
	{
		foreach (ButtonToVoyant assoc in associations) {

		}
	}

}

[System.Serializable]
public class ButtonToVoyant
{
	public GameObject button;
	public GameObject voyant;
};
