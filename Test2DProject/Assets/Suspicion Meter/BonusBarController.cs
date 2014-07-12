using UnityEngine;
using System.Collections;

public class BonusBarController : BarClass {
	private float decreaseRate;
	private float greenBarPercent;
	private float maxWaitTime;
	private float waitTimer;

	// Use this for initialization
	void Start () {
		maxWaitTime = 1f;
		originalScaleVector = transform.localScale;
		originalPosVector = transform.localPosition;
	}

	void Update () {
		if (getPercent() > greenBarPercent && waitTimer >= maxWaitTime) {
			decreaseRate = maxLength * Time.deltaTime / maxWaitTime;
			changeScaleVector = new Vector3 (decreaseRate, 0, 0);
			changePosVector = new Vector3 (decreaseRate / 2, 0, 0);
			transform.localScale -= changeScaleVector;
			transform.localPosition -= changePosVector;
		}

		waitTimer += Time.deltaTime;
	}

	public void bonusAddition(float percent, GreenBarController gr) {
		if (getPercent () <= greenBarPercent) {
			waitTimer = 0f;
			setPercent (gr.getPercent ());
		}
		gr.setPercent (gr.getPercent () - percent);
	}

	public void getGreenBarVars(GreenBarController gr) {
		maxLength = gr.getMaxLength();
		greenBarPercent = gr.getPercent();
	}

	public void resetBonus() {
		setPercent (0);
	}
}