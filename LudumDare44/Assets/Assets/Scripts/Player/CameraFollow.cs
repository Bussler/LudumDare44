using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[SerializeField]
	private GameObject player;

    [SerializeField]
    private float speed;
	
	
	// Update is called once per frame
	void LateUpdate (){

        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z), speed * Time.deltaTime);

    }
}
