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
    public bool canTeleport;

    public float healthMax;
    private float healthCurr;
    public int damage;
    public float speed;

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
    private BoxCollider spawnColl;

    public GameObject Minion;

    public float timeBetweenTeleport;
    public GameObject teleportMarker;
    public GameObject teleportObstacle;

    public float ChargeSpeed;
    private bool isCharging;

    private GameObject player;
    private NavMeshAgent agent;
  

	// Use this for initialization
	void Start (){
        spawnColl = GameObject.Find("Raumbegrenzer").GetComponent<BoxCollider>();
        healthCurr = healthMax;
        player = GameObject.Find("Player");
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = speed;
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
        if (canTeleport)
        {
            attacks.Add("Teleport");           
        }

    }
	
	// Update is called once per frame
	void Update () {



        if (healthCurr <= 0)
        {
            Die();
        }

        if (canFollowPlayer && !isCharging) 
        {
           
            agent.destination = player.transform.position;
        }

      ///  if (!isAttacking)
      // {
            time = time + Time.deltaTime;
      // }
        if(time >= timeBetweenAttacks)
        {
            Debug.Log("Attack");
            time = 0;
            isAttacking = true;
            int x = Random.Range(0, attacks.Count);

            string s = attacks[x];
            //Debug.Log(s);
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
                    StartCoroutine("SpawnMinion");
                   // isAttacking = false;
                    break;

                case "Teleport":
                    StartCoroutine("Teleport");
                    break;
            }


        }

        if (isCharging)
        {
            Debug.Log((agent.destination - this.transform.position).magnitude);
            if((agent.destination- this.transform.position).magnitude <= 0.5f)
            {
                Debug.Log("EndCharge");
                agent.speed = speed;
                isAttacking = false;
               isCharging = false;
                if (!canFollowPlayer)
                {
                    agent.enabled = false;
                }
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
                Instantiate(PatternProjectile, this.transform.position+patterns[i].patternPoint[l].position, patterns[i].patternPoint[l].rotation);
                
            }
            yield return new WaitForSeconds(timeBetweenPatterns);
        }
        isAttacking = false;

    }

    public void Charge()
    {
        Debug.Log("ChargeFunc");
        agent.enabled = true;
        agent.destination = player.transform.position+3*(player.transform.position-this.transform.position).normalized;
        agent.speed = ChargeSpeed;
        isCharging = true;
    }

    IEnumerator SpawnObstacle()
    {
        Debug.Log("SpawnObstacleFunc");
        GameObject o = Instantiate(Obstacle, this.transform.position, Quaternion.identity);
        for (int i =0; i < 1000; i++)
        {
            Vector3 newScale = new Vector3(o.transform.localScale.x+0.001f, o.transform.localScale.y+0.001f, o.transform.localScale.z+ 0.001f);
            o.transform.localScale = newScale;
            yield return new WaitForSeconds(growTime / 1000);
                 
        }

      
        yield return new WaitForSeconds(obstacleActiveTime);
        isAttacking = false;
        Destroy(o.gameObject);


    }

    IEnumerator SpawnDamagePlane()
    {
        Debug.Log("SpawnDamagePlaneFunc");
        Vector3 spawnpoint = new Vector3(spawnColl.bounds.center.x + Random.Range(-0.5f*spawnColl.bounds.size.x,0.5f* spawnColl.bounds.size.x), 1, spawnColl.bounds.center.z + Random.Range(-0.5f*spawnColl.bounds.size.z,0.5f* spawnColl.bounds.size.z));
        Instantiate(DamagePlane, spawnpoint, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
         isAttacking = false;
    }

    IEnumerator SpawnMinion()
    {
        Vector3 spawnpoint = new Vector3(spawnColl.bounds.center.x + Random.Range(-0.5f * spawnColl.bounds.size.x, 0.5f * spawnColl.bounds.size.x), 1f, spawnColl.bounds.center.z + Random.Range(-0.5f * spawnColl.bounds.size.z, 0.5f * spawnColl.bounds.size.z));
        Instantiate(Minion, spawnpoint, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
         isAttacking = false;

    }

   IEnumerator  Teleport()
    {
        Vector3 teleportPoint = new Vector3(spawnColl.bounds.center.x + Random.Range(-0.4f * spawnColl.bounds.size.x, 0.4f * spawnColl.bounds.size.x), 1f, spawnColl.bounds.center.z + Random.Range(-0.4f * spawnColl.bounds.size.z, 0.4f * spawnColl.bounds.size.z));
        this.transform.position = new Vector3(1000, 1, 1000);
       GameObject m= Instantiate(teleportMarker,teleportPoint,Quaternion.identity);
        yield return new WaitForSeconds(timeBetweenTeleport);

        Destroy(m);
        this.transform.position = teleportPoint;
        Instantiate(teleportObstacle, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
        }
    }


    public void TakeDmage(float amount)
    {

        healthCurr -= amount;

    }

    public void Die()
    {
        player.GetComponent<PlayerManager>().battleWon();
        Destroy(this.gameObject);
    }

}
