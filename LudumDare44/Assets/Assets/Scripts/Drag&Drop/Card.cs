using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "CreateCard")]
public class Card : ScriptableObject
{ //we want to store the information of the card in a scriptable object, so we only have to attach this to the prefab at runtime

    public string cardName;
    public string description;
    public Draggable.typeOfCard myType;


    //TODO link a effect script here

    public int attack;

    public string imageName = "Tex_Adventurer";

    public void printInfo()
    {
        Debug.Log(cardName + ": " + description + " attack: " + attack);
    }

}
