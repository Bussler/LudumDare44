using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlane : MonoBehaviour {

    public float growTime;
    public float lifeTime;
    
	// Use this for initialization
	void Start () {
        StartCoroutine("Grow");
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}

    IEnumerator Grow()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 newScale = new Vector3(transform.localScale.x + 0.01f, transform.localScale.y + 0.01f, transform.localScale.z + 0.01f);
            transform.localScale = newScale;
            yield return new WaitForSeconds(growTime / 100);

        }
    }
}
