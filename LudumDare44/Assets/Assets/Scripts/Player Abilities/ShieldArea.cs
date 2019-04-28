using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldArea : MonoBehaviour {


    public float time = 3f;//time to live


    private float elapsedTime = 0f;


    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= time)
        {
            Destroy(this.gameObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Player")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

}
