using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	// public variables
	public float moveSpeed = 3.0f;
	public float gravity = 9.81f;

	private CharacterController myController;
	private Vector3 movement = Vector3.zero;

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

		if (myController.isGrounded)
			movement = movementXZ;
		else {
			movement.x = movementXZ.x;
			movement.z = movementXZ.z;
			//movement.y -= gravity * Time.deltaTime;
		}

		// Apply gravity (so the object will fall if not grounded)
		movement.y -= gravity * Time.deltaTime;

		Debug.Log ("isGrounded: " + myController.isGrounded + "; Movement Vector = " + movement);

		// Actually move the character controller in the movement direction
		myController.Move(movement * Time.deltaTime);
	}
}