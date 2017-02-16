using UnityEngine;
using System.Collections;

public class BasicTargetMover : MonoBehaviour {

	public float spinSpeed = 180f;
	public float motionMagnitude = 0.1f;

	public bool doSpin = true;
	public bool doMotion = false;

	// Update is called once per frame
	void Update () {
		if (doSpin) {
			//Rotate object about y axis
			gameObject.transform.Rotate (Vector3.up * spinSpeed * Time.deltaTime);
		}
		if (doMotion) {
			//Make object oscillate up and down y axis in a sinusoidal pattern with magnitude of motionMagnitude
			gameObject.transform.Translate (Vector3.up * Mathf.Cos (Time.timeSinceLevelLoad) * motionMagnitude);
		}
	}
}
