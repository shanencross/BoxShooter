using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceTime : MonoBehaviour {

	public float timeReduced = 1.0f;
	public float timeReducePeriod = 1.0f; // 1 second
	//public AudioSource timeReductionSound;

	private float lastTime = -1.0f;


	void OnTriggerExit(Collider col) {
		if (!col.gameObject.CompareTag ("Player"))
			return;
		lastTime = -1.0f;
		//GetComponent<AudioSource> ().Stop ();
		//Debug.Log ("leaving time reduction zone");
	}

	void OnTriggerStay(Collider col) {
		if (!col.gameObject.CompareTag ("Player"))
			return;
		if (!GameManager.gm)
			return;
		if (GameManager.gm.gameIsOver)
			return;

		int numberOfReductions = 0;
		float currentTime = Time.time;
		float timeSinceLastRun = currentTime - lastTime; 
		if (lastTime < 0) {
			timeSinceLastRun = -1.0f;
		}
		if ((timeSinceLastRun >= timeReducePeriod || timeSinceLastRun < 0)) {
			numberOfReductions = Mathf.FloorToInt (timeSinceLastRun / timeReducePeriod);
			if (timeSinceLastRun < 0)
				numberOfReductions = 1;
			GameManager.gm.targetHit (0, -timeReduced*numberOfReductions);
			lastTime = currentTime;
		} 
	//	if (GetComponent<AudioSource> ()) {
	//		if (!GetComponent<AudioSource>().isPlaying)
	//			GetComponent<AudioSource> ().Play ();
	//	}

		//Debug.Log ("timeSinceLastRun: " + timeSinceLastRun + "; Number of reductions: " + numberOfReductions);


	}
}
