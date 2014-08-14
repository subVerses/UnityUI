using UnityEngine;
using System.Collections;

public class SneakBar : MonoBehaviour {

	//public SneakBarController SneakBar;
	public bool isDetected; //during detection, eye widens
	public bool isSeen; //when detection is maxed, player is seen
	public bool npcIsHostile;

	int detectionTicks;
	public Texture visibilityStage0;
	public Texture visibilityStage1;
	public Texture visibilityStage2;
	public Texture visibilityStage3;
	public Texture visibilityStage4A;

	private static int MAX_DETECTED = 4; //maximum value of detectionticks needed for isSeen

	// Use this for initialization
	void Start () {
		isDetected = false;
		detectionTicks = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsDetected)
		{
			detectionTicks++;
		}
		else
		{
			detectionTicks--;
		}
	}

	void OnGUI() {
		bool enabledBackup = GUI.enabled;
		if(detectionTicks == 0){
			GUI.Box(new Rect(Screen.width, Screen.height, Screen.width/5f, Screen.height/5f), visibilityStage0);}
		else if (detectionTicks == 1){
			GUI.Box(new Rect(Screen.width, Screen.height, Screen.width/5f, Screen.height/5f), visibilityStage1);}
		else if (detectionTicks == 2){
			GUI.Box(new Rect(Screen.width, Screen.height, Screen.width/5f, Screen.height/5f), visibilityStage2);}
		else if (detectionTicks == 3){
			isSeen = true;
			GUI.Box(new Rect(Screen.width, Screen.height, Screen.width/5f, Screen.height/5f), visibilityStage3);}
		else if (detectionTicks == 4){
			isSeen = true;
			GUI.Box(new Rect(Screen.width, Screen.height, Screen.width/5f, Screen.height/5f), visibilityStage4A);}
		GUI.enabled = enabledBackup;
	}
	
	public void setDetected (bool val){
		isDetected = val;
	}

	public bool NpcIsHostile {
		get {
			return npcIsHostile;
		}
	}

	public bool IsDetected {
		get {
			return isDetected;
		}
	}
}
