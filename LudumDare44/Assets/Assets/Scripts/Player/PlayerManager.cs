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
    private float LimitGougeTimer;

	private float effectTimer;

	private Movement playerMovementScript;
	
	[SerializeField] 
	private Camera mainCamera;
	
	private bool canUseAbilities, timeIsFreezed;
	
	
	private BaseEffect[] playerAbilities;
	
	private bool playEffect = false;
	
	[SerializeField] 
	private GameObject sprites, sprites2, DmgAoe, HealAoe, FireBall, ShieldAoe, StandardFireBall;

    [SerializeField]
    private GameObject myCanvas;

    [SerializeField] 
    private DeckManager deckManager;

    [SerializeField]
    private RectTransform healthBar;
	

	// Use this for initialization
	void Start () {
		
		playerCurrentHealth = playerHealth;

        TakeDamage(30);

		
		playerAbilities = new BaseEffect[8];
		
		cards = new Animator[3];

        playerAbilities[0] = new FireBallSpawn(StandardFireBall, mainCamera, this.transform, 1);

        playerAbilities[1] = new HealEffect(10);
        playerAbilities[2] = new AOEDamage(DmgAoe, mainCamera);
        playerAbilities[3] = new AOEDamage(HealAoe, mainCamera);
        playerAbilities[4] = new FireBallSpawn(FireBall, mainCamera, this.transform, 1);
        playerAbilities[5] = new FireBallSpawn(FireBall, mainCamera, this.transform, 2);
        playerAbilities[6] = new FireBallSpawn(FireBall, mainCamera, this.transform, 3);
        playerAbilities[7] = new AOEDamage(ShieldAoe, mainCamera);


        playerMovementScript = GetComponent<Movement>();

        for (int i = 0; i < 3; i++){
	        cards[i] = shopCards[i].GetComponent<Animator>();
	        displayCardScriptShopCards[i] = shopCards[i].GetComponent<DisplayCard>();
	        draggableShopCards[i] = shopCards[i].GetComponent<Draggable>();
        }
		//battleWon();
    }
	
	// Update is called once per frame
	void Update () {
		
		if (playerCurrentHealth <= 0){
			Die();
		}
		//Debug.Log("Playerhealth: " + playerCurrentHealth);
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
		if (effectTimer < LimitGougeTimer)
        {
			effectTimer++;
        }
        else
        {
            if (effectTimer>= LimitGougeTimer && !timeIsFreezed)
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
        healthBar.sizeDelta=new Vector2(playerCurrentHealth, healthBar.sizeDelta.y);
	}

	public void increaseHealth(int amount){
		playerCurrentHealth += amount;
		if (playerCurrentHealth > playerHealth)
			playerCurrentHealth = playerHealth;
        healthBar.sizeDelta = new Vector2(playerCurrentHealth, healthBar.sizeDelta.y);
    }

	public void Die()
	{
		//GameOver
	}

	//called from enemey, when its health is = 0
	public void battleWon(){
		victorySound.Play();
		playShopFadeIn();
	} 
	
	
	//---------------------------------------------------------------------
	//						   		CardShop
	//---------------------------------------------------------------------	

	[SerializeField]
	private AudioSource victorySound;
	
	[SerializeField]
	private GameObject[] shopCards;
	
	private DisplayCard[] displayCardScriptShopCards = new DisplayCard[3];  
	private Animator[] cards;
	
	[SerializeField]
	private Animator shop, notEnoughHealth;

	[SerializeField] private TextMeshProUGUI healthText;
	private Draggable[] draggableShopCards = new Draggable[3];
	
	private string remHealth = "Remaining Health: ";
	
	//fades th whole sop in
	private void playShopFadeIn(){
		playerMovementScript.enabled = false;
		Card[] chosenPoolCards = deckManager.get3RandomShopCards();
		for (int i = 0; i < 3; i++){
			draggableShopCards[i].enabled = true;
			displayCardScriptShopCards[i].card = chosenPoolCards[i];
			displayCardScriptShopCards[i].applyInfo();
			draggableShopCards[i].enabled = false;
		}
		
		shop.SetTrigger("PlayFadeIn");
		healthText.text = remHealth + playerCurrentHealth;
	}

	//fades the whole shop out
	private void playShopFadeOut(){
		shop.SetTrigger("PlayFadeOut");
		resetLevel();
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
			deckManager.addCardToDeck(displayCardScriptShopCards[cardIndex].card);
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
	
	//---------------------------------------------------------------------
	//						   		Level Change
	//---------------------------------------------------------------------	

	[SerializeField]
	private Animator fadeOutAnimator;

	private void resetLevel(){
		fadeOutAnimator.SetTrigger("PlayFadeOut");
		resetPlayer();
		//chooseNewBoss
		//resetLevel
	//	fadeOutAnimator.SetTrigger("PlayFadeIn");
		playerMovementScript.enabled = true;
	}

	private void resetPlayer(){
		playerCurrentHealth = playerHealth;
		transform.position = new Vector3(0, 1, 0);
		playerMovementScript.reset();
	}
	
	
	
}
