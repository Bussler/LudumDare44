using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PlayerManager : MonoBehaviour{

	[SerializeField]
	private int playerHealth, playerDamage;
	private int playerCurrentHealth;
	[SerializeField]
	private float effectTimer;

	[SerializeField] 
	private Camera mainCamera;
	
	private Boolean canUseAbilities, timeIsFreezed;
	
	
	private BaseEffect[] playerAbilities;

	private bool playEffect = false;
	
	[SerializeField] 
	private GameObject sprites, sprites2, DmgAoe, HealAoe;
	
	
	
	// Use this for initialization
	void Start () {
	
		playerCurrentHealth = playerHealth;
		
		playerAbilities = new BaseEffect[4];

		//playerAbilities[0] = new AoESampleAbility(Instantiate(sprites, transform.position, sprites.transform.rotation), mainCamera, Instantiate(sprites2, transform.position, sprites.transform.rotation));

        playerAbilities[1] = new HealEffect(10);
        playerAbilities[2] = new AOEDamage(DmgAoe, mainCamera);
        playerAbilities[3] = new AOEDamage(HealAoe, mainCamera);
    }
	
	// Update is called once per frame
	void Update () {
		
		if (playerCurrentHealth <= 0){
			Die();
		}
		
	}

    public void DisplayEffect(int i)
    {
        if (timeIsFreezed)
        {
            playerAbilities[i].displayEffect();
        }
    }

    public void PlayEffect(int i)
    {
        if (timeIsFreezed)
        {
            playerAbilities[i].playEffect();	//play the selected effect

            timeIsFreezed = false;
        }
    }

	private void FixedUpdate(){
		if (effectTimer < 100){
			effectTimer++;
        }
        else
        {
            if (effectTimer>=100 && !timeIsFreezed)
            {
                timeIsFreezed = true; //TODO actually freeze time
                effectTimer = 0;
                Debug.Log("Can use ability");
            }

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
