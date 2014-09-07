using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour {

	public BoxCollider2D openRange;
	public BoxCollider2D portal;

	public SpriteMover character;

	public Animator anim;

	bool locked;
	bool open;

	// Use this for initialization
	void Start () {
		locked = true;
		open = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetLock(bool l) {
		locked = l;
		if(l) {
			open = false;
			anim.SetBool("Open", false);
		}
	}

	public void Open() {
		open = true;
		anim.SetBool("Open", true);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player")
			if(!locked) {
				anim.SetBool("Open", true);
				open = true;
			}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player")
			if(!locked) {
				anim.SetBool("Open", false);
				open = false;
			}
	}
}
