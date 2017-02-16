using UnityEngine;
using System.Collections;

public class ControllerTest3 : MonoBehaviour {
	
	// public variables
	public float moveSpeed = 6.0f;
	public float jumpSpeed = 5.0f;
	public float gravity = 9.81f;

	private CharacterController myController;
	private Vector3 movement = Vector3.zero;
	private bool startFalling = false;

	// Use this for initialization
	void Start () {
		// store a reference to the CharacterController component on this gameObject
		// it is much more efficient to use GetComponent() once in Start and store
		// the result rather than continually use etComponent() in the Update function
		myController = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		// Determine how much should move in the x-direction
		Vector3 movementX = Input.GetAxis ("Horizontal") * Vector3.right;
		// Determine how much should move in the z-direction
		Vector3 movementZ = Input.GetAxis ("Vertical") * Vector3.forward;

		// Convert combined Vector3 from local space to world space based on the position of the current gameobject (player)
		Vector3 movementXZ = transform.TransformDirection (movementZ + movementX);
		if (movementXZ.magnitude > 1)
			movementXZ.Normalize ();
		movementXZ *= moveSpeed;

		//update X and Z movement values; Y movement value remains unchanged
		movement.x = movementXZ.x;
		movement.z = movementXZ.z;

		if (myController.isGrounded) {
			startFalling = false;
			//prevent movement.y from acquiring needlessly large negative values while grounded due to gravity
			movement.y = 0;
			// jump
			if (Input.GetButtonDown ("Jump")) {
				movement.y = jumpSpeed;
			}
		}

		if (Input.GetButtonUp("Jump") && myController.velocity.y >= 0) {
			startFalling = true;
		}

		if (startFalling) {
			movement.y -= jumpSpeed * Time.deltaTime;
			// not sure if this works
			//startFalling = false;
		}

		// Apply gravity (so the object will fall if not grounded)
		movement.y -= gravity * Time.deltaTime;
	
		// Actually move the character controller in the movement direction
		myController.Move(movement * Time.deltaTime);

		Debug.Log ("Character velocity: " + myController.velocity + ", magnitude: " + myController.velocity.magnitude  + "; startFalling: " + startFalling);
	}
}
