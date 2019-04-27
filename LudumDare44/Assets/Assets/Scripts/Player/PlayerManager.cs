using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour{

	[SerializeField]
	private int playerHealth, playerDamage;
    private int playerCurrentHealth;
	[SerializeField]
	private float effectTimer;

	private Boolean canUseAbilities;
	
	[SerializeField]
	private BaseEffect[] gameEffects;
	
	// Use this for initialization
	void Start () {
        playerCurrentHealth = playerHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerCurrentHealth <= 0)
        {
            Die();
        }


	}

    public void TakeDamage(int amount)
    {
        playerCurrentHealth -= amount;
    }

    public void GetHealth(int amount)
    {

    }

    public void Die()
    {
        //GameOver
    }
    




}
