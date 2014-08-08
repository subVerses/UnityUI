using UnityEngine;
using System.Collections;

public class CharacterInteraction : MonoBehaviour {

	public CollidableObjects collidableObjects;
	public SpriteMover spriteMov;
	public GUISkin skin;

	int lastDirection;
	int frameCount;
	string testText;
	bool sampleText;

	void Start () {
		lastDirection = 1;
		sampleText = false;
		frameCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
//			collidableObjects.updateCollisionBoxes(spriteMov.getBounds().min.y);
	}

	void OnGUI() {
		if(sampleText && frameCount == 0) {
			sampleText = false;
			frameCount = 200;
		}
		if(frameCount != 0) {
			GUI.Label (new Rect(Screen.width / 7 , Screen.height / 20, Screen.width * 5 / 7, Screen.height / 5), "Hi! Nice to meet you!", skin.FindStyle("sampleGUI"));
			frameCount--;
		} 
		if (GUI.RepeatButton (new Rect (0 , Screen.height * 7 / 10, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("leftButton")) && frameCount == 0) {
			if(testCommand(spriteMov.getBounds(), 0) && !spriteMov.setCurrentDirection(0) && testCommand(spriteMov.getNextBounds(), 0)) //left => currentDirectionalCommand = 0
				spriteMov.setNextDirection(0);
			lastDirection = 0;
		}
		if (GUI.RepeatButton (new Rect (Screen.width / 7 , Screen.height * 3 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("upButton")) && frameCount == 0) {
			if(testCommand(spriteMov.getBounds(), 1) && !spriteMov.setCurrentDirection(1) && testCommand(spriteMov.getNextBounds(), 1)) //up => currentDirectionalCommand = 1
				spriteMov.setNextDirection(1);
			lastDirection = 1;
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 2 / 7 , Screen.height * 7 / 10, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("rightButton")) && frameCount == 0) {
			if(testCommand(spriteMov.getBounds(), 2) && !spriteMov.setCurrentDirection(2) && testCommand(spriteMov.getNextBounds(), 2)) //right => currentDirectionalCommand = 2
				spriteMov.setNextDirection(2);
			lastDirection = 2;
		}
		if (GUI.RepeatButton (new Rect (Screen.width / 7 , Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("downButton")) && frameCount == 0) {
			if(testCommand(spriteMov.getBounds(), 3) && !spriteMov.setCurrentDirection(3) && testCommand(spriteMov.getNextBounds(), 3)) //down => currentDirectionalCommand = 3=
				spriteMov.setNextDirection(3);
			lastDirection = 3;
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 5 / 7, Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("aButton")) && spriteMov.getCurrentDirection() == -1) {
			aPushed(spriteMov.getBounds(), lastDirection);
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 6 / 7, Screen.height * 3 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("bButton")) && spriteMov.getCurrentDirection() == -1) {
			bPushed();
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 11 / 12, 0, Screen.width / 12, Screen.height / 10), "", skin.FindStyle ("optionsButton"))) {
			optionsPushed();
		}
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

	void bPushed() {

	}

	void optionsPushed() {

	}
}
