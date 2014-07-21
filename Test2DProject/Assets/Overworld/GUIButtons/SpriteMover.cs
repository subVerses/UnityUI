using UnityEngine;
using System.Collections;

public class SpriteMover : MonoBehaviour {

	int nextDirection;
	int currentDirection;
	float xScale;
	float yScale;
	float totalDistance;
	float speed; //seconds per square travelled 

	// Use this for initialization
	void Start () {
		xScale = transform.localScale.x;
		yScale = transform.localScale.y;
		nextDirection = -1;
		currentDirection = -1;
		speed = .3f;
		totalDistance = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		float timeChange = Time.deltaTime;
		switch (currentDirection) {
			case -1:
				if(nextDirection != -1) {
					currentDirection = nextDirection;
					nextDirection = -1;
				}
				break;
			case 0: //direction is left
				if(xScale * (timeChange / speed) >= xScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x - (xScale - totalDistance), transform.localPosition.y, transform.localPosition.z);
					totalDistance = 0f;
					currentDirection = -1;
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x - xScale * (timeChange / speed), transform.localPosition.y, transform.localPosition.z);
					totalDistance += xScale * (timeChange / speed);
				}
				break;
			case 1: //direction is up
				if(yScale * (timeChange / speed) >= yScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y + (yScale - totalDistance), transform.localPosition.z);
					totalDistance = 0f;
					currentDirection = -1;
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y + yScale * (timeChange / speed), transform.localPosition.z);
					totalDistance += yScale * (timeChange / speed);
				}
				break;
			case 2: //direction is right
				if(xScale * (timeChange / speed) >= xScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x + (xScale - totalDistance), transform.localPosition.y, transform.localPosition.z);
					totalDistance = 0f;
					currentDirection = -1;
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x + xScale * (timeChange / speed), transform.localPosition.y, transform.localPosition.z);
					totalDistance += xScale * (timeChange / speed);
				}
				break;
			case 3: //direction is down
				if(yScale * (timeChange / speed) >= yScale - totalDistance) {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y - (yScale - totalDistance), transform.localPosition.z);
					totalDistance = 0f;
					currentDirection = -1;
				} else {
					transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y - yScale * (timeChange / speed), transform.localPosition.z);
					totalDistance += yScale * (timeChange / speed);
				}
				break;
		}
	}

	public void moveSprite(int dir) { //-1 = stop; 0 = left; 1 = up; 2 = right; 3 = down
		if(dir <= 3 && dir >= 0) {
			if(currentDirection != -1) {
				if(dir != currentDirection)
					nextDirection = dir;
			} else
				currentDirection = dir;
		}
	}
}
