using UnityEngine;
using System.Collections;

public class Item {

	public string name;
	public string description;
	public int value;
	public bool isSellable;
	public bool isUsable;

	public Item(){
		name = "";
		description = "";
		value = 0;
		isSellable = true;
		isUsable = true;
	}

	public Item(string n, string d, int v, bool s, bool u){
		name = n;
		description = d;
		value = v;
		isSellable = s;
		isUsable = u;
	}

	//getters
	public string Name {
		get {
			return name;
		}
	}

	public string Description {
		get {
			return description;
		}
	}

	public int Value {
		get {
			return value;
		}
	}

	public bool IsSellable {
		get {
			return isSellable;
		}
	}

	public bool IsUsable {
		get {
			return isUsable;
		}
	}
}
