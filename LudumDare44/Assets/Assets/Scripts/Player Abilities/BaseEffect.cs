using System.Collections;
 using System.Collections.Generic;
   using UnityEngine;
       
       public abstract class BaseEffect: MonoBehaviour{
       	//animator for possible animations
       	[SerializeField]
       	protected Animator animator;
       
       	protected Vector3 mousePosition;
       	protected Camera mainCamera;
       	
       	//sprite which displays how the ability works (e.g. for AoE the place it lands)
       	[SerializeField] 
       	protected GameObject effectDisplaySprite, effectPlaySprite;
       
        
       	protected SpriteRenderer spritePrefab;
       	protected Transform spriteTransform;
       	
       	public abstract void displayEffect();
       	public abstract void playEffect();
       
       	public abstract void start();
		
       }