using UnityEngine;
using System.Collections;

public abstract class BarClass : MonoBehaviour {
	protected float maxLength;

	protected Vector3 changeScaleVector;
	protected Vector3 changePosVector;
	protected Vector3 originalScaleVector;
	protected Vector3 originalPosVector;
	
	public float getPercent() {
		return transform.localScale.x / maxLength; 
	}

	public void setPercent(float percent) {
		transform.localScale = originalScaleVector + new Vector3 (maxLength * percent, 0, 0);
		transform.localPosition = originalPosVector + new Vector3 (maxLength * percent / 2, 0, 0);
	}

	public float getMaxLength() {
		return maxLength;
	}
}
