using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


	[SerializeField] 
	private float movementSpeed = 5f, dodgeRollSpeed = 10f;

	private Vector3 previousWalkDirection;
	private Rigidbody rigidbody;

	private int canDodgeTimer = 100;

	private bool canDodge = true;
	// Use this for initialization
	void Start (){
		//get the rigidbody component
		rigidbody = GetComponent<Rigidbody>();
		if(rigidbody == null)
			Debug.LogError("No Rigidbody2D on object");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//reset the velocity, so that the player does not move when no button is pressed
			rigidbody.velocity = Vector3.zero;

			if (!canDodge)
				canDodgeTimer--;
			if (canDodgeTimer < 0){
				canDodgeTimer = 100;
				canDodge = true;
			}

			//WASD movement
			if (Input.GetKey(KeyCode.A)){
				rigidbody.velocity += (Vector3.left * movementSpeed);
			}

			if (Input.GetKey(KeyCode.D)){
				rigidbody.velocity += (Vector3.right * movementSpeed);
			}

			if (Input.GetKey(KeyCode.W)){
				rigidbody.velocity += (Vector3.forward * movementSpeed);
			}

			if (Input.GetKey(KeyCode.S)){
				rigidbody.velocity += (Vector3.back * movementSpeed);
			}

			if (rigidbody.velocity != Vector3.zero)
				previousWalkDirection = rigidbody.velocity;
			
			//Multiply the walk speed with dodge speed in order to make a dodge roll
			if (canDodge && Input.GetKeyDown(KeyCode.Space)){
				if(rigidbody.velocity == Vector3.zero)
					rigidbody.velocity = previousWalkDirection * dodgeRollSpeed;
				else
					rigidbody.velocity *= dodgeRollSpeed;
				canDodge = false;
			}
	}
}
