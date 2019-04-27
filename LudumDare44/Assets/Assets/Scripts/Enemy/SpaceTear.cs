using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTear : MonoBehaviour {
    public float timeBetweenSpriteSwitch;
    private float time;
    public Sprite[] sprites;
    private int x=0;
    private SpriteRenderer render;
	// Use this for initialization
	void Start () {
        render = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;

        if(time > timeBetweenSpriteSwitch)
        {
            time = 0;
            if(x == 0)
            {
                render.sprite = sprites[1];
                x = 1;
            }
            else
            {
                render.sprite = sprites[0];
                x = 0;
            }
        }
	}
}
