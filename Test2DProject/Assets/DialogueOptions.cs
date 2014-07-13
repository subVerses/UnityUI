using UnityEngine;
using System.Collections;

public class DialogueOptions{
	public string choice1;
	public string choice2;
	public string choice3;
	public string choice4;

	public string Choice1 {
		get {
			return choice1;
		}
	}

	public string Choice2 {
		get {
			return choice2;
		}
	}

	public string Choice3 {
		get {
			return choice3;
		}
	}

	public string Choice4 {
		get {
			return choice4;
		}
	}


	// Empty string initializer
	void Start () {
		choice1 = "";
		choice2 = "";
		choice3 = "";
		choice4 = "";
	}
	
	// Update is called once per frame, not useful here
	void Update () {
	
	}


}
