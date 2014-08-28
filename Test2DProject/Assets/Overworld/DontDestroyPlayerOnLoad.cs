using UnityEngine;
using System.Collections;

public class DontDestroyPlayerOnLoad : MonoBehaviour {
	public SpriteMover spriteMover;
	public CollidableObjects collidableObjects;
	public GameObject interactables;
	public GameObject uninteractables;

	void Start() {
		spriteMover = this.GetComponentInChildren<SpriteMover>();
		
		collidableObjects = this.GetComponentInChildren<CollidableObjects>();
	}

	void Update(){

	}

	void Awake() {
		DontDestroyOnLoad(this);
		DontDestroyOnLoad(spriteMover);
	}
}