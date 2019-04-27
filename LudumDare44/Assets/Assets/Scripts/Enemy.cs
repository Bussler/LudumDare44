using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool canFollowPlayer;
    public bool canShootProjectile;
    public bool canShootPattern;
    public bool canChargeAtPlayer;
    public bool canSpawnObstacle;
    

    public float healthMax;
    private float healthCurr;

    public float timeBetweenAttacks;

    public List<string> attacks;
    private float time = 0;
    private bool isAttacking= false;

    public GameObject SingleProjectile;
    public GameObject PatternProjectile;


    public GameObject Obstacle;


    private GameObject player;

	// Use this for initialization
	void Start () {
        healthCurr = healthMax;
        player = GameObject.FindGameObjectWithTag("Player");


        if (canShootProjectile)
        {
            attacks.Add("ShootProjectile");
        }
        if (canShootPattern)
        {
            attacks.Add("ShootPattern");
        }
        if (canChargeAtPlayer)
        {
            attacks.Add("Charge");
        }
        if (canSpawnObstacle)
        {
            attacks.Add("SpawnObstacle");
        }




    }
	
	// Update is called once per frame
	void Update () {
        if (!isAttacking)
        {
            time = time + Time.deltaTime;
        }
        if(time >= timeBetweenAttacks)
        {
            time = 0;

            int x = Random.Range(0, attacks.Count);

            string s = attacks[x];
            switch (s)
            {

                case "ShootProjectile":
                    ShootProjectile();
                    break;

                case "ShootPattern":
                    ShootPattern();
                    break;

                case "Charge":
                    Charge();
                    break;

                case "SpawnObstacle":
                    SpawnObstacle();
                    break;

            }


        }



	}

    public void ShootProjectile()
    {

      GameObject p=  Instantiate(SingleProjectile, this.transform.position, Quaternion.identity);
        p.transform.LookAt(player.transform);
    }

    public void ShootPattern()
    {

    }

    public void Charge()
    {

    }

    public void SpawnObstacle()
    {

    }




    public void TakeDmage(float amount)
    {

        healthCurr -= amount;

    }



}
