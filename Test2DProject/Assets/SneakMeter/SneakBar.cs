using UnityEngine;
using System.Collections;

public class SneakBar {

	//public SneakBarController SneakBar;
	public bool isDetected; //during detection, eye widens
	public bool isSeen; //when detection is maxed, player is seen
	public bool npcIsHostile;

	int detectionTicks;
	public Texture visibilityStage0 = Resources.Load<Texture>("SneakMeter/VisiblityStage0");
	public Texture visibilityStage1 = Resources.Load<Texture>("SneakMeter/VisibilityStage1");
	public Texture visibilityStage2 = Resources.Load<Texture>("SneakMeter/VisibilityStage2");
	public Texture visibilityStage3 = Resources.Load<Texture>("SneakMeter/VisibilityStage3");
	public Texture visibilityStage4A = Resources.Load<Texture>("SneakMeter/VisibilityStage4");

	public const int MAX_DETECTED = 4; //maximum value of detectionticks needed for isSeen

	// Use this for initialization
	public SneakBar () {
		isDetected = false;
		detectionTicks = 0;
	}

	public void incrementDetection() {
		if(detectionTicks<MAX_DETECTED)
			detectionTicks++;
	}

	public void decrementDetection() {
		if(detectionTicks>0)
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

	public Texture VisibilityStage0 {
		get {
			return visibilityStage0;
		}
	}
	
	public Texture VisibilityStage1 {
		get {
			return visibilityStage1;
		}
	}
	
	public Texture VisibilityStage2 {
		get {
			return visibilityStage2;
		}
	}
	
	public Texture VisibilityStage3 {
		get {
			return visibilityStage3;
		}
	}
	
	public Texture VisibilityStage4A {
		get {
			return visibilityStage4A;
		}
	}
}
