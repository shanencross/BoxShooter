using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour {
	// public variables
	public float secondsBetweenSpawning = 0.1f;
	public bool spawnPositionRelativeToSpawner = true;
	public bool spawnerRunning = true;

	public bool destroySpawnerAtPointValue = true;
	public int pointsToDestroySpawnerAt = 25;
	public bool destroySpawnerAtTime = false;
	public float secondsToDestroySpawnerAt = 30.0f;
	
	public float xMinRange = -25.0f;
	public float xMaxRange = 25.0f;
	public float yMinRange = 8.0f;
	public float yMaxRange = 25.0f;
	public float zMinRange = -25.0f;
	public float zMaxRange = 25.0f;
	public GameObject[] spawnObjects; // what prefabs to spawn

	private float nextSpawnTime;
	private bool spawnerKilled = false;
	[SerializeField]
	private float pointsAcquired = 0;
	private float targetTime;

	// Use this for initialization
	void Start () {
		// determine when to spawn the next object
		nextSpawnTime = Time.time + secondsBetweenSpawning;

		targetTime = Time.time + secondsToDestroySpawnerAt;
	}
	
	// Update is called once per frame
	void Update () {
		// Destroy spawner if flagged for killing and children are all gone
		if (spawnerKilled && transform.childCount <= 0) {
			Destroy (gameObject);
		}

		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		// Exit if spawner is deactivated
		if (!spawnerRunning) {
			return;
		}

		// if time to spawn a new game object
		if (Time.time  >= nextSpawnTime) {
			// Spawn the game object through function below
			MakeThingToSpawn ();

			// determine the next time to spawn the object
			nextSpawnTime = Time.time+secondsBetweenSpawning;
		}

		//If enough points have been acquired, terminate spawner
		if ((destroySpawnerAtPointValue && pointsAcquired >= pointsToDestroySpawnerAt)
		    || (destroySpawnerAtTime && Time.time >= targetTime)) {
			//Debug.Log ("Destroying Spawner");
			destroySpawner ();
		}
	}

	void MakeThingToSpawn () {
		Vector3 spawnPosition;

		// get a random position between the specified ranges
		spawnPosition.x = Random.Range (xMinRange, xMaxRange);
		spawnPosition.y = Random.Range (yMinRange, yMaxRange);
		spawnPosition.z = Random.Range (zMinRange, zMaxRange);

		// determine which object to spawn
		int objectToSpawn = Random.Range (0, spawnObjects.Length);

		// make spawn position relative to spawner object if corresponding flag is set to true;
		// otherwise leave it as a world coordinate
		if (spawnPositionRelativeToSpawner)
			spawnPosition += transform.position;

		// actually spawn the game object
		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, transform.rotation) as GameObject;

		// make the parent the spawner so hierarchy doesn't get super messy
		spawnedObject.transform.parent = gameObject.transform;
	}

	void destroySpawner() {
		//Debug.Log ("Destroying spawner");
		//Trigger the destruction animation for each child targte and destroy them

		//deactivate and initiate destruction of each child target
		foreach (Transform childTransform in transform) {
			if (childTransform.tag == "Target")
				childTransform.GetComponent<TargetBehavior> ().targetActive = false;
				childTransform.GetComponent<TargetExit> ().startTargetDestruction ();
		}
		//Deactivate the spawner object
		spawnerRunning = false;

		//Flag spawner object for destruction after child objects are destroyed
		spawnerKilled = true;
	}

	public void addPoints(int pointsAdded) {
		pointsAcquired += pointsAdded;
	}
}