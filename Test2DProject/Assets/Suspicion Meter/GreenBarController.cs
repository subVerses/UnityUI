using UnityEngine;
using System.Collections;

public class GreenBarController : BarClass {
	private float maxTime;
	private float increaseRate; //Amount Bar Increases after each tick
	bool fullySuspicious;

	public Material newMaterialRef; 

	public Texture greenTexture;
	public Texture greenYellowTexture;
	public Texture yellowTexture;
	public Texture yellowOrangeTexture;
	public Texture orangeTexture;
	public Texture redTexture;

	// Use this for initialization
	void Start () {
		fullySuspicious = false;
		renderer.material = newMaterialRef;
		renderer.material.SetTexture ("_MainTex", greenTexture);
	}
	
	// Update is called once per frame
	void Update () {
		if (getPercent() < .35f && renderer.material.GetTexture("_MainTex") != greenTexture)
			renderer.material.SetTexture("_MainTex", greenTexture);
		else if (getPercent() >= .35f && getPercent() < .6f && renderer.material.GetTexture("_MainTex") != greenYellowTexture)
			renderer.material.SetTexture("_MainTex", greenYellowTexture);
		else if (getPercent() >= .6f && getPercent() < .75f && renderer.material.GetTexture("_MainTex") != yellowTexture)
			renderer.material.SetTexture("_MainTex", yellowTexture);
		else if (getPercent() >= .75f && getPercent() < .85f && renderer.material.GetTexture("_MainTex") != yellowOrangeTexture)
			renderer.material.SetTexture("_MainTex", yellowOrangeTexture);
		else if (getPercent() >= .85f && getPercent() < .925f && renderer.material.GetTexture("_MainTex") != orangeTexture)
			renderer.material.SetTexture("_MainTex", orangeTexture);
		else if (getPercent() >= .925f && getPercent() < 1f && renderer.material.GetTexture("_MainTex") != redTexture)
			renderer.material.SetTexture("_MainTex", redTexture);

		if (getPercent() <= 1f &&  !fullySuspicious) {
			increaseRate = maxLength * Time.deltaTime / maxTime;
			changeScaleVector = new Vector3 (increaseRate, 0, 0);
			changePosVector = new Vector3 (increaseRate / 2, 0, 0);
			transform.localScale += changeScaleVector;
			transform.localPosition += changePosVector;
		} else {
			fullySuspicious = true;
		}
	}

	public void setTime(float tm) {
		maxTime = tm;
		maxLength = transform.localScale.x;
		increaseRate = maxLength * Time.deltaTime / maxTime;
		changeScaleVector = new Vector3 (increaseRate, 0, 0);
		changePosVector = new Vector3 (increaseRate / 2, 0, 0);
		transform.localPosition = new Vector3 (transform.localPosition.x - maxLength / 2, 
		                                       transform.localPosition.y, 
		                                       transform.localPosition.z);
		originalPosVector = transform.localPosition;
		transform.localScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);
		originalScaleVector = transform.localScale;
	}

	public void resetConversation() {
		fullySuspicious = false;
		increaseRate = 0;
		changeScaleVector = new Vector3 (0, 0, 0);
		changePosVector = new Vector3 (0, 0, 0);
		transform.localScale = originalScaleVector;
		transform.localPosition = originalPosVector;
		renderer.material = newMaterialRef;
		renderer.material.SetTexture ("_MainTex", greenTexture);
	}

	public float getIncreaseRate() {
		return increaseRate;
	}

	public float getMaxTime() {
		return maxTime;
	}
	
	public bool isSuspicious() {
		return fullySuspicious;
	}
}