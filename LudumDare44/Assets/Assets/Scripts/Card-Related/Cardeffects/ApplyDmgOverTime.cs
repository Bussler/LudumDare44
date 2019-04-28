using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDmgOverTime : MonoBehaviour {

    public float time = 5;//time to live
    public int dmg = 1; //dmg to apply each second

    public bool doesDmg = true;

    private float elapsedTime=0f;

    public float timeInterval = 0.5f;

    private float ElapsedInterval;

	void Update () {
        elapsedTime += Time.deltaTime;
        if (elapsedTime>=time)
        {
            Destroy(this.gameObject);
        }

	}


    private void OnTriggerStay(Collider collision)
    {

        ElapsedInterval += Time.deltaTime;
        if (ElapsedInterval >= timeInterval)
        {

            if (!doesDmg && collision.gameObject.name=="Player")
            {
                PlayerManager plM = GameObject.FindObjectOfType<PlayerManager>();
                if (plM != null)
                {
                    plM.GetHealth(dmg);
                    Debug.Log("Player healed");
                }
            }
            if (doesDmg)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDmage(-dmg);
                }
            }

            ElapsedInterval = 0f;

        }
    }

}
