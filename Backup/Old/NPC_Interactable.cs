using UnityEngine;
using System.Collections;

public class NPC_Interactable : MonoBehaviour {
	BoxCollider2D npcCollider;
	BoxCollider2D npcVision;
	Bounds nextBounds;
	Bounds lastBounds;
	CollidableObjects collidables;
	
	int nextDirection;
	int currentDirection;
	int lastDirection; //direction NPC is facing
	int charDirection; //last direction to face before stopping next to player
	int[] directionsToMove; //array of order of directions to move
	bool isMoving;
	bool noticedChar;
	float xScale;
	float yScale;
	float totalDistance;
	public float speed;//seconds per square travelled 

	// Use this for initialization
	void Start () {
		npcCollider = transform.GetComponents<BoxCollider2D>()[0];
		npcVision = transform.GetComponents<BoxCollider2D>()[1];
		collidables = GameObject.Find ("ObjectSprites").GetComponent<CollidableObjects>();
		nextBounds = npcCollider.bounds;
		lastBounds = npcCollider.bounds;
		xScale = npcCollider.bounds.extents.x * 2;
		yScale = npcCollider.bounds.extents.x * 2;
		isMoving = false;
		noticedChar = false;
		nextDirection = -1;
		currentDirection = -1;
		lastDirection = 3;
		charDirection = -1;
		directionsToMove = new int[] {-1};
		speed = .2f;
		totalDistance = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		float timeChange = Time.deltaTime;
		//if(noticedChar)
			moveInDirection (timeChange);
	}

	void moveInDirection(float deltaTime) {
		switch (directionsToMove[0]) {
			case -1:
				if(charDirection != -1) {
					lastDirection = charDirection;
					charDirection = -1;
				}
				break;
			case 0: //direction is left
				if(xScale * (deltaTime / speed) >= xScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x - (xScale - totalDistance), transform.localPosition.y, transform.localPosition.z);
					totalDistance = 0f;
					for(int i = 0; i < directionsToMove.Length - 1; i++)
						directionsToMove[i] = directionsToMove[i + 1];
					directionsToMove[directionsToMove.Length-1] = -1;
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x - xScale * (deltaTime / speed), transform.localPosition.y, transform.localPosition.z);
					totalDistance += xScale * (deltaTime / speed);
				}
				break;
			case 1: //direction is up
				if(yScale * (deltaTime / speed) >= yScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y + (yScale - totalDistance), transform.localPosition.z);
					totalDistance = 0f;
					for(int i = 0; i < directionsToMove.Length - 1; i++)
						directionsToMove[i] = directionsToMove[i + 1];
					directionsToMove[directionsToMove.Length-1] = -1;
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y + yScale * (deltaTime / speed), transform.localPosition.z);
					totalDistance += yScale * (deltaTime / speed);
				}
				break;
			case 2: //direction is right
				if(xScale * (deltaTime / speed) >= xScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x + (xScale - totalDistance), transform.localPosition.y, transform.localPosition.z);
					totalDistance = 0f;
					for(int i = 0; i < directionsToMove.Length - 1; i++)
						directionsToMove[i] = directionsToMove[i + 1];
					directionsToMove[directionsToMove.Length-1] = -1;
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x + xScale * (deltaTime / speed), transform.localPosition.y, transform.localPosition.z);
					totalDistance += xScale * (deltaTime / speed);
				}
				break;
			case 3: //direction is down
				if(yScale * (deltaTime / speed) >= yScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y - (yScale - totalDistance), transform.localPosition.z);
					totalDistance = 0f;
					for(int i = 0; i < directionsToMove.Length - 1; i++)
						directionsToMove[i] = directionsToMove[i + 1];
					directionsToMove[directionsToMove.Length-1] = -1;
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y - yScale * (deltaTime / speed), transform.localPosition.z);
					totalDistance += yScale * (deltaTime / speed);
				}
				break;
		}
	}
	
	public bool setCurrentDirection(int dir) { //0 = left; 1 = up; 2 = right; 3 = down
		if(currentDirection != dir && currentDirection == -1) {
			currentDirection = dir;
			setNextBounds(currentDirection);
			return true;
		}
		return false;
	}

	public bool npcMoving {
		get {
			return isMoving;
		}
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

	public bool inSight(SpriteMover charSprite) {
		if(npcVision.OverlapPoint(new Vector2(charSprite.getBounds().center.x, charSprite.getBounds().center.y))) {
			Vector2 testPoint;
				switch(lastDirection) {
					case 0: //left
						testPoint = new Bounds(new Vector3(npcCollider.bounds.center.x - xScale, npcCollider.bounds.center.y), npcCollider.bounds.size);
						break;
					case 1: //up
						testPoint = new Bounds(new Vector3(npcCollider.bounds.center.x, npcCollider.bounds.center.y + yScale), npcCollider.bounds.size);
						break;
					case 2: //right
						testPoint = new Bounds(new Vector3(npcCollider.bounds.center.x + xScale, npcCollider.bounds.center.y), npcCollider.bounds.size);
						break;
					case 3: //down
						testPoint = new Vector2(npcCollider.bounds.center.x, npcCollider.bounds.center.y - yScale);
						break;
				}
		}
		return false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log (coll.gameObject.tag);
		if (coll.gameObject.tag == "CharSprite") {
			Vector2 testPoint;
			switch(lastDirection) {
				case 0: //left
					testPoint = new Bounds(new Vector3(npcCollider.bounds.center.x - xScale, npcCollider.bounds.center.y), npcCollider.bounds.size);
					break;
				case 1: //up
					testPoint = new Bounds(new Vector3(npcCollider.bounds.center.x, npcCollider.bounds.center.y + yScale), npcCollider.bounds.size);
					break;
				case 2: //right
					testPoint = new Bounds(new Vector3(npcCollider.bounds.center.x + xScale, npcCollider.bounds.center.y), npcCollider.bounds.size);
					break;
				case 3: //down
					testPoint = new Vector2(npcCollider.bounds.center.x, npcCollider.bounds.center.y);
					break;
			}
		}
	}

	public void setMovement(int[] movement) {
		directionsToMove = movement;
	}
}
