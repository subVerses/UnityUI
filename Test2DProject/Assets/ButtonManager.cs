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
	public DialogueOptions dialogue = new DialogueOptions();
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

//		dialogXML = new XmlDocument();
//		dialogXML.Load(@"Assets\DialogXML\subVerses-Scene01.xml");
//		nodeReader = new XmlNodeReader (dialogXML);
//		XmlReaderSettings settings = new XmlReaderSettings();
//		settings.ValidationType = ValidationType.Schema;
//		XmlReader reader = XmlReader.Create (nodeReader, settings);
//		for(int i = 0; i < 10; i++) {
//			reader.Read ();
//			switch (reader.NodeType) {
//				case XmlNodeType.Element:
//						Debug.Log(String.Format("1<{0}>", reader.Name));
//						break;
//				case XmlNodeType.Text:
//						Debug.Log(String.Format ("2" + reader.Value));
//						break;
//				case XmlNodeType.CDATA:
//						Debug.Log(String.Format("3<![CDATA[{0}]]>", reader.Value));
//						break;
//				case XmlNodeType.ProcessingInstruction:
//						Debug.Log(String.Format("4<?{0} {1}?>", reader.Name, reader.Value));
//						break;
//				case XmlNodeType.Comment:
//						Debug.Log(String.Format("5<!--{0}-->", reader.Value));
//						break;
//				case XmlNodeType.XmlDeclaration:
//						Debug.Log(String.Format("6<?xml version='1.0'?>"));
//						break;
//				case XmlNodeType.Document:
//						break;
//				case XmlNodeType.DocumentType:
//						Debug.Log(String.Format("<7!DOCTYPE {0} [{1}]", reader.Name, reader.Value));
//						break;
//				case XmlNodeType.EntityReference:
//						Debug.Log(String.Format ("8" + reader.Name));
//						break;
//				case XmlNodeType.EndElement:
//				         Debug.Log(String.Format("9</{0}>", reader.Name));
//						break;
//			}
//		}
	}

	static int nextScene = 0;

	// Update is called once per frame
	void Update () {

	}

	IEnumerator DialogueUpdate(int seconds) {
			yield return new WaitForSeconds(seconds);
	}



	void OnGUI () {
		bool enabledBackup = GUI.enabled;

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
		GUI.depth = 0;
		//GUI text boxes
		//		GUI.Label (new Rect (Screen.width * 1 / 20, Screen.height * 9 / 20, Screen.width / 4, Screen.height / 10), "GUARD0107", skin.FindStyle("yourTitle"));
		GUI.Label (new Rect (Screen.height * 1 / 20, 0, Screen.width / 6, Screen.height / 10), "SUSPICION", skin.FindStyle("suspicionLabel"));
		GUI.Label (new Rect (Screen.width * 7 / 13, 0, Screen.width / 4, Screen.height / 7), "JUAN RIVIERA\nOficinista", skin.FindStyle("theirTitle"));
		GUI.Label (new Rect (Screen.width * 51 / 160, Screen.height * 11 / 20, Screen.width * 7 / 15, Screen.height * 2 / 5), yourResponse, skin.FindStyle("yourResponse"));
		GUI.Label (new Rect (Screen.width * 1 / 60, Screen.height * 3 / 20, Screen.width * 23 / 45, Screen.height * 1 / 3), theirResponse, skin.FindStyle("theirResponse"));


		if (GUI.Button (new Rect (Screen.width * 4 / 5, 0, Screen.width / 5, Screen.height / 6), dialogue.Choice1, skin.FindStyle ("button"))) {
			this.CheckAnswers(1);
			SetSpeech ();
		}

		if (GUI.Button (new Rect (Screen.width * 4 / 5, Screen.height / 6, Screen.width / 5, Screen.height / 6), dialogue.Choice2, skin.FindStyle ("button"))) {
			this.CheckAnswers(2);
			SetSpeech ();
		}

		if (GUI.Button (new Rect (Screen.width * 4 / 5, Screen.height * 2 / 6, Screen.width / 5, Screen.height / 6), dialogue.Choice3, skin.FindStyle ("button"))) {
			this.CheckAnswers(3);
			SetSpeech ();
		}

		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*3/6,Screen.width/5,Screen.height/6), dialogue.Choice4, skin.FindStyle("button"))) {
			Application.LoadLevel (2);
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*4/6,Screen.width/5,Screen.height/6), "Items", skin.FindStyle("button"))) {
			Application.LoadLevel (2);
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*5/6,Screen.width/5,Screen.height/6), "Back", skin.FindStyle("button"))) {
			//Debug.Log ("Untoggled.");
		}

		GUI.enabled = enabledBackup;
	}

	//sets the dialogue tree, to be refactored later
	public void SetSpeech(){

		if (nextScene == 0) {
			theirResponse = "Buenos Dias.";
			yourResponse = "...";
			dialogue.choice1 = "Buenos Dias";
			dialogue.choice2 = "Bienvenidos";
			dialogue.choice3 = "Hola";
		}
		else if (nextScene == 1) {
			StartCoroutine(DialogueUpdate(1));
			theirResponse = "Aqui.";
			StartCoroutine(DialogueUpdate(1));
			yourResponse = "...";
			dialogue.choice1 = "[SCAN]";
			dialogue.choice2 = "";
			dialogue.choice3 = "";
		}
		else if (nextScene == 2) {
			StartCoroutine(DialogueUpdate(1));
			theirResponse = "Gracias.";
			StartCoroutine(DialogueUpdate(1));
			yourResponse = ":)";
			StartCoroutine (DialogueUpdate(2)); //COMPLETE
		}
	}

	//checks the users answers
	public void CheckAnswers(int choice){
		
		GameObject go = GameObject.Find ("SpeechRecognition");
		SpeechListener sl = go.GetComponent<SpeechListener>();


		string ans = "";
		string compare = "";

		if (nextScene == 1) {	//doesnt work perfectly yet, a little buggy due to testing in speechlistener
			ans="[SCAN]";
		}
		else {
			ans = sl.LastResults;
		}

		//checks the users choice
		switch(choice){
			case 1: 
				compare = dialogue.Choice1.ToLower();;
				yourResponse = dialogue.Choice1;
				suspicionBar.addBonus(.5f);
				break;
			case 2:
				compare = dialogue.Choice2.ToLower();
				yourResponse = dialogue.Choice2;
				suspicionBar.addBonus(.5f);
				break;
			case 3:
				compare = dialogue.Choice3.ToLower();
				yourResponse = dialogue.Choice3;
				suspicionBar.addBonus(.5f);
				break;
			case 4:
				break;
		}

		//compares the answer from speech and the users' button selection
		if(ans.Contains(compare)){
			nextScene++;
			//Debug.Log ("Answer Reached.");
		}
		else{
			yourResponse = "...?";
		}
	}
}
