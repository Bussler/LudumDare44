using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMovement : MonoBehaviour {

    public float speed;

    public float dmg;

    public Vector3 Target;

    private bool ready;
	
	// Update is called once per frame
	void Update () {

        if (ready)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, Target, speed*Time.deltaTime);

            float mag = Mathf.Abs(Vector3.Magnitude(new Vector2(transform.position.x, transform.position.z) - new Vector2(Target.x, Target.z)));
            if (mag<=0.1)
            {
                Destroy(gameObject);
            }
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
            Destroy(gameObject);
        }
        
    }

}
