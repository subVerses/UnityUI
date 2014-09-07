using UnityEngine;
using System.Collections;

public class VisionManager_NPC : MonoBehaviour {
	
	BoxCollider2D npcCollider; //testing purposes
	NPC_Interactable npcDir;
	GameObject charController;
	SpriteMover charDir;

	SneakBar sneakBar;
	bool isSeen;
	bool npcIsHostile;
	int detection;

	public Ray visionRay;
	public int currentDirection;
	public Vector3 dirVector;
	
	void Start () {
		npcCollider = transform.GetComponentInChildren<BoxCollider2D>();
		npcDir = npcCollider.GetComponent<NPC_Interactable>();
		charController = GameObject.Find ("CharSprite");
		charDir = charController.GetComponent<SpriteMover>();

		sneakBar = new SneakBar();
		isSeen = false;
		detection = sneakBar.getDetection;
		npcIsHostile = false;

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
		if(isPlayerSeen())
		{
			isSeen = true;
			sneakBar.incrementDetection();
			detection = sneakBar.getDetection;
			//Debug.Log ("Player Seen" + detection);
		}
		else
		{
			isSeen = false;	
			sneakBar.decrementDetection();
			detection = sneakBar.getDetection;
			//Debug.Log ("Player Not Seen" + detection);
		}
	}
//	
//	void OnGUI() {
//		bool enabledBackup = GUI.enabled;
//
//		if(detection == 0){
//			GUI.DrawTexture(new Rect(0, 0, Screen.width/8f, Screen.height/8f), sneakBar.VisibilityStage0);}
//		else if (detection == 1){
//			GUI.DrawTexture(new Rect(0, 0, Screen.width/8f, Screen.height/8f), sneakBar.VisibilityStage1);}
//		else if (detection == 2){
//			GUI.DrawTexture(new Rect(0, 0, Screen.width/8f, Screen.height/8f), sneakBar.VisibilityStage2);}
//		else if (detection >= 3){
//			GUI.DrawTexture(new Rect(0, 0, Screen.width/8f, Screen.height/8f), sneakBar.VisibilityStage3);}
//		else if (detection == 4 && npcIsHostile){
//			GUI.DrawTexture(new Rect(0, 0, Screen.width/8f, Screen.height/8f), sneakBar.VisibilityStage4A);}
//		else{
//			GUI.DrawTexture(new Rect(0, 0, Screen.width/8f, Screen.height/8f), sneakBar.VisibilityStage0);}
//
//		GUI.enabled = enabledBackup;
//	}

	public bool isPlayerSeen() //checks the bounds of the character compared to the ray of the vision
	{
		Bounds b = charDir.getBounds ();
		return b.IntersectRay(visionRay);
	}
}
