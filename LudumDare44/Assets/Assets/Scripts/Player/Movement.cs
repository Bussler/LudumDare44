using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


	[SerializeField] 
	private float movementSpeed = 5f;

	private Rigidbody rigidbody;
	// Use this for initialization
	void Start (){
		rigidbody = GetComponent<Rigidbody>();
		if(rigidbody == null)
			Debug.LogError("No Rigidbody2D on object");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbody.velocity = Vector3.zero;
		
			if (Input.GetKey(KeyCode.A)){
				rigidbody.velocity = (Vector2.left * movementSpeed);
			}

			if (Input.GetKey(KeyCode.D)){
				rigidbody.velocity = (Vector2.right * movementSpeed);
			}

			if (Input.GetKey(KeyCode.W)){
				rigidbody.velocity = (Vector3.forward * movementSpeed);
			}

			if (Input.GetKey(KeyCode.S)){
				rigidbody.velocity = (Vector3.back * movementSpeed);
			}
		Debug.Log("Velocity: " + rigidbody.velocity);
	}
}
