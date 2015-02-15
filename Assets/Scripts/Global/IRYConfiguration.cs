using UnityEngine;
using System.Collections;
using System;

namespace IRY {
	public class IRYConfiguration : MonoBehaviour {


		public static string baseUrl = "http://iry.reaco.fr";

		//public static string baseUrl = "http://localhost:8000";

		public static string STARTPRACTICALTRAINING = "STARTPRACTICALTRAINING";
		public static string STARTDEMONSTRATIVECOURSE = "STARTDEMONSTRATIVECOURSE";
		public static string STARTIMMERSIVEMOVIE = "STARTIMMERSIVEMOVIE";
		public static string COMMAND_SHOWBTN = "COMMAND_SHOWBTN";
		public static string COMMAND_GOTO_WAITING = "COMMAND_GOTO_WAITING";
		public static string COMMAND_SHOWIMAGE = "COMMAND_SHOWIMAGE";
		public static string COMMAND_CHANGE_SPEED = "COMMAND_CHANGE_SPEED";


		public static string apiUrl() {
			return IRYConfiguration.baseUrl + "/api";
		}
		public static string callInstructorURL() {
			return IRYConfiguration.baseUrl + "/instructeur/rest/call_instructor/{id}";
		}
		public static string getCourseInstructionsURL() {
			return IRYConfiguration.baseUrl + "/instructeur/rest/course_instructions/{id}";
		}
		public static string getInstructionsURL() {
			return IRYConfiguration.baseUrl + "/instructeur/rest/last_instruction/{id}";
		}
		public static string getSeriesURL() {
			return IRYConfiguration.apiUrl() + "/series";
		}
		public static string addPilotURL() {
			return IRYConfiguration.apiUrl() + "/pilots";
		}
		public static string deletePilotURL() {
			return IRYConfiguration.apiUrl() + "/pilots/{id}";
		}
		public static string getHelicopterURL() {
			return IRYConfiguration.apiUrl() + "/immersiveapplications/{id}/data";
		}
		public static string getProcedureURL() {
			return IRYConfiguration.apiUrl() + "/procedures/{courseId}";
		}

		public static string postStepProcedureURL() {
			return IRYConfiguration.baseUrl + "/instructeur/rest/poststep/{stepId}/{pilotId}/{success}";
		}

		void Awake() {
			if (String.IsNullOrEmpty( PlayerPrefs.GetString("baseUrl")) == false) {
				IRYConfiguration.baseUrl = PlayerPrefs.GetString("baseUrl");
			} else {
				PlayerPrefs.SetString("baseUrl", IRYConfiguration.baseUrl);
			}
		}

	}

}