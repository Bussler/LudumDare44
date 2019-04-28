using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawn : BaseEffect
{
    GameObject FireBall;
    Transform playerPos;
    Transform enemyPos;
    int amtOfBalls;

    public FireBallSpawn(GameObject sprite, Camera camera, Transform player, int amt)
    {
        FireBall = sprite;
        mainCamera = camera;
        playerPos = player;
        amtOfBalls = amt;
    }

    public override void displayEffect()
    {
        Debug.Log("DisplayEffect Success");
    }

    public override void playEffect()
    {
        //Instantiate
        Vector2 m = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(m.x, m.y, mainCamera.transform.position.y - 1));

        /*GameObject myBall = GameObject.Instantiate(FireBall, playerPos.position, Quaternion.identity);
        myBall.GetComponent<FireBallMovement>().UpdateValues(mousePosition);*/
        GameObject.FindObjectOfType<CreateMultipleFireBalls>().UpdateValues(mousePosition, playerPos, amtOfBalls);
    }

    public override void start()
    {

    }
}
