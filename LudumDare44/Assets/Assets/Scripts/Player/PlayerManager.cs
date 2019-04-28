using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour{

	[SerializeField]
	private int playerHealth, playerDamage;
	private int playerCurrentHealth;
	[SerializeField]
	private float effectTimer;

	[SerializeField] 
	private Camera mainCamera;
	
	private bool canUseAbilities, timeIsFreezed;
	
	
	private BaseEffect[] playerAbilities;
	
	private bool playEffect = false;
	
	[SerializeField] 
	private GameObject sprites, sprites2, DmgAoe, HealAoe, FireBall, ShieldAoe;

    [SerializeField]
    private GameObject myCanvas;
    
	
	

	// Use this for initialization
	void Start () {
	
		playerCurrentHealth = playerHealth;
		
		playerAbilities = new BaseEffect[8];
		
		cards = new Animator[3];
		
		//playerAbilities[0] = new AoESampleAbility(Instantiate(sprites, transform.position, sprites.transform.rotation), mainCamera, Instantiate(sprites2, transform.position, sprites.transform.rotation));

        playerAbilities[1] = new HealEffect(10);
        playerAbilities[2] = new AOEDamage(DmgAoe, mainCamera);
        playerAbilities[3] = new AOEDamage(HealAoe, mainCamera);
        playerAbilities[4] = new FireBallSpawn(FireBall, mainCamera, this.transform, 1);
        playerAbilities[5] = new FireBallSpawn(FireBall, mainCamera, this.transform, 2);
        playerAbilities[6] = new FireBallSpawn(FireBall, mainCamera, this.transform, 3);
        playerAbilities[7] = new AOEDamage(ShieldAoe, mainCamera);


        for (int i = 0; i < 3; i++){
	        cards[i] = shopCards[i].GetComponent<Animator>();
        }
        //battleWon();

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
            myCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

	private void FixedUpdate(){
		if (effectTimer < 1000){
			effectTimer++;
        }
        else
        {
            if (effectTimer>=1000 && !timeIsFreezed)
            {
                timeIsFreezed = true; //TODO actually freeze time
                Time.timeScale = 0;

                myCanvas.gameObject.SetActive(true);//enable card canvas
                GameObject.Find("DeckManager").GetComponent<DeckManager>().handleHand(); //redraw cards

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

	//called from enemey, when its health is = 0
	public void battleWon(){
		
		playShopFadeIn();
	} 
	
	
	//---------------------------------------------------------------------
	//						   		CardShop
	//---------------------------------------------------------------------	

	[SerializeField]
	private GameObject[] shopCards;
	
	private Animator[] cards;
	
	[SerializeField]
	private Animator shop, notEnoughHealth;

	[SerializeField] private TextMeshProUGUI healthText;
	
	private string remHealth = "Remaining Health: ";
	
	//fades th whole sop in
	private void playShopFadeIn(){
		shop.SetTrigger("PlayFadeIn");
		healthText.text = remHealth + playerCurrentHealth;
	}

	//fades the whole shop out
	private void playShopFadeOut(){
		shop.SetTrigger("PlayFadeOut");
	}

	//called when a player clicks a card
	public void playCardBuy(int cardIndex){
		//attack values is the cost value for this card
		int cardCost = int.Parse(shopCards[cardIndex].GetComponent<DisplayCard>().attack.text);
		//can the player buy teh card?
		if (playerCurrentHealth >= cardCost){
			//yes -> buy card
			cards[cardIndex].SetTrigger("PlayBuyAnimation");	//buy animation
			playerCurrentHealth -= cardCost;						//update health
			healthText.text = remHealth + playerCurrentHealth;		//update text
			//TODO Add the card at the index to the deck			
		}else{
			//no -> display a text stating that not enough health is available
			notEnoughHealth.SetTrigger("NotEnoughHealth");
		}
	}

	//triggers the state machine reset for the cards
	private void BuySceneEnded(){
		for (int i = 0; i < 3; i++){
			cards[i].SetTrigger("BuySceneEnded");
		}
	}

	//called by continue button
	public void continueButton(){
		playShopFadeOut();
		BuySceneEnded();
	}
	
}
