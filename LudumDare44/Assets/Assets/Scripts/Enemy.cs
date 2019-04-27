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
    public float time = 0;
    private bool isAttacking= false;

    public GameObject SingleProjectile;
    public GameObject PatternProjectile;

    public Pattern[] patterns;
    public int patternsPerAttack;
    public float timeBetweenPatterns;


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
            Debug.Log("Attack");
            time = 0;
            isAttacking = true;
            int x = Random.Range(0, attacks.Count);

            string s = attacks[x];
            Debug.Log(s);
            switch (s)
            {

                case "ShootProjectile":
                    ShootProjectile();
                    break;

                case "ShootPattern":
                   StartCoroutine( "ShootPattern");
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
        Debug.Log("ShootProjectile");
      GameObject p=  Instantiate(SingleProjectile, this.transform.position, Quaternion.identity);
        p.transform.LookAt(player.transform);
        isAttacking = false;
    }

   IEnumerator ShootPattern()
    {
        Debug.Log("ShootPattern");

        for (int i=0; i <  patternsPerAttack; i++)
        {

            for(int l =0; l< patterns[i].patternPoint.Length; l++)
            {
                Instantiate(PatternProjectile, patterns[i].patternPoint[l].position, patterns[i].patternPoint[l].rotation);
                
            }
            yield return new WaitForSeconds(timeBetweenPatterns);
        }
        isAttacking = false;

    }

    public void Charge()
    {

    }

    public void SpawnObstacle()
    {
       GameObject o= Instantiate(Obstacle, this.transform.position, Quaternion.identity);

    }




    public void TakeDmage(float amount)
    {

        healthCurr -= amount;

    }



}
