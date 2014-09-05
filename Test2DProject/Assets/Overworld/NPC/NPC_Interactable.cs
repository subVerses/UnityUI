using UnityEngine;
using System.Collections;

public class NPC_Interactable : MonoBehaviour {
	BoxCollider2D npcCollider;
	Bounds nextBounds;
	Bounds lastBounds;
	
	int nextDirection;
	int currentDirection;
	float xScale;
	float yScale;
	float totalDistance;
	public float speed;//seconds per square travelled 

	void Awake () {
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		npcCollider = transform.GetComponent<BoxCollider2D>();
		nextBounds = npcCollider.bounds;
		lastBounds = npcCollider.bounds;
		xScale = npcCollider.bounds.extents.x * 2;
		yScale = npcCollider.bounds.extents.x * 2;
		nextDirection = -1;
		currentDirection = -1;
		speed = .4f;
		totalDistance = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		float timeChange = Time.deltaTime;
		//moveInDirection (timeChange);
	}

	public bool setCurrentDirection(int dir) { //0 = left; 1 = up; 2 = right; 3 = down
		if(currentDirection != dir && currentDirection == -1) {
			currentDirection = dir;
			setNextBounds(currentDirection);
			return true;
		}
		return false;
	}

	public int getCurrentDirection() {
		return currentDirection;
	}
	
	public void setNextDirection(int dir) {
		if(currentDirection != dir)
			nextDirection = dir;
	}
	
	public int getNextDirection() {
		return nextDirection;
	}
	public Bounds getBounds() {
		return npcCollider.bounds;
	}
	
	public Bounds getLastBounds() {
		return lastBounds;
	}
	
	public Bounds getNextBounds() {
		return nextBounds;
	}
	
	public void setNextBounds(int currDir) {
		switch(currDir) {
		case -1:
			nextBounds = npcCollider.bounds;
			break;
		case 0: //left
			nextBounds = new Bounds(new Vector3(npcCollider.bounds.center.x - xScale, npcCollider.bounds.center.y), npcCollider.bounds.size);
			break;
		case 1: //up
			nextBounds = new Bounds(new Vector3(npcCollider.bounds.center.x, npcCollider.bounds.center.y + yScale), npcCollider.bounds.size);
			break;
		case 2: //right
			nextBounds = new Bounds(new Vector3(npcCollider.bounds.center.x + xScale, npcCollider.bounds.center.y), npcCollider.bounds.size);
			break;
		case 3: //down
			nextBounds = new Bounds(new Vector3(npcCollider.bounds.center.x, npcCollider.bounds.center.y - yScale), npcCollider.bounds.size);
			break;
		}
		lastBounds = npcCollider.bounds;
	}
}
