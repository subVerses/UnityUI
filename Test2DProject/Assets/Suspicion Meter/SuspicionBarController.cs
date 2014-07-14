using UnityEngine;
using System.Collections;

public class SuspicionBarController : MonoBehaviour {
	public GreenBarController greenBar;
	public BonusBarController bonusBar;
	int ticks;

	// Use this for initialization
	void Start () {
		ticks = 0;
		greenBar.setTime (30f);
		greenBar.resetConversation ();
	}
	
	// Update is called once per frame
	void Update () {
		bonusBar.getGreenBarVars (greenBar);
//		if (greenBar.getPercent () >= .6f)
//			bonusBar.bonusAddition (.45f, greenBar);
//		if (ticks == 750)
//			bonusBar.bonusAddition (.1f, greenBar);
		ticks++;
	}
	
	public void addBonus(float percent) {
		if(percent >= greenBar.getPercent())
			bonusBar.bonusAddition(greenBar.getPercent (), greenBar);
		else
			bonusBar.bonusAddition (percent, greenBar);
	}
	
	public bool isSuspicious() {
		return greenBar.isSuspicious ();
	}
	
	public void reset() {
		greenBar.resetConversation ();
	}
}