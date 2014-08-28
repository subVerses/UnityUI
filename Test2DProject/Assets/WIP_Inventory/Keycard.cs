using UnityEngine;
using System.Collections;

public class Keycard : Item {
	Color col;

	public Keycard(string n, string desc, Color c){
		this.name = n;
		this.description = desc;
		this.isUsable = true;
		col = c;
	}

	public void useKeycard(){
		return;
	}
}
