using UnityEngine;
using System.Collections;

public class InventoryPopulate : MonoBehaviour {

	public GameObject RootGrid;
	public GameObject ItemPref;
	
	void Start () {

		for(int i = 0; i < 25; i++)
		{
			GameObject item = NGUITools.AddChild(RootGrid,ItemPref);
			//item.GetComponent<UILabel>().text = "FOAD";
		}

		RootGrid.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition ();
		RootGrid.GetComponent<UIGrid>().Reposition ();

	}

}
