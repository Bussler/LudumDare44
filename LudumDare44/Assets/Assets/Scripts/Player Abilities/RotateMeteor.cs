using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMeteor : MonoBehaviour
{

	private float rotateSpeed = 2f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * rotateSpeed);
	}
}
