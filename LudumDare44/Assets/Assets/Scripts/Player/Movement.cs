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

			//can the player dodge?
			if (!canDodge)
				//no -> subtract one from the timer
				canDodgeTimer--;
			//is the dodge timer <= 0?
			if (canDodgeTimer <= 0){
				//yes -> the cooldown has ended
				canDodgeTimer = 100;	//reset timer
				canDodge = true;		//activate dodge ability
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
			
			//Can the player dodge and did he press space button? 
			if (canDodge && Input.GetKeyDown(KeyCode.Space)){
				//yes -> is teh current velocity = (0,0,0)?
				if(rigidbody.velocity == Vector3.zero)
					//yes-> take the previous walking direction and dodge into that direction
					rigidbody.velocity = previousWalkDirection * dodgeRollSpeed;
				else
					//no->dodge into the current waling direction 
					rigidbody.velocity *= dodgeRollSpeed;
				canDodge = false;
				Debug.Log("Player Dodged");
			}
	}
}
