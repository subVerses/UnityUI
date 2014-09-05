using UnityEngine;
using System.Collections;

public class CharacterInteraction : MonoBehaviour {

	public CollidableObjects collidableObjects;
	public SpriteMover spriteMov;
	public GUISkin skin;
	public GameObject optionsPanel;
	Animator anim;

	public int lastDirection; //last direction chosen by player
	public int nextInput; //next input queued up after the current one
	public int frameDelay; //frames left until character moves in given direction
	bool npcChat; //should chat be activated?
	public bool isMoving; //is the character moving?
	bool optionsOn;
	bool[] validDirections; //which directions can the character currently move in?
	bool buttonPushed;
	int resetFrames;

	public const int FRAMESBEFOREMOVE = 8;

	void Awake () {
		DontDestroyOnLoad (this);
	}

	void Start () {
		lastDirection = 3;
		nextInput = -1;
		frameDelay = FRAMESBEFOREMOVE;
		resetFrames = 8;
		npcChat = false;
		isMoving = false;
		validDirections = new bool[4] { true,   //right
										true,   //up
										true,   //left
										true }; //down
		anim = GameObject.Find("CharSprite").GetComponent<Animator>();
		resetAnimation();
		Application.LoadLevelAdditive("Overworld_Reload");
	}
	
	// Update is called once per frame
	void Update () {
		if(npcChat) {
			npcChat = false;
			nextInput = -1;
			isMoving = false;
			gameObject.SetActive(false);
			Application.LoadLevel (2); //loading the NPC scene here, although not sure if this is the best way
		}
		if(!buttonPushed) {
			if(resetFrames == 0)
				frameDelay = FRAMESBEFOREMOVE;
			else
				resetFrames--;
		} else
			resetFrames = 8;
	}
	
	void OnGUI() {
		buttonPushed = false;
		if(optionsOn) {

		} else {
			if(isMoving && spriteMov.getCurrentDirection() == -1) { //if character is stopped and has just finished moving, update viable directions
				isMoving = false;
				anim.SetBool("isMoving", false);
				updateDirections (spriteMov.getNextBounds());
			}
			if(!isMoving && nextInput != -1 && spriteMov.getCurrentDirection() == -1) { //if a new input is queued up and the character is currently idle, make the character do the new input
				if(nextInput == 4) {
					aPushed(spriteMov.getBounds(), lastDirection);
					nextInput = -1;
				} else if(nextInput == 5) {
					bPushed ();
					nextInput = -1;
				} else if(testCommand(spriteMov.getNextBounds(), nextInput)) {
					if(frameDelay == 0) {
						spriteMov.setCurrentDirection(nextInput);
						isMoving = true;
						lastDirection = nextInput;
						anim.SetBool("isMoving", true);
					}
				} else {
					lastDirection = nextInput;
					isMoving = false;
					anim.SetBool("isMoving", false);
					anim.SetInteger("lastDirection", lastDirection);
				}
				anim.SetInteger("lastDirection", nextInput);
				nextInput = -1;
			}
			if (GUI.RepeatButton (new Rect (0 , Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("leftButton")) && !optionsOn) {
				buttonPushed = true;
				if(spriteMov.getCurrentDirection() == -1) {
					if(validDirections[0]) {
						if(frameDelay == 0) {
							spriteMov.setCurrentDirection(0);
							isMoving = true;
							anim.SetBool("isMoving", true);
						} else
							frameDelay--;
					}
					lastDirection = 0;
					anim.SetInteger("lastDirection", 0);
				} else if (spriteMov.getCurrentDirection() == 2) {
					spriteMov.reverseDirection(0);
					frameDelay = 0;
					nextInput = -1;
					lastDirection = 0;
					anim.SetInteger("lastDirection", 0);
				} else if (spriteMov.getCurrentDirection() != 0) {
					nextInput = 0;
					if(frameDelay != 0)
						frameDelay--;
				}
			}
			if (GUI.RepeatButton (new Rect (Screen.width / 7 , Screen.height * 3 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("upButton")) && !optionsOn) {
				buttonPushed = true;
				if(spriteMov.getCurrentDirection() == -1) {
					if(validDirections[1]) {
						if(frameDelay == 0) {
							spriteMov.setCurrentDirection(1);
							isMoving = true;
							anim.SetBool("isMoving", true);
						} else
							frameDelay--;
					}
					lastDirection = 1;
					anim.SetInteger("lastDirection", 1);
				} else if (spriteMov.getCurrentDirection() == 3) {
					spriteMov.reverseDirection(1);
					frameDelay = 0;
					nextInput = -1;
					lastDirection = 1;
					anim.SetInteger("lastDirection", 1);
				} else if (spriteMov.getCurrentDirection() != 1) {
					nextInput = 1;
					if(frameDelay != 0)
						frameDelay--;
				}
			}
			if (GUI.RepeatButton (new Rect (Screen.width * 2 / 7 , Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("rightButton")) && !optionsOn) {
				buttonPushed = true;
				if(spriteMov.getCurrentDirection() == -1) {
					if(validDirections[2]) {
						if(frameDelay == 0) {
							spriteMov.setCurrentDirection(2);
							isMoving = true;
							anim.SetBool("isMoving", true);
						} else
							frameDelay--;
					}
					lastDirection = 2;
					anim.SetInteger("lastDirection", 2);
				} else if (spriteMov.getCurrentDirection() == 0) {
					spriteMov.reverseDirection(2);
					frameDelay = 0;
					nextInput = -1;
					lastDirection = 2;
					anim.SetInteger("lastDirection", 2);
				} else if (spriteMov.getCurrentDirection() != 2) {
					nextInput = 2;
					if(frameDelay != 0)
						frameDelay--;
				}
			}
			if (GUI.RepeatButton (new Rect (Screen.width / 7 , Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("downButton")) && !optionsOn) {
				buttonPushed = true;
				if(spriteMov.getCurrentDirection() == -1) {
					if(validDirections[3]) {
						if(frameDelay == 0) {
							spriteMov.setCurrentDirection(3);
							isMoving = true;
							anim.SetBool("isMoving", true);
						} else
							frameDelay--;
					}
					lastDirection = 3;
					anim.SetInteger("lastDirection", 3);
				} else if (spriteMov.getCurrentDirection() == 1) {
					spriteMov.reverseDirection(3);
					frameDelay = 0;
					nextInput = -1;
					lastDirection = 3;
					anim.SetInteger("lastDirection", 3);
				} else if (spriteMov.getCurrentDirection() != 3) {
					nextInput = 3;
					if(frameDelay != 0)
						frameDelay--;
				}
			}
			if (GUI.RepeatButton (new Rect (Screen.width * 104 / 140, Screen.height * 159 / 200, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("aButton")) && spriteMov.getCurrentDirection() == -1) {
				nextInput = 4;
			}
			if (GUI.RepeatButton (new Rect (Screen.width * 119 / 140, Screen.height * 130 / 200, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("bButton")) && spriteMov.getCurrentDirection() == -1) {
				nextInput = 5;
			}
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 11 / 12, 0, Screen.width / 12, Screen.height / 10), "", skin.FindStyle ("optionsButton"))) {
			optionsPushed();
		}
	}

	public void updateDirections(Bounds bounds) {
		float spriteX = bounds.center.x;
		float spriteY = bounds.center.y;
		validDirections[0] = !collidableObjects.testBounds(new Vector2(spriteX - bounds.size.x, spriteY));
		validDirections[1] = !collidableObjects.testBounds(new Vector2(spriteX, spriteY + bounds.size.y));
		validDirections[2] = !collidableObjects.testBounds(new Vector2(spriteX + bounds.size.x, spriteY));
		validDirections[3] = !collidableObjects.testBounds(new Vector2(spriteX, spriteY - bounds.size.y));
	}

	public bool testCommand(Bounds bounds, int dir) {
		float spriteX = bounds.center.x;
		float spriteY = bounds.center.y;
		switch (dir) {
			case 0: // left
				if(collidableObjects.testBounds(new Vector2(spriteX - bounds.size.x, spriteY)))
			   		return false;
				break;
			case 1: // up
				if(collidableObjects.testBounds(new Vector2(spriteX, spriteY + bounds.size.y)))
			   		return false;
				break;
			case 2: // right
				if(collidableObjects.testBounds(new Vector2(spriteX + bounds.size.x, spriteY)))
			   		return false;
				break;
			case 3: // down
				if(collidableObjects.testBounds(new Vector2(spriteX, spriteY - bounds.size.y)))
			   		return false;
				break;
		}
		return true;
	}

	void aPushed(Bounds bounds, int dir) {
		float spriteX = bounds.center.x;
		float spriteY = bounds.center.y;
		int interactableIndex;
		switch (dir) {
			case 0: // left
				interactableIndex = collidableObjects.findInteractable(new Vector2(spriteX - bounds.size.x, spriteY));
				if(interactableIndex != -1)
					npcChat = true;
				break;
			case 1: // up
				interactableIndex = collidableObjects.findInteractable(new Vector2(spriteX, spriteY + bounds.size.y));
				if(interactableIndex != -1)
					npcChat = true;
				break;
			case 2: // right
				interactableIndex = collidableObjects.findInteractable(new Vector2(spriteX + bounds.size.x, spriteY));
				if(interactableIndex != -1)
					npcChat = true;
				break;
			case 3: // down
				interactableIndex = collidableObjects.findInteractable(new Vector2(spriteX, spriteY - bounds.size.y));
				if(interactableIndex != -1)
					npcChat = true;
				break;
		}
	}

	public bool charMoving {
		get {
			return isMoving;
		}
	}

	public int NextInput {
		get {
			return nextInput;
		}
	}

	public int LastDirection {
		get {
			return lastDirection;
		}
	}

	public void resetAnimation() {
		anim.SetBool("isMoving", isMoving);
		anim.SetInteger("lastDirection", lastDirection);
	}

	void bPushed() {

	}

	void optionsPushed() {
		optionsOn = !optionsOn;
		NGUITools.SetActive(optionsPanel, optionsOn);
	}
}
