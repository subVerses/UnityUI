﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechListener : MonoBehaviour, ISpeechRecognitionListener {

	private GUIStyle fontStyle = new GUIStyle();
	
	private string lastResults = "buenos dias";

	public string LastResults {
		get {
			return lastResults;
		}
	}
	
	//private static Vector2 relativePosition = new Vector2(1,1); //used for the SpeechDictionary example
	
	private Rect touchZone = new Rect(0,Screen.height*4/6f,Screen.width/6f,Screen.height/4f);
	
	private ScreenOrientation currentOrientation = ScreenOrientation.Landscape;

	private Texture Mic;

	private Texture MicActive;

	private Texture MicVerified;

	//example of ISpeechRecognitionListener
	public void OnBeginningOfSpeech ()
	{}
	
	public void OnBufferReceived (byte[] buffer)
	{}
	
	public void OnEndOfSpeech ()
	{}
	
	public void OnError (int error, string errorMessage)
	{
		lastResults = errorMessage;
	}
	
	public void OnEvent (int eventType, System.Collections.Generic.Dictionary<string, string> bundle)
	{}
	
	public void OnPartialResults (System.Collections.Generic.Dictionary<string, string> partialResults)
	{}
	
	public void OnReadyForSpeech (System.Collections.Generic.Dictionary<string, string> bundle)
	{}
	
	public void OnResults (string[] results)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		foreach(string s in results){
			sb.Append(s);
			sb.Append(", ");
		}
		lastResults = sb.ToString();
	}
	
	public void OnRmsChanged (float rmsdB)
	{}

	//can change these colors later
	public void OnChangeState (SpeechRecognition.State newState)
	{
		if(newState == SpeechRecognition.State.NOT_INITIALIZED){
			Camera.main.backgroundColor = Color.red;
		}else if(newState == SpeechRecognition.State.IDLE){
			Camera.main.backgroundColor = Color.blue;
		}else if(newState == SpeechRecognition.State.LISTENING_TO_SOUND){
			Camera.main.backgroundColor = Color.yellow;
		}else if(newState == SpeechRecognition.State.LISTINING_TO_SPEECH_INIT){
			Camera.main.backgroundColor = new Color(1f,0.5f,0);
		}else if(newState == SpeechRecognition.State.LISTENING_TO_SPEECH){
			Camera.main.backgroundColor = Color.green;
		}
	}


	//ways to change the microphone sprite/texture based on speech recognition!!!


	// Use this for initialization
	void Start () {
		SpeechRecognition.AddSpeechRecognitionListeren(this);

		SpeechRecognition.SetTouchToListenZone(touchZone,true);

		Mic = (Texture) Resources.Load ("MicrophoneCircle");
		MicActive = (Texture) Resources.Load ("MicrophoneCircleActive");
		MicVerified = (Texture) Resources.Load ("MicrophoneCircleVerified");
	}


	// Update is called once per frame
	void Update () {
		if(SpeechRecognition.CommandRecognized("HELLO")){
			lastResults = "Hello.";
			//relativePosition = new Vector2(relativePosition.x,Mathf.Clamp(relativePosition.y - 1,0,2));
		}

		if(currentOrientation != Screen.orientation){
			currentOrientation = Screen.orientation;
			touchZone = new Rect(0,Screen.height/4f,Screen.width,Screen.height/4f);
			SpeechRecognition.SetTouchToListenZone(touchZone,true);
		}
	}

	bool MicIsOn = false;
	public bool showMic = false;

	void OnGUI(){
		bool enabledBackup = GUI.enabled;

		if(showMic)
		{
			GameObject go = GameObject.Find ("Buttons");
			ButtonManager bm = go.GetComponent<ButtonManager>();

			//at top
			GUI.Label(new Rect(0,0,Screen.width,Screen.height/8f),lastResults,fontStyle); //comment out when no longer needed

			//makes the button appear like a toggle, although it doesn't act like one
			if(!MicIsOn)
			{
				if(GUI.Button(new Rect(Screen.width/3f, Screen.height/3f, Screen.width/3f, Screen.height/3f), Mic, new GUIStyle())){//0,Screen.height*3/4f,Screen.width/6f,Screen.height/4f
					MicIsOn = true;
					SpeechRecognition.StartListening();
				}
			}
			if(MicIsOn)
			{
				if(GUI.Button(new Rect(Screen.width/3f, Screen.height/3f, Screen.width/3f, Screen.height/3f), MicActive, new GUIStyle())){//0,Screen.height*3/4f,Screen.width/6f,Screen.height/4f
					MicIsOn = false;
					SpeechRecognition.StopListening();
					bm.CheckAnswers(bm.ButtonSelected);
					showMic = false;
				}
			}
		}

		GUI.enabled = enabledBackup;
	}
}
