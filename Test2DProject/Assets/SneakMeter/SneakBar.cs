using UnityEngine;
using System.Collections;

public class SneakBar {

	//public SneakBarController SneakBar;
	public bool isDetected; //during detection, eye widens
	public bool isSeen; //when detection is maxed, player is seen
	public bool npcIsHostile;

	int detectionTicks;
	public Texture visibilityStage0;
	public Texture visibilityStage1;
	public Texture visibilityStage2;
	public Texture visibilityStage3;
	public Texture visibilityStage4A;

	private static int MAX_DETECTED = 4; //maximum value of detectionticks needed for isSeen

	// Use this for initialization
	public SneakBar () {
		isDetected = false;
		detectionTicks = 0;
	}

	public void incrementDetection() {
		detectionTicks++;
	}

	public void decrementDetection() {
		detectionTicks--;
	}

	public int getDetection {
		get {
			return detectionTicks;
		}
	}

	public bool NpcIsHostile {
		get {
			return npcIsHostile;
		}
	}
}
