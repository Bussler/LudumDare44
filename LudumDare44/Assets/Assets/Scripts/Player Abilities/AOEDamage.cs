using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage: BaseEffect {

    public GameObject groundAoe;

    public AOEDamage(GameObject sprite, Camera camera)
    {
        groundAoe = sprite;
        mainCamera = camera;
    }

    public override void displayEffect()
    {
        Debug.Log("DisplayEffect Success");
    }

    public override void playEffect()
    {
        Vector2 m = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(m.x, m.y, 10));

        GameObject aoe = GameObject.Instantiate(groundAoe, mousePosition, Quaternion.Euler(90,0,0));

    }

    public override void start()
    {

    }

}
