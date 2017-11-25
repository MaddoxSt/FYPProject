using System.Collections;
using UnityEngine;
using System.Collections;

public class CharMove : MonoBehaviour {


	//For walk and run speed
	public float walkSpeed = 2;
	public float runSpeed = 6;

	//Walk and turn smothing
	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;
	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;

	//Camera's transform
	Transform cameraMovement;


	//Prevent attack spamming
	[SerializeField]
	float attackReset = 0.5f;
	bool canAttack = true;

	//Setting the gravity and velocity
	public float gravity=-10f;
	float velocityY;

	Animator animator;
	CharacterController controller;


	void Start () {
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
		cameraMovement = Camera.main.transform;

	}

	void Update () {

		Attack ();
		//Walking and Running
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;



		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraMovement.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		bool running = Input.GetKey (KeyCode.LeftShift);
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

		velocityY += Time.deltaTime * gravity;

		Vector3 velocity = transform.forward * currentSpeed +Vector3.up*velocityY;
		if (controller.isGrounded) {
			velocityY = 0;
		}
		controller.Move (velocity * Time.deltaTime);

		float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
		animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
	}


	//Method responsible for attack animations and attack spam check

	void Attack(){

		if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack) {
			canAttack = false;
			animator.SetTrigger ("attack1");
			Invoke ("ResetAttackTimer", attackReset);
		}

		if (Input.GetKeyDown(KeyCode.Mouse1) && canAttack) {
			canAttack = false;
			animator.SetTrigger ("attack2");
			Invoke ("ResetAttackTimer", attackReset);
		}
			
	}


	//Method to set the attacking to true 
	void ResetAttackTimer(){
		canAttack = true;
	}

}

