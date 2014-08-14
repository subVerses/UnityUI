using UnityEngine;
using System.Collections;

public class VisionManager_NPC : MonoBehaviour {
	
	GameObject npcSprite; //testing purposes
	NPC_Interactable npcDir;
	GameObject charController;
	SpriteMover charDir;
	GameObject SneakIcon;
	SneakBar sneakBar;

	public Ray visionRay;
	public int currentDirection;
	public Vector3 dirVector;
	
	void Start () {
		npcSprite = GameObject.Find ("TestNPCSprite");
		npcDir = npcSprite.GetComponent<NPC_Interactable>();
		charController = GameObject.Find ("CharacterController");
		charDir = charController.GetComponent<SpriteMover>();
		SneakIcon = GameObject.Find ("SneakIcon");
		sneakBar = SneakIcon.GetComponent<SneakBar>();

		currentDirection = 0;
		visionRay = new Ray();
		dirVector = new Vector3();
	}

	//gets the direction the npc is facing, then checks the transform matrix of that npc, then fires a ray in the direction the npc is facing
	void Update () {
		npcDir.setCurrentDirection (3);
		currentDirection = npcDir.getCurrentDirection();

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

		visionRay = new Ray (npcSprite.transform.localPosition, dirVector);
		Debug.Log ("THE RAY HAS BEEN DRAWN");

		if(isPlayerSeen())
		{
			sneakBar.setDetected(true);
		}
		else
		{
			sneakBar.setDetected(false);
		}
	}

	public bool isPlayerSeen() //checks the bounds of the character compared to the ray of the vision
	{
		Bounds b = charDir.getBounds ();
		return b.IntersectRay(visionRay);
	}

}
