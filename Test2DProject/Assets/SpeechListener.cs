using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechListener : MonoBehaviour, ISpeechRecognitionListener {

	private GUIStyle fontStyle = new GUIStyle();
	
	private string lastResults = "";
	
	private static Vector2 relativePosition = new Vector2(1,1); //used for the SpeechDictionary example
	
	private Rect touchZone = new Rect(0,Screen.height/4f,Screen.width,Screen.height/4f);
	
	private ScreenOrientation currentOrientation = ScreenOrientation.Landscape;

	private Texture Mic;

	private Texture MicActive;


	
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
	// Use this for initialization
	void Start () {
		SpeechRecognition.AddSpeechRecognitionListeren(this);

		SpeechRecognition.SetTouchToListenZone(touchZone,true);

		Mic = (Texture) Resources.Load ("Microphone");
		MicActive = (Texture) Resources.Load ("MicrophoneActive");
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

	void OnGUI(){
		bool enabledBackup = GUI.enabled;
		
		GUI.Label(new Rect(0,0,Screen.width,Screen.height/8f),"RESULTS OF SPEECH: " + lastResults,fontStyle);
		
		if(SpeechRecognition.GetTouchToListenEnabled()){
			GUI.DrawTexture(touchZone,(Texture2D)Resources.Load("SpeechRecognitionExample/grey"));
			GUI.Label(touchZone, "Press here to listen to speech",fontStyle);
		}else{
			if(!MicIsOn)
			{
				if(GUI.Button(new Rect(0,Screen.height*4/6f,Screen.width/6f,Screen.height/4f), Mic)){
					MicIsOn = true;
					SpeechRecognition.StartListening();
				}
			}
			if(MicIsOn)
			{
				if(GUI.Button(new Rect(0,Screen.height*4/6f,Screen.width/6f,Screen.height/4f), MicActive)){
					MicIsOn = false;
					SpeechRecognition.StopListening();
				}
			}
		}
		
		SpeechRecognition.SetTouchToListenEnabled(GUI.Toggle(new Rect(0,Screen.height*2/6f,Screen.width/4f,Screen.height/4f), SpeechRecognition.GetTouchToListenEnabled(), "Touch to listen"));
		SpeechRecognition.instance.autoRestart = GUI.Toggle(new Rect(0,Screen.height*3/6f,Screen.width/4f,Screen.height/4f), SpeechRecognition.instance.autoRestart, "Auto restart on sound");
		
		//GUI.DrawTexture(new Rect((Screen.width-Screen.height/2f)/2f,Screen.height/2f,Screen.height/2f,Screen.height/2f),(Texture2D)Resources.Load("SpeechRecognitionExample/3x3_grid"));
		
		//GUI.DrawTexture(new Rect((Screen.width-Screen.height/2f)/2f+Screen.height*relativePosition.x/6f,Screen.height/2f+Screen.height*relativePosition.y/6f,Screen.height/6f,Screen.height/6f),(Texture2D)Resources.Load("SpeechRecognitionExample/x"));
		
		GUI.enabled = enabledBackup;
	}
}
