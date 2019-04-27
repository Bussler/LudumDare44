using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : Cardeffect {

    private int HealAmount=10;
 
	void Start () {
		
	}

    public override void Apply()
    {
        Debug.Log("Success Effect activation");
        PlayerManager plM = GameObject.FindObjectOfType<PlayerManager>();
        if(plM != null)
        {
            plM.GetHealth(HealAmount);
            Debug.Log("Player healed");
        }
    }
}
