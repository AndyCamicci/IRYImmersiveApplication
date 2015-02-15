using UnityEngine;
using System.Collections;
using IRY;

public class WebPlayerMethods : MonoBehaviour {

	private LookAtMouse look;

	private HighlightItem highlight;

	public void Awake() {
		SetAsInstructor ("1");
		look = Camera.main.gameObject.GetComponent<LookAtMouse> ();
		highlight = GameObject.Find ("CONTROLLER").GetComponent<HighlightItem> ();
	}

	public void SetAsInstructor(string serie) {
		IRYController.IRYid = 0;
		IRYController.IRYserie = int.Parse (serie);
		IRYController.typeCourse = TypeCourse.DemonstrativeCourse;
		IRYController.isInstructor = true;
	}

	public void SetActive(string btn) {
		GameObject go = GameObject.Find (btn).gameObject;
		if (go != null) {
			look.target = go.transform;
			highlight.SetActive(btn);
		}

	}

	public void SetTimeScale(string ts) {
		Time.timeScale = float.Parse (ts);
	}

	/*public float ts = 1.0f;
	void  Update() {
		Time.timeScale = ts;
	}*/

}
