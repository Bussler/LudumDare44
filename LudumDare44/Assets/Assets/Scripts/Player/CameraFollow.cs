using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[SerializeField]
	private GameObject player;
	
	
	// Update is called once per frame
	void LateUpdate (){
		transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	}
}
