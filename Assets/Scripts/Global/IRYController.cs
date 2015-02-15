using UnityEngine;
using System.Collections;
using System.Net;
using IRY;
using SimpleJSON;

namespace IRY {

	public class IRYController : MonoBehaviour {

		public static int IRYserie = 1;
		public static int IRYid = 16;
		public static string IRYname = "Andy";
		public static int IRYcourse = 14;
		public static bool isInstructor = false;
		public static JSONNode helicopterProperties;
		public static TypeCourse typeCourse = TypeCourse.PracticalTraining;
		
		void OnApplicationQuit() {
			// REMOVE THE PLAYER FROM THE DATABASE
			if (IRYController.IRYid != 16) {
				string deleteUrl = IRYConfiguration.deletePilotURL().Replace ("{id}", IRYController.IRYid.ToString());
				Debug.Log (deleteUrl);
				WebRequest webRequest = WebRequest.Create(deleteUrl);
				webRequest.Method = "DELETE";
				WebResponse response = webRequest.GetResponse ();
				Debug.Log(response);
			}

		}

	}

	public enum TypeCourse {
		PracticalTraining, ImmersiveMovie, DemonstrativeCourse
	}

}
