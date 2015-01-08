using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;

public class HardwareCheck : MonoBehaviour {

	public bool oculusConnected = false;
	public bool leapConnected = false;
	
	private Text statusOVR;
	private Text statusLM;
	
	private Controller LeapController;

	void Awake()
	{
		statusOVR = GameObject.Find ("OculusRiftStatus").GetComponent<Text> ();
		statusLM = GameObject.Find ("LeapMotionStatus").GetComponent<Text> ();
	}

	void Start () {
		LeapController = new Controller();
		
		checkOculusPresence();
		checkLeapPresence();
		
		//LeapEventListener listener = new LeapEventListener(this);
		//LeapController.AddListener(listener); @TODO Remove comments
		
		OVRManager.HMDAcquired += checkOculusPresence;
		OVRManager.HMDLost += checkOculusPresence;
	}

	public void checkLeapPresence() {
		leapConnected = LeapController.IsConnected;
		
		if (leapConnected == true) {
			statusLM.text = "Connected";
			statusLM.color = Color.green;
		} else {
			statusLM.text = "Disconnected";
			statusLM.color = Color.red;
		}
	}
	
	public void checkOculusPresence () {
		oculusConnected = Ovr.Hmd.Detect() > 0;
		
		if (oculusConnected == true) {
			statusOVR.text = "Connected";
			statusOVR.color = Color.green;
		} else {
			statusOVR.text = "Disconnected";
			statusOVR.color = Color.red;
		}
	}


}

public class LeapEventListener : Listener
{
	private HardwareCheck HardwareCheck;
	
	public LeapEventListener(HardwareCheck hardwareCheck) {
		HardwareCheck = hardwareCheck;
	}
	
	public override void OnConnect (Controller controller){
		HardwareCheck.checkLeapPresence ();
	}
	public override void OnDisconnect (Controller controller){
		HardwareCheck.checkLeapPresence ();
	}
}

