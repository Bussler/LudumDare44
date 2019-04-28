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
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(m.x, m.y, mainCamera.transform.position.y-1));

        GameObject aoe = GameObject.Instantiate(groundAoe, new Vector3(mousePosition.x,0.5f, mousePosition.z) , Quaternion.Euler(90,0,0));

    }

    public override void start()
    {
    }

}
