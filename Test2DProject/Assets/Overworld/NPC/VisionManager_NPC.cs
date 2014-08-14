using UnityEngine;
using System.Collections;

public class VisionManager_NPC : MonoBehaviour {
	
	BoxCollider2D npcCollider; //testing purposes
	NPC_Interactable npcDir;
	GameObject charController;
	SpriteMover charDir;
	SneakBar sneakBar;
	bool isSeen;

	public Ray visionRay;
	public int currentDirection;
	public Vector3 dirVector;
	
	void Start () {
		npcCollider = transform.GetComponentInChildren<BoxCollider2D>();
		npcDir = npcCollider.GetComponent<NPC_Interactable>();
		charController = GameObject.Find ("SpriteController");
		charDir = charController.GetComponent<SpriteMover>();
		sneakBar = new SneakBar();
		isSeen = false;

		currentDirection = 0;
		visionRay = new Ray();
		dirVector = new Vector3();
	}

	//gets the direction the npc is facing, then checks the transform matrix of that npc, then fires a ray in the direction the npc is facing
	void Update () {
		npcDir.setCurrentDirection (3);
		currentDirection = 3;

		switch(currentDirection){
			case -1:
				break;
			case 0: //currentDirection = 0//i have a feeling this isn't set up for vector math...
				dirVector = new Vector3(-1, 0, 0);
				break;
			case 1:
				dirVector = new Vector3(0, 1, 0);
				break;
			case 2:
				dirVector = new Vector3(1, 0, 0);
				break;
			case 3:
				dirVector = new Vector3(0, -1, 0);
				break;
		}
		
		visionRay = new Ray (new Vector3(npcCollider.bounds.center.x, npcCollider.bounds.center.y), dirVector);
		Debug.Log ("THE RAY HAS BEEN DRAWN");

	}
	
	void OnGUI() {
		if(isPlayerSeen())
		{
			sneakBar.incrementDetection();
		}
		else
		{
			sneakBar.decrementDetection();
		}
		bool enabledBackup = GUI.enabled;
		int detection = sneakBar.getDetection;
		if(detection == 0){
			GUI.Box(new Rect(0, 0, Screen.width/5f, Screen.height/5f), sneakBar.visibilityStage0);}
		else if (detection == 1){
			GUI.Box(new Rect(0, 0, Screen.width/5f, Screen.height/5f), sneakBar.visibilityStage1);}
		else if (detection == 2){
			GUI.Box(new Rect(0, 0, Screen.width/5f, Screen.height/5f), sneakBar.visibilityStage2);}
		else if (detection == 3){
			isSeen = true;
			GUI.Box(new Rect(0, 0, Screen.width/5f, Screen.height/5f), sneakBar.visibilityStage3);}
		else if (detection == 4){
			isSeen = true;
			GUI.Box(new Rect(0, 0, Screen.width/5f, Screen.height/5f), sneakBar.visibilityStage4A);}
		GUI.enabled = enabledBackup;
	}

	public bool isPlayerSeen() //checks the bounds of the character compared to the ray of the vision
	{
		Bounds b = charDir.getBounds ();
		Debug.Log (b.IntersectRay (visionRay));
		return !b.IntersectRay(visionRay);
	}
}
