using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour {

    public float speed = 10;

    public float dmg = 5;

    public Vector3 Target;

    private bool ready;
	
	// Update is called once per frame
	void Update () {

        if (ready)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, Target, speed*Time.deltaTime);
        }

	}

    public void UpdateValues( Vector3 t)
    {
        Target = t;
        ready = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDmage(-dmg);
        }
    }

}
