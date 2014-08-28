using UnityEngine;
using System.Collections;

public class CharacterLocationSaver : MonoBehaviour {
	public SpriteMover charMover;

	public Vector3 charPos;
	public Quaternion charDir;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("SpriteController");
		charMover = go.GetComponent<SpriteMover>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject go = GameObject.Find("SpriteController");
		charMover = go.GetComponent<SpriteMover>();
		charPos = charMover.transform.position;
		charDir = charMover.transform.rotation;
	}

	void Awake() {
		DontDestroyOnLoad(this);
	}

	void OnLevelWasLoaded()
	{
		Debug.Log ("...");
		charMover.transform.position = charPos;
		charMover.transform.rotation = charDir;
	}
}
