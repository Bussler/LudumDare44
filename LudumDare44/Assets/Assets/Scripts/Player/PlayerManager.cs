﻿using System;
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
	
	private int effectIndex = 0;
	
	private BaseEffect[] playerAbilities;

	private bool playEffect = false;
	
	[SerializeField] 
	private GameObject sprites, sprites2;
	
	
	
	// Use this for initialization
	void Start () {
	
		playerCurrentHealth = playerHealth;
		
		playerAbilities = new BaseEffect[2];

		playerAbilities[0] = new AoESampleAbility(Instantiate(sprites, transform.position, sprites.transform.rotation), mainCamera, Instantiate(sprites2, transform.position, sprites.transform.rotation));
		playerAbilities[0].start();
		playerAbilities[0].displayEffect();

        playerAbilities[1] = new HealEffect(10);
    }
	
	// Update is called once per frame
	void Update () {

		//show the display UI for the effect when time is freezed and an effect is selected
		if(effectIndex != -1 && timeIsFreezed)
			playerAbilities[effectIndex].displayEffect();
		
		if (playerCurrentHealth <= 0){
			Die();
		}
		
		
		//player pressed ability button
		if (Input.GetKeyDown(KeyCode.E)){
			//was the player in realtime mode before?
			if (canUseAbilities){
				//yes -> freeze time if the timer is full
				timeIsFreezed = true; // TODO actually freeze time
				canUseAbilities = false;
				effectTimer = 0;
			}else if (timeIsFreezed){
				//no -> time is already freezed
				timeIsFreezed = false;	//unfreeze time
				playerAbilities[effectIndex].playEffect();	//play the selected effect
			}
		}	
	}

	private void FixedUpdate(){
		if (effectTimer < 100){
			effectTimer++;
		}else
			canUseAbilities = true;
		//Debug.Log(effectTimer);
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

	public void chosenEffectCard(int index){
		effectIndex = index;

	}
}
