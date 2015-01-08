using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ButtonScript : MonoBehaviour {

	public ButtonType buttonType;

	public Vector3 pushedPosition;
}

public enum ButtonType {
	Poussoir,
	Levier,
	Manche
};

/*
[CustomEditor(typeof(ButtonScript))]
public class ButtonScriptEditor : Editor
{
	void OnInspectorGUI()
	{
		ButtonScript myScript = target as ButtonScript;
		
		if (myScript.buttonType == ButtonType.Poussoir) {

		}
	}
}
 */