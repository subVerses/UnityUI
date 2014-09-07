using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	public CharacterInteraction charInter;
	public GameObject scene;
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
			scene.SetActive (true);
			charInter.resetAnimation();
			done = true;
		}
	}
}
