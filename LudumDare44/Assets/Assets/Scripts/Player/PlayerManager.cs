using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour{

	[SerializeField]
	private int playerHealth, playerDamage;
	
	[SerializeField]
	private float effectTimer;

	[SerializeField] 
	private Camera mainCamera;
	
	private Boolean canUseAbilities;
	
	private BaseEffect[] playerAbilities;

	[SerializeField] 
	private GameObject sprites;
	
	// Use this for initialization
	void Start () {
		playerAbilities = new BaseEffect[1];
		
		playerAbilities[0] = new AoESampleAbility(Instantiate(sprites, transform.position, sprites.transform.rotation), mainCamera);
		playerAbilities[0].start();
		playerAbilities[0].displayEffect();
	}
	
	// Update is called once per frame
	void Update () {
		playerAbilities[0].displayEffect();
	}
	
	
	
}
