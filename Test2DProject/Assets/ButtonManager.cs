using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.IO;

public class ButtonManager : MonoBehaviour {

	public GUISkin skin = null;
	public ResponseListener listener;
	public XmlDocument dialogXML;
	public XmlNodeReader nodeReader;
	public String output;

	string bouton1 = "Click me to record!";

	// Use this for initialization
	void Start () {
		SpeechRecognition.AddSpeechRecognitionListeren(listener);
		dialogXML = new XmlDocument();
		dialogXML.Load(@"Assets\DialogXML\subVerses-Scene01.xml");
		nodeReader = new XmlNodeReader (dialogXML);
		XmlReaderSettings settings = new XmlReaderSettings();
		settings.ValidationType = ValidationType.Schema;
		XmlReader reader = XmlReader.Create (nodeReader, settings);
		for(int i = 0; i < 10; i++) {
			reader.Read ();
			switch (reader.NodeType) {
				case XmlNodeType.Element:
						Debug.Log(String.Format("1<{0}>", reader.Name));
						break;
				case XmlNodeType.Text:
						Debug.Log(String.Format ("2" + reader.Value));
						break;
				case XmlNodeType.CDATA:
						Debug.Log(String.Format("3<![CDATA[{0}]]>", reader.Value));
						break;
				case XmlNodeType.ProcessingInstruction:
						Debug.Log(String.Format("4<?{0} {1}?>", reader.Name, reader.Value));
						break;
				case XmlNodeType.Comment:
						Debug.Log(String.Format("5<!--{0}-->", reader.Value));
						break;
				case XmlNodeType.XmlDeclaration:
						Debug.Log(String.Format("6<?xml version='1.0'?>"));
						break;
				case XmlNodeType.Document:
						break;
				case XmlNodeType.DocumentType:
						Debug.Log(String.Format("<7!DOCTYPE {0} [{1}]", reader.Name, reader.Value));
						break;
				case XmlNodeType.EntityReference:
						Debug.Log(String.Format ("8" + reader.Name));
						break;
				case XmlNodeType.EndElement:
				         Debug.Log(String.Format("9</{0}>", reader.Name));
						break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		bool enabledBackup = GUI.enabled;

		GUI.Label (new Rect (Screen.width / 18, Screen.height * 9 / 20, Screen.width / 4, Screen.height / 10), "GUARD0107", skin.FindStyle("label"));
		GUI.Label (new Rect (Screen.width / 18, 0, Screen.width / 4, Screen.height / 10), "SUSPICION", skin.FindStyle("label"));
		GUI.Label (new Rect (Screen.width * 4/11, Screen.height * 11 / 20, Screen.width *2/ 5, Screen.height* 1 / 5), "JUAN RIVIERA\nOficinista", skin.FindStyle("textfield"));

		if (GUI.Button (new Rect (Screen.width*4/5,0,Screen.width/5,Screen.height/6), "Click me to record!", skin.FindStyle("button"))) {
			SpeechRecognition.StartListening();
			bouton1 = listener.getResults();
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height/6,Screen.width/5,Screen.height/6), "Click me to Stop!", skin.FindStyle("button"))) {
			SpeechRecognition.StopListening();
		}
		
		if (GUI.Button (new Rect (Screen.width*4/5,Screen.height*2/6,Screen.width/5,Screen.height/6), bouton1, skin.FindStyle("button"))) {
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

		GUI.enabled = enabledBackup;
	}
}
