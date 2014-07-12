using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResponseListener : MonoBehaviour, ISpeechRecognitionListener {

	private string lastResults = "";

	public void OnBeginningOfSpeech() {}
	public void OnBufferReceived(byte[] buffer) {}
	public void OnEndOfSpeech() {}
	public void OnError(int error, string errorMessage) {}
	public void OnEvent(int eventType, Dictionary<string,string> bundle) {}
	public void OnPartialResults(Dictionary<string,string> partialResults) {}
	public void OnReadyForSpeech(Dictionary<string,string> bundle) {}

	public void OnResults(string[] results) {
		lastResults = results [0];
	}
	public void OnRmsChanged(float rmsdB){}

	public void OnChangeState(SpeechRecognition.State newState) {
		if (newState == SpeechRecognition.State.IDLE)
			Camera.main.backgroundColor = Color.red;
		else
			Camera.main.backgroundColor = Color.green;
	}

	public string getResults() {
		return lastResults;
	}
}
