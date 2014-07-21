using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	public GUISkin skin;
	private int currentCommand;
	private bool commandChanged;

	// Use this for initialization
	void Start () {
		currentCommand = -1;
		commandChanged = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		commandChanged = false;
		if (GUI.RepeatButton (new Rect (Screen.width * 4 / 7, Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("leftButton"))) {
			currentCommand = 0;
			commandChanged = true;
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 6 / 7, Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("rightButton"))) {
			currentCommand = 2;
			commandChanged = true;
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 5 / 7, Screen.height * 3 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("upButton"))) {
			currentCommand = 1;
			commandChanged = true;
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 5 / 7, Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("downButton"))) {
			currentCommand = 3;
			commandChanged = true;
		}
		if (GUI.RepeatButton (new Rect (0 , Screen.height * 3 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("aButton"))) {
			currentCommand = 4;
			commandChanged = true;
		}
		if (GUI.RepeatButton (new Rect (0 , Screen.height * 4 / 5, Screen.width / 7, Screen.height / 5), "", skin.FindStyle ("bButton"))) {
			currentCommand = 5;
			commandChanged = true;
		}
		if (GUI.RepeatButton (new Rect (Screen.width * 11 / 12, 0, Screen.width / 12, Screen.height / 10), "", skin.FindStyle ("optionsButton"))) {
			currentCommand = 6;
			commandChanged = true;
		}

		if(!commandChanged && currentCommand != -1)
			currentCommand = -1;
	}

	public int getCommand() {
		return currentCommand;
	}
}
