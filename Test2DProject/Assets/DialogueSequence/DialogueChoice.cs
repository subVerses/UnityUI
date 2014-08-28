using UnityEngine;
using System.Collections;

public class DialogueChoice {
	private string text;
	private int id;

	public DialogueChoice(){
		text = "";
		id = 0;
	}
	public DialogueChoice(string t, int i){
		text = t;
		id = i;
	}

	public string Text {
		get {
			return text;
		}
	}

	public int Id {
		get {
			return id;
		}
	}
}
