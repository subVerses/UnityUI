using UnityEngine;
using System.Collections;

public class CharacterInteraction : MonoBehaviour {

	public CollidableObjects collidableObjects;
	public SpriteMover spriteMov;
	public GUISkin skin;
	public GameObject panel;

	int lastDirection; //last direction chosen by player
	int nextInput; //next input queued up after the current one
	int frameCount; //frames left until GUI disappears
	bool sampleText; //should sample chat GUI be activated?
	bool isMoving; //is the character moving?
	bool optionsOn;
	bool[] validDirections; //which directions can the character currently move in?

	void Start () {
		lastDirection = 1;
		nextInput = -1;
		frameCount = 0;
		sampleText = false;
		isMoving = false;
		validDirections = new bool[4] { true,   //right
										true,   //up
										true,   //left
										true }; //down
	}

	void FixedUpdate() {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		if(optionsOn) {

		} else {
			if(isMoving && spriteMov.getCurrentDirection() == -1) { //if character is stopped and has just finished moving, update viable directions
				isMoving = false;
				updateDirections (spriteMov.getNextBounds());
			}
			if(frameCount != 0) { //if chat GUI should still be displayed, display it
				GUI.Label (new Rect(Screen.width / 7 , Screen.height / 20, Screen.width * 5 / 7, Screen.height / 5), "Hi! Nice to meet you!", skin.FindStyle("sampleGUI"));
				frameCount--;
			} else if(sampleText) { //if chat GUI should start to be displayed, activate it
				sampleText = false;
				frameCount = 200;
			}
			if(!isMoving && nextInput != -1 && spriteMov.getCurrentDirection() == -1) { //if a new input is queued up and the character is currently idle, make the character do the new input
				if(nextInput == 4) 
					aPushed(spriteMov.getBounds(), lastDirection);
				else if(nextInput == 5)
					bPushed ();
				else if(testCommand(spriteMov.getNextBounds(), nextInput)) {
					spriteMov.setCurrentDirection(nextInput);
				}
			}
			if(isMoving && spriteMov.getCurrentDirection() == -1) { //if character is stopped and has just finished moving, update viable directions
				isMoving = false;
				updateDirections (spriteMov.getNextBounds());
			}
			if(frameCount != 0) { //if chat GUI should still be displayed, display it
				GUI.Label (new Rect(Screen.width / 7 , Screen.height / 20, Screen.width * 5 / 7, Screen.height / 5), "Hi! Nice to meet you!", skin.FindStyle("sampleGUI"));
				Application.LoadLevel ("NPCInteraction"); //loading the NPC scene here, although not sure if this is the best way
				frameCount--;
			} else if(sampleText) { //if chat GUI should start to be displayed, activate it
				sampleText = false;
				frameCount = 200;
			}
			if(!isMoving && nextInput != -1 && spriteMov.getCurrentDirection() == -1) { //if a new input is queued up and the character is currently idle, make the character do the new input
				if(nextInput == 4) 
					aPushed(spriteMov.getBounds(), lastDirection);
				else if(nextInput == 5)
					bPushed ();
				else if(testCommand(spriteMov.getNextBounds(), nextInput)) {
					spriteMov.setCurrentDirection(nextInput);
					isMoving = true;
					lastDirection = nextInput;
				} else
					lastDirection = nextInput;
				nextInput = -1;
			}
			if (GUI.RepeatButton (new Rect (0 , Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("leftButton")) && frameCount == 0 && !optionsOn) {
				if(spriteMov.getCurrentDirection() == -1) {
					if(validDirections[0]) {
						spriteMov.setCurrentDirection(0);
						isMoving = true;
					}
					lastDirection = 0;
				} else if (spriteMov.getCurrentDirection() == 2) {
					spriteMov.reverseDirection(0);
					nextInput = -1;
					lastDirection = 0;
				} else if (spriteMov.getCurrentDirection() != 0)
					nextInput = 0;
			}
			if (GUI.RepeatButton (new Rect (Screen.width / 7 , Screen.height * 3 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("upButton")) && frameCount == 0 && !optionsOn) {
				if(spriteMov.getCurrentDirection() == -1) {
					if(validDirections[1]) {
						spriteMov.setCurrentDirection(1);
						isMoving = true;
					}
					lastDirection = 1;
				} else if (spriteMov.getCurrentDirection() == 3) {
					spriteMov.reverseDirection(1);
					nextInput = -1;
					lastDirection = 1;
				} else if (spriteMov.getCurrentDirection() != 1)
					nextInput = 1;
			}
			if (GUI.RepeatButton (new Rect (Screen.width * 2 / 7 , Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("rightButton")) && frameCount == 0 && !optionsOn) {
				if(spriteMov.getCurrentDirection() == -1) {
					if(validDirections[2]) {
						spriteMov.setCurrentDirection(2);
						isMoving = true;
					}
					lastDirection = 2;
				} else if (spriteMov.getCurrentDirection() == 0) {
					spriteMov.reverseDirection(2);
					nextInput = -1;
					lastDirection = 2;
				} else if (spriteMov.getCurrentDirection() != 2)
					nextInput = 2;
			}
			if (GUI.RepeatButton (new Rect (Screen.width / 7 , Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("downButton")) && frameCount == 0 && !optionsOn) {
				if(spriteMov.getCurrentDirection() == -1) {
					if(validDirections[3]) {
						spriteMov.setCurrentDirection(3);
						isMoving = true;
					}
					lastDirection = 3;
				} else if (spriteMov.getCurrentDirection() == 1) {
					spriteMov.reverseDirection(3);
					nextInput = -1;
					lastDirection = 3;
				} else if (spriteMov.getCurrentDirection() != 3)
					nextInput = 3;
			}
			if (GUI.RepeatButton (new Rect (Screen.width * 5 / 7, Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("aButton")) && spriteMov.getCurrentDirection() == -1) {
				nextInput = 4;
			}
			if (GUI.RepeatButton (new Rect (Screen.width * 6 / 7, Screen.height * 3 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("bButton")) && spriteMov.getCurrentDirection() == -1) {
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
					sampleText = true;
				break;
			case 1: // up
				interactableIndex = collidableObjects.findInteractable(new Vector2(spriteX, spriteY + bounds.size.y));
				if(interactableIndex != -1)
					sampleText = true;
				break;
			case 2: // right
				interactableIndex = collidableObjects.findInteractable(new Vector2(spriteX + bounds.size.x, spriteY));
				if(interactableIndex != -1)
					sampleText = true;
				break;
			case 3: // down
				interactableIndex = collidableObjects.findInteractable(new Vector2(spriteX, spriteY - bounds.size.y));
				if(interactableIndex != -1)
					sampleText = true;
				break;
		}
	}

	public bool charMoving {
		get {
			return isMoving;
		}
	}

	void bPushed() {

	}

	void optionsPushed() {
		optionsOn = !optionsOn;
		NGUITools.SetActive(panel, optionsOn);
	}
}
