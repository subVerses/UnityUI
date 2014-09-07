using UnityEngine;
using System.Collections;

public class NPC_Interactable : MonoBehaviour {
	BoxCollider2D npcCollider;
	BoxCollider2D npcVision;
	Bounds nextBounds;
	Bounds lastBounds;
	CollidableObjects collidables;
	public GameObject exclamation;
	public Animator anim;
	public DoorControl doorControl;
	
	int nextDirection;
	int currentDirection;
	int lastDirection; //direction NPC is facing
	int charDirection; //last direction to face before stopping next to player
	int frameTimer;
	int[] directionsToMove; //array of order of directions to move
	bool isMoving;
	bool noticedChar;
	float xScale;
	float yScale;
	float totalDistance;
	public float speed;//seconds per square travelled 

	void Awake () {
		DontDestroyOnLoad (this);
	}

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
		frameTimer = 0;
		lastDirection = 3;
		charDirection = -1;
		directionsToMove = new int[] {-1};
		speed = .4f;
		totalDistance = 0f;
		anim.SetBool("isMoving", false);
		anim.SetInteger("lastDirection", lastDirection);
	}
	
	// Update is called once per frame
	void Update () {
		if(frameTimer != 0) {
			if(!noticedChar && frameTimer == 20) {
				doorControl.SetLock(false);
				doorControl.Open();
			}
			frameTimer--;
		} else if(noticedChar) {
			float timeChange = Time.deltaTime;
			moveInDirection (timeChange);
			if(!isMoving && charDirection == -1) {
				exclamation.SetActive(false);
				if(directionsToMove.Length == 2)
					setMovement(new int[1] {2});
				else if(directionsToMove.Length == 3)
					setMovement(new int[2] {2, 2});
				charDirection = 3;
				noticedChar = false;
				frameTimer = 200;
				SendMessageUpwards("setNPCChat", true);
			} else {
				exclamation.SetActive(false);
				anim.SetBool("isMoving", true);
			}
		} else {
			resetAnimation();
			float timeChange = Time.deltaTime;
			moveInDirection (timeChange);
		}
	}

	void moveInDirection(float deltaTime) {
		switch (directionsToMove[0]) {
			case -1:
				if(charDirection != -1) {
					lastDirection = charDirection;
					anim.SetInteger("lastDirection", lastDirection);
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
					anim.SetBool("isMoving", false);
					isMoving = false;
				} else {
					anim.SetBool("isMoving", true);
					isMoving = true;
					lastDirection = 0;
					anim.SetInteger("lastDirection", lastDirection);
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
					anim.SetBool("isMoving", false);
					isMoving = false;
				} else {
					anim.SetBool("isMoving", true);
					isMoving = true;
					lastDirection = 1;
					anim.SetInteger("lastDirection", lastDirection);
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
					anim.SetBool("isMoving", false);
					isMoving = false;
				} else {
					anim.SetBool("isMoving", true);
					isMoving = true;
					lastDirection = 2;
					anim.SetInteger("lastDirection", lastDirection);
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
					anim.SetBool("isMoving", false);
					isMoving = false;
				} else {
					anim.SetBool("isMoving", true);
					isMoving = true;
					lastDirection = 3;
					anim.SetInteger("lastDirection", lastDirection);
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

	public int getCurrentDirection() {
		return currentDirection;
	}
	
	public bool npcMoving {
		get {
			return isMoving;
		}
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

	public void setMovement(int[] movement) {
		directionsToMove = movement;
	}

	public void resetAnimation() {
		anim.SetBool("isMoving", isMoving);
		anim.SetInteger("lastDirection", lastDirection);
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
	
	void OnTriggerEnter2D(Collider2D col) {
		setCurrentDirection(0);
		if (col.gameObject.tag == "Player") {
			Destroy(npcVision);
			anim.SetInteger("lastDirection", 0);
			charDirection = 0;
			col.SendMessage("setConversation", true);
			SendMessageUpwards("SetLastDirection", 2);
			int steps = (int)(npcCollider.bounds.center.x - col.bounds.center.x) / 398;
			if(steps == 1)
				setMovement(new int[1] {-1});
			if(steps == 2)
				setMovement(new int[2] {0, -1});
			else if(steps == 3)
				setMovement(new int[3] {0, 0, -1});
			exclamation.SetActive(true);
			frameTimer = 300;
			noticedChar = true;
		}
	}
}