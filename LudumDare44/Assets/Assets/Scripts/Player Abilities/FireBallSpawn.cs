using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawn : BaseEffect
{
    GameObject FireBall;
    Transform playerPos;
    Transform enemyPos;

    public FireBallSpawn(GameObject sprite, Camera camera, Transform player)
    {
        FireBall = sprite;
        mainCamera = camera;
        playerPos = player;
    }

    public override void displayEffect()
    {
        Debug.Log("DisplayEffect Success");
    }

    public override void playEffect()
    {
        //Instantiate
        Vector2 m = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(m.x, m.y, 10));
        GameObject myBall = GameObject.Instantiate(FireBall, playerPos.position, Quaternion.identity);
        myBall.GetComponent<FireBallMovement>().UpdateValues(mousePosition);

    }

    public override void start()
    {
    }
}
