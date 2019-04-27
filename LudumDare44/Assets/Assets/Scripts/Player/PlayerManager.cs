using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour{

	[SerializeField]
	private int playerHealth, playerDamage;
	[SerializeField]
	private float effectTimer;

	private Boolean canUseAbilities;
	
	[SerializeField]
	private BaseEffect[] gameEffects;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	
}
