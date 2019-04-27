using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public bool canFollowPlayer;
    public bool canShootProjectile;
    public bool canShootPattern;
    public bool canChargeAtPlayer;
    public bool canSpawnObstacle;
    public bool canSpawnDamagePlane;
    public bool canSpawnMinions;

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
    public float obstacleActiveTime;
    public float growTime;

    public GameObject DamagePlane;
    public BoxCollider spawnColl;

    public GameObject Minion;


    private GameObject player;
    private NavMeshAgent agent;
  

	// Use this for initialization
	void Start () {
        healthCurr = healthMax;
        player = GameObject.FindGameObjectWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        if (canFollowPlayer)
        {
            agent.enabled = true;
            agent.destination = player.transform.position;
        }

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
        if (canSpawnDamagePlane)
        {
            attacks.Add("SpawnDamagePlane");
        }
        if (canSpawnMinions)
        {
            attacks.Add("SpawnMinon");
        }


    }
	
	// Update is called once per frame
	void Update () {

        if (canFollowPlayer)
        {
           
            agent.destination = player.transform.position;
        }

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
                    StartCoroutine("SpawnObstacle");
                    break;

                case "SpawnDamagePlane":
                    StartCoroutine("SpawnDamagePlane");
                   // isAttacking = false;
                    break;

                case "SpawnMinion":
                    StartCoroutine(" SpawnMinion");
                   // isAttacking = false;
                    break;
            }


        }



	}

    public void ShootProjectile()
    {
        Debug.Log("ShootProjectileFunc");
      GameObject p=  Instantiate(SingleProjectile, this.transform.position, Quaternion.identity);
        p.transform.LookAt(player.transform);
        isAttacking = false;
    }

   IEnumerator ShootPattern()
    {
        Debug.Log("ShootPatternFunc");

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

    IEnumerator SpawnObstacle()
    {
        Debug.Log("SpawnObstacleFunc");
        GameObject o = Instantiate(Obstacle, this.transform.position, Quaternion.identity);
        for (int i =0; i < 100; i++)
        {
            Vector3 newScale = new Vector3(o.transform.localScale.x+0.01f, o.transform.localScale.y+0.01f, o.transform.localScale.z+ 0.01f);
            o.transform.localScale = newScale;
            yield return new WaitForSeconds(growTime / 100);
                 
        }

      
        yield return new WaitForSeconds(obstacleActiveTime);
        isAttacking = false;
        Destroy(o.gameObject);


    }

    IEnumerator SpawnDamagePlane()
    {
        Debug.Log("SpawnDamagePlaneFunc");
        Vector3 spawnpoint = new Vector3(spawnColl.bounds.center.x + Random.Range(-0.5f*spawnColl.bounds.size.x,0.5f* spawnColl.bounds.size.x), 0, spawnColl.bounds.center.z + Random.Range(-0.5f*spawnColl.bounds.size.z,0.5f* spawnColl.bounds.size.z));
        Instantiate(DamagePlane, spawnpoint, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
         isAttacking = false;
    }

    IEnumerator SpawnMinion()
    {
        Vector3 spawnpoint = new Vector3(spawnColl.bounds.center.x + Random.Range(-0.5f * spawnColl.bounds.size.x, 0.5f * spawnColl.bounds.size.x), 0.5f, spawnColl.bounds.center.z + Random.Range(-0.5f * spawnColl.bounds.size.z, 0.5f * spawnColl.bounds.size.z));
        Instantiate(Minion, spawnpoint, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
         isAttacking = false;

    }


    public void TakeDmage(float amount)
    {

        healthCurr -= amount;

    }



}
