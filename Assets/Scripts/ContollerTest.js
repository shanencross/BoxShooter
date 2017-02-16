#pragma strict
 	 var speed : float = 6.0;
     var jumpSpeed : float = 100.0;
     var gravity : float = 25.0;
     private var moveDirection : Vector3 = Vector3.zero;
     private var vSpeed : float = 0;
     
     function ControllerTest(){
         var controller : CharacterController = GetComponent(CharacterController);
         moveDirection = Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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