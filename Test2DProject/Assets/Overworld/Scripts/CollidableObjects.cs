using UnityEngine;
using System.Collections;
using System.IO;

public class CollidableObjects : MonoBehaviour {

	Collider2D[] uninteractibleObjects;
	Collider2D[] interactibleObjects;
	//int currentCharIndex; //smallest index of objectColliders at which the player is either at or in front of

	void Start() {
		
		uninteractibleObjects = transform.FindChild("Uninteractibles").GetComponentsInChildren<Collider2D>();
		interactibleObjects = transform.FindChild("Interactibles").GetComponentsInChildren<Collider2D> ();


//		int[] layerIndex = new int[objectColliders.Length];
//		currentCharIndex = 0;
//
//		foreach(Collider2D collider in objectColliders) {
//			collider.GetComponent<SpriteRenderer>().sortingLayerID = 2;
//			collider.GetComponent<SpriteRenderer>().sortingLayerName = "BehindChar";
//			collider.GetComponent<SpriteRenderer>().sortingOrder = 0;
//		}
//
//		float lowestY = objectColliders[0].GetComponent<SpriteRenderer>().bounds.min.y;
//		for(int i = 1; i < objectColliders.Length; i++) {
//			for(int j = i; j >= 0; j--) {
//				SpriteRenderer currentSR = objectColliders[j].GetComponent<SpriteRenderer>();
//				if(currentSR.bounds.min.y > lowestY)
//					layerIndex[j]++;
//				else
//					lowestY = currentSR.bounds.min.y;
//			}
//		}
//
//		int lowestIndex = 0;
//		for(int i = 0; i < objectColliders.Length;) {
//			for(int j = 0; j < objectColliders.Length; j++) {
//				if(layerIndex[j] == lowestIndex) {
//					if(i != j) {
//						Collider2D tempC2D = objectColliders[i];
//						objectColliders[i] = objectColliders[j];
//						objectColliders[j] = tempC2D;
//
//						int tempLI = layerIndex[i];
//						layerIndex[i] = layerIndex[j];
//						layerIndex[j] = tempLI;
//					}
//
//					i++;
//				}
//			}
//
//			lowestIndex++;
//		}
//		for(int i = 0; i < layerIndex.Length; i++)
//			Debug.Log (layerIndex [i]);
//
//		for(int i = 0; i < objectColliders.Length; i++)
//			objectColliders[i].GetComponent<SpriteRenderer>().sortingOrder = layerIndex[i];
	}

	public bool testBounds(Vector2 point) {
		foreach(Collider2D collider in uninteractibleObjects)
			if(collider.OverlapPoint(point))
				return true;
		foreach(Collider2D collider in interactibleObjects)
			if(collider.OverlapPoint(point))
				return true;
		return false;
	}

//	public void updateCollisionBoxes(float charMinY) {
//		BoxCollider2D[] escalatorColliders = transform.FindChild("RightEscalator_Sprite").GetComponents<BoxCollider2D>();
//		SpriteRenderer spriteRenderer = transform.FindChild ("RightEscalator_Sprite").GetComponent<SpriteRenderer> ();
//		if(charMinY < escalatorColliders[0].bounds.min.y) {
//			spriteRenderer.sortingLayerID = 2;
//			spriteRenderer.sortingLayerName = "BehindChar";
//			escalatorColliders[0].enabled = false;
//		} else {
//			spriteRenderer.sortingLayerID = 4;
//			spriteRenderer.sortingLayerName = "AheadChar";
//			escalatorColliders[0].enabled = true;
//		}
//	}

//	public void updateLayers(float charMinY) {
//		if(objectColliders[currentCharIndex].GetComponent<SpriteRenderer>().bounds.min.y < charMinY) {
//
//		} else if(objectColliders[currentCharIndex - 1].GetComponent<SpriteRenderer>().bounds.min.y >= charMinY) {
//
//		}
//	}
	
//	public void sortLayers(float charMinY) {
//		for(int i = 0; i < objectColliders.Length; i++) {
//			SpriteRenderer spriteRendererI = objectColliders[i].GetComponent<SpriteRenderer>();
//			if(spriteRendererI.bounds.min.y >= charMinY) {
//				currentCharIndex = i;
//				for(int j = 0; j < objectColliders.Length; j++) {
//					SpriteRenderer spriteRendererJ = objectColliders[j].GetComponent<SpriteRenderer>();
//					if(j < i) {
//						spriteRendererJ.sortingLayerID = 4;
//						spriteRendererJ.sortingLayerName = "AheadChar";
//						spriteRendererJ.sortingOrder = spriteRendererI.sortingOrder - spriteRendererJ.sortingOrder - 1;
//					} else {
//						collider.GetComponent<SpriteRenderer>().sortingLayerID = 2;
//						collider.GetComponent<SpriteRenderer>().sortingLayerName = "BehindChar";
//						spriteRendererJ.sortingOrder -= spriteRendererI.sortingOrder;
//					}
//				}
//				break;
//			} else if(i == objectColliders.Length - 1) {
//				currentCharIndex = i;
//				int currSortOrder = 0;
//				for(int j = objectColliders.Length - 1; j >= 0; j--) {
//					SpriteRenderer spriteRendererJ = objectColliders[j].GetComponent<SpriteRenderer>();
//					if(j != 0 && objectColliders[j-1].GetComponent<SpriteRenderer>().sortingOrder != spriteRendererJ.sortingOrder) {
//						spriteRendererJ.sortingOrder = currSortOrder;
//						currSortOrder++;
//					} else
//						spriteRendererJ.sortingOrder = currSortOrder;
//				}
//			}
//		}
//	}
}
