using UnityEngine;
using System.Collections;

public class DontDestroyPlayerOnLoad : MonoBehaviour {
	public SpriteMover spriteMover;

	void Start() {
		spriteMover = this.GetComponentInChildren<SpriteMover>();

	}

	void Update(){

	}

	void Awake() {
		DontDestroyOnLoad(this);
		DontDestroyOnLoad(spriteMover);
	}
}