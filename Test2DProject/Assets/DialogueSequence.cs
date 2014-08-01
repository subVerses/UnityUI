using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueSequence{
	/*public struct Option
	{
		public string text;
		public int next;
	}

	public ArrayList Head;

	public DialogueOptions(ArrayList head){
		this.Head = head;
	}*/

	public List<DialogueScene> dialogueTree;
	private DialogueScene currentScene;

	public DialogueSequence(){
		dialogueTree = new List<DialogueScene> ();
	}

	public void AddScene(){
		dialogueTree.Add (new DialogueScene());
	}

	public void AddSceneOptions(int i, string text, int id){ //index, choice string, id of choice
		DialogueScene d = dialogueTree[i];
		d.AddChoice (text, id);
	}

	public DialogueScene SetCurrentScene(int i){
		currentScene = dialogueTree[i];
		return currentScene;
	}

	public DialogueScene CurrentScene {
		get {
			return currentScene;
		}
	}
}
