using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
	//animator for possible animations
	[SerializeField]
	private Animator animator;

	//sprite which displays how the ability works (e.g. for AoE the place it lands)
	[SerializeField] 
	private Sprite effectDisplaySprite;
	
	public abstract void displayEffect();
	public abstract void playEffect();

	
}
