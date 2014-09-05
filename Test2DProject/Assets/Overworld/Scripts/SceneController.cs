using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public GameObject skene;
	public bool done;

	void Awake () {
		DontDestroyOnLoad (this);
		done = false;
	}

	void OnLevelWasLoaded(int level) {
		if(level == 1)
			done = false;
	}

	void Update() {
		if(!done && Application.loadedLevelName == "Overworld_Reload") {
			skene.SetActive (true);
			skene.GetComponent<CharacterInteraction>().resetAnimation();
			done = true;
		}
	}
}
