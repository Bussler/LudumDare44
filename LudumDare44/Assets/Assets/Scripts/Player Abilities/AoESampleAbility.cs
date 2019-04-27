using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AoESampleAbility : BaseEffect {

	public AoESampleAbility(GameObject sprite, Camera camera){
		effectDisplaySprite = sprite;
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
		Debug.Log("Coords:" + mousePosition.x + " " + mousePosition.z);
		spriteTransform.position = mousePosition; //new Vector3(mousePosition.x, 2f, mousePosition.z);
	}

	public override void playEffect(){
		throw new System.NotImplementedException();
	}
}
