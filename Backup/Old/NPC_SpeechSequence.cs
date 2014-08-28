using UnityEngine;
using System.Collections;

public class NPC_SpeechSequence : MonoBehaviour {

	/*This class is supposed to be attached to an interactable object
	 * I'm not sure how to ensure that it will work upon the user pressing 'A'
	 * */

	
	bool activated; //is the speech sequence activated by the user pressing a on the NPC?

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void beginSpeechSequence(){
		Application.LoadLevel ("NPCInteraction");
	}

	/*public GameObject[] findAndSaveScene(){
		object[] allObjects = FindObjectsOfTypeAll(typeof(GameObject));
		ArrayList allGameObjects = new ArrayList();
		foreach(object thisObject in allObjects)
			if (((GameObject) thisObject).activeInHierarchy)
				allGameObjects.Add(thisObject);
		return allGameObjects.ToArray();
	}*/
}
