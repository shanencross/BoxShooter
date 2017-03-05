using UnityEngine;
using System.Collections;

public class ControllerWithVariableJump : MonoBehaviour {

	// public variables
	public float moveSpeed = 6.0f;
	public float jumpSpeed = 5.0f;
	public float jumpTime = 1.0f;
	public float gravity = Physics.gravity.magnitude;

	private CharacterController myController;
	private Vector3 movement = Vector3.zero;
	private float jumpTimer = 0.0f;
	private bool jumping = false;

	// Use this for initialization
	void Start () {
		// store a reference to the CharacterController component on this gameObject
		// it is much more efficient to use GetComponent() once in Start and store
		// the result rather than continually use getComponent() in the Update function
		myController = gameObject.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {
		// Determine how much should move in the z-direction
		Vector3 movementZ = Input.GetAxis("Vertical") * Vector3.forward;

		// Determine how much should move in the x-direction
		Vector3 movementX = Input.GetAxis("Horizontal") * Vector3.right;

		// Convert combined Vector3 from local space to world space based on the position of the current gameobject (player)
		Vector3 movementXZ = transform.TransformDirection(movementZ+movementX);

		if (movementXZ.magnitude > 1)
			movementXZ.Normalize ();

		movementXZ *= moveSpeed;

		if (myController.isGrounded) {
			movement = movementXZ;
			if (Input.GetButtonDown ("Jump")) {
				movement.y = jumpSpeed;
				jumping = true;
			}
		}
		else {
			movement.x = movementXZ.x;
			movement.z = movementXZ.z;
			//movement.y -= gravity * Time.deltaTime;
		}

		float jumpPercentage = jumpTimer / jumpTime;

		if (jumpPercentage > 1.0f ) {
			jumpTimer = 0;
			jumping = false;
		}

		// Apply gravity (so the object will fall if not grounded)
		movement.y -= gravity * Time.deltaTime;

		Debug.Log ("isGrounded: " + myController.isGrounded + "jumpPercentage: " + jumpPercentage + "; Movement Vector = " + movement);

		// Actually move the character controller in the movement direction
		myController.Move(movement * Time.deltaTime);

		if (jumping) {
			jumpTimer += Time.deltaTime;
		}
	}
}