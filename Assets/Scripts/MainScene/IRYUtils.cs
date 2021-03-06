﻿using UnityEngine;
using System.Collections;

namespace IRYUtils {

	public class Convert : MonoBehaviour {

		public static Vector3 getVector3(string rString){
			string[] temp = rString.Substring(1,rString.Length-2).Split(',');
			float x = float.Parse(temp[0]);
			float y = float.Parse(temp[1]);
			float z = float.Parse(temp[2]);
			Vector3 rValue = new Vector3(x,y,z);
			return rValue;
		}
		
		public static Quaternion getQuaternion(string rString){
			string[] temp = rString.Substring(1,rString.Length-2).Split(',');
			float x = float.Parse(temp[0]);
			float y = float.Parse(temp[1]);
			float z = float.Parse(temp[2]);
			float w = float.Parse(temp[3]);
			Quaternion rValue = new Quaternion(x,y,z,w);
			return rValue;
		}
	}

}
