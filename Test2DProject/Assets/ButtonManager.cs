using UnityEngine;
using System.Collections;


public class ButtonManager : MonoBehaviour {

	public GUISkin skin = null;

	string bouton1 = "Click me!";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.Label (new Rect (Screen.width / 18, Screen.height * 9 / 20, Screen.width / 4, Screen.height / 10), "GUARD0107", skin.FindStyle("label"));
		GUI.Label (new Rect (Screen.width * 4/11, Screen.height * 11 / 20, Screen.width *2/ 5, Screen.height* 2 / 5), "JUAN RIVIERA\nOficinista", skin.FindStyle("textfield"));

		if (GUI.Button (new Rect (Screen.width*4/5,0,Screen.width/5,Screen.height/6), bouton1, skin.FindStyle("button"))) {
			bouton1 = "You Clicked Me!";
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height/6,Screen.width/5,Screen.height/6), "Welcome", skin.FindStyle("button"))) {

			Application.LoadLevel (2);
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*2/6,Screen.width/5,Screen.height/6), "Microphone", skin.FindStyle("button"))) {
			SpeechRecognition.StartListening();
			Application.LoadLevel (2);
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*3/6,Screen.width/5,Screen.height/6), "Scan", skin.FindStyle("button"))) {
			Application.LoadLevel (2);
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*4/6,Screen.width/5,Screen.height/6), "Items", skin.FindStyle("button"))) {
			Application.LoadLevel (2);
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*5/6,Screen.width/5,Screen.height/6), "Exit", skin.FindStyle("button"))) {
			Application.LoadLevel (2);
		}
	}
}
