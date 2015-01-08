using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Ionic.Zip;

public class RegisterVideoScript : MonoBehaviour {

	public GameObject[] registerObjects;

	public bool triggerStartRegister = false;
	public bool triggerStopRegister = false;
	public bool triggerStartPlaying = false;

	private List<RegisterData> registerData = new List<RegisterData> ();

	private bool register;
	private bool play;

	private float deltaTime = 0.0f;

	private int currentImagePlaying = 0;

	void Start () {
		register = false;
		play = false;
	}

	void Update() {
		if (triggerStartRegister == true) {
			StartRegister();
			triggerStartRegister = false;
		}

		if (triggerStopRegister == true) {
			StopRegister();
			triggerStopRegister = false;
		}

		if (triggerStartPlaying == true) {
			StartPlay();
			triggerStartPlaying = false;
		}
	}

	void FixedUpdate () {
		if (register == true) {
			RegisterData reg = new RegisterData();
			reg.time = Time.time - deltaTime;
			for (int i = 0; i < registerObjects.Length; i++) {
				reg.addObject(registerObjects[i]);
			}
			//if (registerData.Count > 0) {
			//	RegisterData lastReg = registerData.Last() as RegisterData;
			//	if (lastReg.toString() == reg.toString()) {
			//
			//	}
			//} else {
				registerData.Add(reg);
			//}
		}

		if (play == true) {
			if (currentImagePlaying < registerData.Count) {
				if (registerData[currentImagePlaying].time >= Time.time - deltaTime) {
					registerData[currentImagePlaying].Play ();
					currentImagePlaying++;
					//registerData.RemoveAt(0);
				}
			} else {
				play = false;
			}
		}
	}

	public void StartRegister() {
		register = true;
		deltaTime = Time.time;
	}

	public void StopRegister() {
		register = false;
		StreamWriter writer = new StreamWriter (Application.dataPath + "/GameData/video.txt");

		for (int i = 0; i < registerData.Count; i++) {
			RegisterData reg = registerData[i];
			writer.Write(reg.time);
			writer.WriteLine();
			for (int j = 0; j < reg.objectsData.Count; j++) {
				ObjectData od = reg.objectsData[j];
				if (j > 0) {
					writer.WriteLine ();
				}
				writer.Write (od.go.name);
				writer.Write ("|" + od.position.ToString());
				writer.Write ("|" + od.rotation.ToString());
				
			}
			writer.WriteLine();
		}
		
		writer.Close ();

		ZipFile zip = new ZipFile ();
		zip.AddFile(Application.dataPath + "/GameData/video.txt", ""); 
		zip.Save(Application.dataPath + "/GameData/video.zip");
	}


	public void StartPlay() {

		//ZipFile zip = new ZipFile (Application.dataPath + "/GameData/video.zip");
		//zip.ExtractAll(Application.dataPath + "/GameData/", ExtractExistingFileAction.OverwriteSilently);

		string line;

		StreamReader reader = new StreamReader (Application.dataPath + "/GameData/video.txt");

		RegisterData reg = null;

		while((line = reader.ReadLine()) != null) {
			//Debug.Log(line);
			string[] data = line.Split('|');


			switch (data.Length) {
			case 1:
				//0 = time
				if (reg != null) {
					registerData.Add(reg);
				}
				reg = new RegisterData();
				reg.time = float.Parse(data[0]);
				break;
			case 3:
				// 0 = name
				// 1 = pos
				// 2 = rot
				GameObject go = GameObject.Find (data[0]);
				if (go) {
					reg.addObject(go, getVector3(data[1]), getQuaternion(data[2]));
				}
				break;

			}

			
		}

		Debug.Log (registerData);
		currentImagePlaying = 0;
		play = true;
		deltaTime = Time.time;
	}

	public Vector3 getVector3(string rString){
		string[] temp = rString.Substring(1,rString.Length-2).Split(',');
		float x = float.Parse(temp[0]);
		float y = float.Parse(temp[1]);
		float z = float.Parse(temp[2]);
		Vector3 rValue = new Vector3(x,y,z);
		return rValue;
	}

	public Quaternion getQuaternion(string rString){
		string[] temp = rString.Substring(1,rString.Length-2).Split(',');
		float x = float.Parse(temp[0]);
		float y = float.Parse(temp[1]);
		float z = float.Parse(temp[2]);
		float w = float.Parse(temp[3]);
		Quaternion rValue = new Quaternion(x,y,z,w);
		return rValue;
	}
}

public class RegisterData {

	public List<ObjectData> objectsData = new List<ObjectData> ();
	public float time;
	public bool objectsHaveMoved = false;
	
	public void addObject(GameObject go) {
		ObjectData od = new ObjectData ();
		od.go = go;
		od.position = go.transform.position;
		od.rotation = go.transform.rotation;

		this.objectsData.Add (od);
	}

	public void addObject(GameObject go, Vector3 position, Quaternion rotation) {
		ObjectData od = new ObjectData ();
		od.go = go;
		od.position = position;
		od.rotation = rotation;
	
		this.objectsData.Add (od);
	}

	public void Play() {
		for (int i = 0; i < objectsData.Count; i++) {
			ObjectData od = this.objectsData[i];
			GameObject go = od.go;
			go.transform.position = od.position;
			go.transform.rotation = od.rotation;

		}
	}

	//public string toString() {
	//
	//}
}

public class ObjectData {
	public Vector3 position;
	public Quaternion rotation;
	public GameObject go;
}