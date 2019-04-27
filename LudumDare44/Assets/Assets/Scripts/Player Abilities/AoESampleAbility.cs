using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AoESampleAbility : BaseEffect {

	public AoESampleAbility(GameObject sprite, Camera camera, GameObject sprite2){
		effectDisplaySprite = sprite;
		effectPlaySprite = sprite2;
		mainCamera = camera;
	}
	
	
	public override void start(){
		spritePrefab = effectDisplaySprite.GetComponent<SpriteRenderer>();
		mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		spriteTransform = effectDisplaySprite.GetComponent<Transform>();
	}

	public override void displayEffect(){
		spritePrefab.enabled = true;
		Vector2 m = Input.mousePosition;
		mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(m.x, m.y, 10));
		spriteTransform.position = mousePosition;
	}

	public override void playEffect(){
		spritePrefab.enabled = false;
		spritePrefab = effectPlaySprite.GetComponent<SpriteRenderer>();
		spriteTransform = effectPlaySprite.GetComponent<Transform>();
		spritePrefab.enabled = true;
		spriteTransform.Translate((mousePosition-spriteTransform.position)*Time.deltaTime * 1f);
	}
	
	
}
