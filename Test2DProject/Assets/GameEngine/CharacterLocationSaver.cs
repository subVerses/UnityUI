using UnityEngine;
using System.Collections;

public class CharacterLocationSaver : MonoBehaviour {
	//These are the game objects we will be attempting to find and save. This class needs serious work.
	public GameObject sprController;
	public SpriteMover sprMover;

	public GameObject charController;
	public CharacterInteraction charCont;

	public Vector3 charPos;
	public Quaternion charDir;

	// The next 2 find operations are likely unuseful when attaching this to the charactercontroller
	// However I want to try using it outside of that hierarchy, because that hierarchy is where all of the problems are
	void Start () {
		sprController = GameObject.Find("SpriteController");
		sprMover = sprController.GetComponent<SpriteMover>();
		
		charController = GameObject.Find("CharacterController");
		charCont = charController.GetComponent<CharacterInteraction>();

	}

	//I really hope some of these finds can be deprecated - the problem is that when the spritecontroller/charactercontroller are destroyed (and I can't save them)
	//This object loses its references, so it has to find them again, waiting to save the location of the player until they press A
	void Update () {
		//sprMover = sprController.GetComponent<SpriteMover>(); //saves the current sprites position by checking the spriteMover object in the scene
		//charCont = charController.GetComponent<CharacterInteraction>(); //needed to check if the button pressed is A

		if(charCont.NextInput==4) //4 == A
		{
			charPos = sprController.transform.position;
			charDir = sprController.transform.rotation;
		}
	}

	//This should save the location to transform the player position to
	void Awake() {
		DontDestroyOnLoad(this);
	}

	//this is supposed to transport the player as well as the spritemover to that position.
	void OnLevelWasLoaded()
	{
		if(Application.loadedLevelName == "Overworld") //makes sure the scene loaded is the overworld, should move the character on load
		{
			sprController.transform.position = charPos;
			sprController.transform.rotation = charDir;
		}
	}

}
