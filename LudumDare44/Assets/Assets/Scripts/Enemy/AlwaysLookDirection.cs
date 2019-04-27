using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysLookDirection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(this.transform.position + Vector3.down);
	}
}
