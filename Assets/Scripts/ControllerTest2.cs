using UnityEngine;
using System.Collections;

public class ControllerTest2 : MonoBehaviour {

	public float speed = 6.0f;
	public float jumpSpeed = 100.0f;
	public float gravity = 25.0f;
	private Vector3 moveDirection = Vector3.zero;
	private float vSpeed = 0f;

	void Update() {
		CharacterController controller = gameObject.GetComponent<CharacterController>();
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		if (controller.isGrounded) {
			vSpeed = -1;
			if (Input.GetButtonDown ("Jump")) {
				vSpeed = jumpSpeed;
			}
		}
		vSpeed -= gravity * Time.deltaTime;
		moveDirection.y = vSpeed;
		controller.Move(moveDirection * Time.deltaTime);
		Debug.Log("Grounded: " + controller.isGrounded + " vSpeed: " + vSpeed);
	}
}