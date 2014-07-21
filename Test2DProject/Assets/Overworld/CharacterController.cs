using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public InputManager inputMan;
	public SpriteMover spriteMov;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spriteMov.moveSprite (3);
	}
}
