using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour {
	// target impact on game
	public int scoreAmount = 0;
	public float timeAmount = 0.0f;
	public bool targetActive = true;

	// explosion when hit?
	public GameObject explosionPrefab;

	// when collided with another gameObject
	void OnCollisionEnter (Collision newCollision) {
		//Debug.Log ("Collision with target!");
		// exit if there is a game manager and the game is over
		if (GameManager.gm && GameManager.gm.gameIsOver)
			return;

		if (!targetActive)
			return;

		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			if (explosionPrefab) {
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate (explosionPrefab, transform.position, transform.rotation);
			}

			// if game manager exists, make adjustments based on target properties
			if (GameManager.gm)
				GameManager.gm.targetHit (scoreAmount, timeAmount);

			if (transform.parent && transform.parent.tag == "Spawner")
				transform.parent.GetComponent<SpawnGameObjects> ().addPoints (scoreAmount);	
				
			// destroy the projectile
			Destroy (newCollision.gameObject);
				
			// destroy self
			Destroy (gameObject);
		}
	}
}
