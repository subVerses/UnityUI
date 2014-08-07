using UnityEngine;
using System.Collections;

public class SpriteMover : MonoBehaviour {

	BoxCollider2D charCollider;
	Bounds nextBounds;
	Bounds lastBounds;

	int nextDirection;
	int currentDirection;
	float xScale;
	float yScale;
	float totalDistance;
	public float speed; //seconds per square travelled 

	// Use this for initialization
	void Start () {
		charCollider = transform.Find("CharSprite").GetComponent<BoxCollider2D>();
		nextBounds = charCollider.bounds;
		lastBounds = charCollider.bounds;
		xScale = charCollider.bounds.extents.x * 2;
		yScale = charCollider.bounds.extents.x * 2;
		nextDirection = -1;
		currentDirection = -1;
		speed = .4f;
		totalDistance = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		float timeChange = Time.deltaTime;
		moveInDirection (timeChange);
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
	
	void moveInDirection(float deltaTime) {
		switch (currentDirection) {
			case -1:
				break;
			case 0: //direction is left
				if(xScale * (deltaTime / speed) >= xScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x - (xScale - totalDistance), transform.localPosition.y, transform.localPosition.z);
					totalDistance = 0f;
					currentDirection = -1;
					setNextBounds(-1);
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x - xScale * (deltaTime / speed), transform.localPosition.y, transform.localPosition.z);
					totalDistance += xScale * (deltaTime / speed);
				}
				break;
			case 1: //direction is up
				if(yScale * (deltaTime / speed) >= yScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y + (yScale - totalDistance), transform.localPosition.z);
					totalDistance = 0f;
					currentDirection = -1;
					setNextBounds(-1);
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y + yScale * (deltaTime / speed), transform.localPosition.z);
					totalDistance += yScale * (deltaTime / speed);
				}
				break;
			case 2: //direction is right
				if(xScale * (deltaTime / speed) >= xScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x + (xScale - totalDistance), transform.localPosition.y, transform.localPosition.z);
					totalDistance = 0f;
					currentDirection = -1;
					setNextBounds(-1);
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x + xScale * (deltaTime / speed), transform.localPosition.y, transform.localPosition.z);
					totalDistance += xScale * (deltaTime / speed);
				}
				break;
			case 3: //direction is down
				if(yScale * (deltaTime / speed) >= yScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y - (yScale - totalDistance), transform.localPosition.z);
					totalDistance = 0f;
					currentDirection = -1;
					setNextBounds(-1);
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y - yScale * (deltaTime / speed), transform.localPosition.z);
					totalDistance += yScale * (deltaTime / speed);
				}
				break;
		}
	}

	public Bounds getBounds() {
		return charCollider.bounds;
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
				nextBounds = charCollider.bounds;
				break;
			case 0: //left
				nextBounds = new Bounds(new Vector3(charCollider.bounds.center.x - xScale, charCollider.bounds.center.y), charCollider.bounds.size);
				break;
			case 1: //up
				nextBounds = new Bounds(new Vector3(charCollider.bounds.center.x, charCollider.bounds.center.y + yScale), charCollider.bounds.size);
				break;
			case 2: //right
				nextBounds = new Bounds(new Vector3(charCollider.bounds.center.x + xScale, charCollider.bounds.center.y), charCollider.bounds.size);
				break;
			case 3: //down
				nextBounds = new Bounds(new Vector3(charCollider.bounds.center.x, charCollider.bounds.center.y - yScale), charCollider.bounds.size);
				break;
		}
		lastBounds = charCollider.bounds;
	}
}
