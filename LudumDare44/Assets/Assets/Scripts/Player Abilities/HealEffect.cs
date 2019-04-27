using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : BaseEffect {

    private int HealAmount = 10;

    public HealEffect(int amt)
    {

    }

    public override void displayEffect()
    {
        Debug.Log("DisplayEffect Success");
    }

    public override void playEffect()
    {
        PlayerManager plM = GameObject.FindObjectOfType<PlayerManager>();
        if (plM != null)
        {
            plM.GetHealth(HealAmount);
            Debug.Log("Player healed");
        }
    }

    public override void start()
    {
        
    }

}
