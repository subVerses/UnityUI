using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public SpriteMover spriteMov;
	float xPos;
	float yPos;

	// Use this for initialization
	void Start () {
		xPos = spriteMov.GetComponent<Transform> ().localPosition.x;
		yPos = spriteMov.GetComponent<Transform> ().localPosition.y;
		transform.localPosition = new Vector3 (xPos, yPos, transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
		xPos = spriteMov.GetComponent<Transform> ().localPosition.x;
		yPos = spriteMov.GetComponent<Transform> ().localPosition.y;
		transform.localPosition = new Vector3 (xPos, yPos, transform.localPosition.z);
	}
}
