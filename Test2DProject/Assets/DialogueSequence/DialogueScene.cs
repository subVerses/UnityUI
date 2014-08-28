using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueScene {

	private List<DialogueChoice> dialogue;

	public DialogueScene(){
		dialogue = new List<DialogueChoice> ();
	}
	public DialogueScene(List<DialogueChoice> ar){
		dialogue = ar;
	}
	public void AddChoice(string text, int id){
		DialogueChoice ch = new DialogueChoice (text, id);
		this.dialogue.Add (ch);
	}

	public List<DialogueChoice> Dialogue {
		get {
			return dialogue;
		}
	}
}
