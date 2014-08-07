using UnityEngine;
using System.Collections;

public class CharacterInteraction : MonoBehaviour {

	public CollidableObjects collidableObjects;
	public SpriteMover spriteMov;
	public GUISkin skin;
	
	// Update is called once per frame
	void Update () {
//			collidableObjects.updateCollisionBoxes(spriteMov.getBounds().min.y);
	}

	void OnGUI() {

		if (GUI.RepeatButton (new Rect (0 , Screen.height * 7 / 10, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("leftButton"))) {
			if(testCommand(spriteMov.getBounds(), 0) && !spriteMov.setCurrentDirection(0) && testCommand(spriteMov.getNextBounds(), 0)) //left => currentDirectionalCommand = 0
				spriteMov.setNextDirection(0);
		}
		if (GUI.RepeatButton (new Rect (Screen.width / 7 , Screen.height * 3 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("upButton"))) {
			if(testCommand(spriteMov.getBounds(), 1) && !spriteMov.setCurrentDirection(1) && testCommand(spriteMov.getNextBounds(), 1)) //up => currentDirectionalCommand = 1
				spriteMov.setNextDirection(1);
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 2 / 7 , Screen.height * 7 / 10, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("rightButton"))) {
			if(testCommand(spriteMov.getBounds(), 2) && !spriteMov.setCurrentDirection(2) && testCommand(spriteMov.getNextBounds(), 2)) //right => currentDirectionalCommand = 2
				spriteMov.setNextDirection(2);
		}
		if (GUI.RepeatButton (new Rect (Screen.width / 7 , Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("downButton"))) {
			if(testCommand(spriteMov.getBounds(), 3) && !spriteMov.setCurrentDirection(3) && testCommand(spriteMov.getNextBounds(), 3)) //down => currentDirectionalCommand = 3=
				spriteMov.setNextDirection(3);
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 5 / 7, Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("aButton"))) {
			aPushed();
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 6 / 7, Screen.height * 3 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("bButton"))) {
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

	void aPushed() {

	}

	void bPushed() {

	}

	void optionsPushed() {

	}
}
