using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.IO;

public class ButtonManager : MonoBehaviour {

	public GUISkin skin;
	public String output;
	public float fadeWaitTime;
	public Color fadeColor;
	public float fadeAlpha;
	public int fadeDirection; //-1 = Fade in, 1 = Fade out
	public Texture2D fadeTexture;
	public Texture2D successTexture;
	public Texture2D failureTexture;
	public SuspicionBarController suspicionBar;
	public String yourResponse;
	public String theirResponse;
	public ResponseListener listener;
	public DialogueSequence dialogueTree = new DialogueSequence();
	//	public XmlDocument dialogXML;
	//	public XmlNodeReader nodeReader;

	// Use this for initialization
	void Start () {
		SpeechRecognition.AddSpeechRecognitionListeren(listener);
		fadeAlpha = 1;
		fadeDirection = -1;
		fadeColor = Color.white;
		fadeColor.a = fadeAlpha;
		fadeWaitTime = 5f;
		SetSpeech ();
		TypeText(yourResponse);
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator DialogueUpdate(int seconds) {
		yield return new WaitForSeconds(seconds);
	}


	float letterPause = 0.2f; //PROBLEMS BE HERE
	private string currentWord = "";

	public void AddText(string newText){
		yourResponse = newText;
		TypeText(yourResponse);
	}

	private IEnumerator TypeText (string compareWord){
		foreach (char c in compareWord){
		if (yourResponse != compareWord) break;
			currentWord += c;
			yield return new WaitForSeconds(letterPause);
		}
	} // END THE PROBLEMS

	static int nextScene = 0;
	DialogueScene currentScene;
	string choice1 = "";
	string choice2 = "";


	void OnGUI () {
		bool enabledBackup = GUI.enabled;

		GameObject go = GameObject.Find ("SpeechRecognition");
		SpeechListener sl = go.GetComponent<SpeechListener>();


		if(suspicionBar.isSuspicious()) {
			Color failureColor = Color.red;
			failureColor.a = .5f;
			GUI.color = failureColor;
			GUI.depth = -2;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),failureTexture);
		} else if (yourResponse == ":)"){
			Color successColor = Color.green;
			successColor.a = .5f;
			GUI.color = successColor;
			GUI.depth = -3;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),successTexture);
		}

		//fade in/out large GUI box code in front of all UI elements
		if(fadeAlpha != 0 && fadeAlpha != 1)
			suspicionBar.reset ();
		fadeAlpha += fadeDirection * Time.deltaTime / fadeWaitTime;
		fadeAlpha = Mathf.Clamp01(fadeAlpha);
		fadeColor.a = fadeAlpha;
		GUI.color = fadeColor;
		GUI.depth = -2;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);
		//Other GUI elements fade in/out with opposite alpha as large GUI box
		fadeColor.a = 1 - fadeAlpha;
		GUI.color = fadeColor;
		GUI.depth = -2;
		//GUI text boxes
		GUI.Label (new Rect (Screen.height * 1 / 20, 0, Screen.width / 6, Screen.height / 10), "SUSPICION", skin.FindStyle("suspicionLabel"));
		GUI.Label (new Rect (Screen.width * 7 / 13, 0, Screen.width / 4, Screen.height / 7), "JUAN RIVIERA\nOficinista", skin.FindStyle("theirTitle"));
		GUI.Label (new Rect (Screen.width * 51 / 160, Screen.height * 11 / 20, Screen.width * 7 / 15, Screen.height * 2 / 5), yourResponse, skin.FindStyle("yourResponse"));
		GUI.Label (new Rect (Screen.width * 1 / 60, Screen.height * 3 / 20, Screen.width * 23 / 45, Screen.height * 1 / 3), theirResponse, skin.FindStyle("theirResponse"));

		if (GUI.Button (new Rect (Screen.width * 4 / 5, 0, Screen.width / 5, Screen.height / 6), choice1, skin.FindStyle ("button"))) {

			if(nextScene == 1 || nextScene == 2){
				nextScene++;}
			else{
				sl.showMic = true;
				this.CheckAnswers(1);}
			SetSpeech ();

		}

		if (GUI.Button (new Rect (Screen.width * 4 / 5, Screen.height / 6, Screen.width / 5, Screen.height / 6), choice2, skin.FindStyle ("button"))) {

			SetSpeech ();
			if(nextScene == 1 || nextScene == 2){
					}
			else{
				sl.showMic = true;
				this.CheckAnswers(2);}
			SetSpeech();
		}

		if (GUI.Button (new Rect (Screen.width * 4 / 5, Screen.height * 2 / 6, Screen.width / 5, Screen.height / 6), "", skin.FindStyle ("button"))) {
			//SetSpeech ();
			//this.CheckAnswers(3);
		}

		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*3/6,Screen.width/5,Screen.height/6), "", skin.FindStyle("button"))) {
			//Application.LoadLevel (2);
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*4/6,Screen.width/5,Screen.height/6), "Items", skin.FindStyle("button"))) {
			//Application.LoadLevel (2);
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*5/6,Screen.width/5,Screen.height/6), "Back", skin.FindStyle("button"))) {
			//Debug.Log ("Untoggled.");
		}
		Debug.Log (nextScene);
		GUI.enabled = enabledBackup;
	}

	//sets the dialogue tree, to be refactored later
	public void SetSpeech(){
		if (nextScene == 0) {
			theirResponse = "Buenos Dias.";
			yourResponse = "...";//yourResponse = "...";
			dialogueTree.AddScene();
			dialogueTree.SetCurrentScene(0);
			dialogueTree.CurrentScene.AddChoice ("Buenos Dias",1);
			choice1 = dialogueTree.CurrentScene.Dialogue[0].Text;
			dialogueTree.CurrentScene.AddChoice ("Bienvenidos",2);
			choice2 = dialogueTree.CurrentScene.Dialogue[1].Text;
		}
		else if (nextScene == 1) {
			theirResponse = "Aqui.";
			yourResponse = "...";//AddText("...")
			dialogueTree.AddScene();
			dialogueTree.SetCurrentScene(1);
			dialogueTree.CurrentScene.AddChoice ("[SCAN]",1);
			choice1 = dialogueTree.CurrentScene.Dialogue[0].Text;
			dialogueTree.CurrentScene.AddChoice ("",2);
			choice2 = dialogueTree.CurrentScene.Dialogue[1].Text;
		}
		else if (nextScene == 2) {
			theirResponse = "Gracias.";
			yourResponse = ":)";//COMPLETE
			dialogueTree.AddScene();
			dialogueTree.SetCurrentScene(2);
			dialogueTree.CurrentScene.AddChoice ("Dialogue Complete",1);
			choice1 = dialogueTree.CurrentScene.Dialogue[0].Text;

		}
	}


	//checks the users answers
	public void CheckAnswers(int choice){

		GameObject go = GameObject.Find ("SpeechRecognition");
		SpeechListener sl = go.GetComponent<SpeechListener>();

		string ans = "";
		string compare = "";
		ans = sl.LastResults;

		//checks the users choice
		switch(choice){
			case 1: 
				compare = dialogueTree.CurrentScene.Dialogue[0].Text;
				yourResponse = compare + "."; //AddText(compare + ".");
				suspicionBar.addBonus(.5f);
				break;
			case 2:
				compare = dialogueTree.CurrentScene.Dialogue[1].Text;
				yourResponse = compare + ".";
				suspicionBar.addBonus(.5f);
				break;
			case 3:
				break;
			case 4:
				break;
		}

		compare = compare.ToLower();

		//compares the answer from speech and the users' button selection
		if(ans.Contains(compare)){

			nextScene++;
		}
		else{
			yourResponse = "...?";
		}
	}
}
