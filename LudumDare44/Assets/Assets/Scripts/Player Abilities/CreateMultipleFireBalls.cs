using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMultipleFireBalls : MonoBehaviour {

    public GameObject FireBall;
    Transform playerPos;
    Vector3 enemyPos;

    int amtOfBalls;

    public void UpdateValues(GameObject fireball, Vector3 enemy, Transform player, int amt)
    {
        FireBall = fireball;
        playerPos = player;
        enemyPos = enemy;
        amtOfBalls = amt;

        StartCoroutine(spawnFBalls());

    }

    IEnumerator spawnFBalls()
    {
        for (int i=0;i<amtOfBalls;i++)
        {
            GameObject myBall = GameObject.Instantiate(FireBall, playerPos.position, Quaternion.identity);
            myBall.GetComponent<FireBallMovement>().UpdateValues(enemyPos);
            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }

}
