using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

//This script is supposed to be attached to any UI object that should be dragged

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {//we haave to implement the INterfaces Beging, DragHandler and End in order to fire the specific events

    public Transform lastParent=null;//parent to return to; last legit drag

    GameObject placeholder = null;//gameobject to function as placeholder in the horizontal gui layout

    public Transform currentDropzone = null;//set on dragzone when entering a new draggable zone, so that the right gameobjects are shifted; currently hovering dropzone

    public Transform originalParent=null;

    public enum typeOfCard
    {
        Monster, //On enemy
        Spell, //on self
        SelfSpell
    };

    public typeOfCard myType=typeOfCard.Monster;

    public void OnBeginDrag(PointerEventData eventData)//the hand is a panel with a horizontal layout so that it orders the cards as children nicely. when we drag the card, we have to undo the child parent behaviour between card and hand
    {
        //create a placeholder so that the cards won't automaticly shift when the card is dragged out
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleHeight = 0;
        le.flexibleWidth = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        //grab the point to return to if invalid drop and delete the gameobject of the current hand layout
        lastParent = this.transform.parent;
        originalParent = this.transform.parent;
        currentDropzone = lastParent;
        this.transform.SetParent(this.transform.parent.parent);

        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false; //turn responsibility to raycasts off, so that we see what zone/other stuff is beneat the card

        if (this.GetComponent<DisplayCard>().effectID != -1)
            GameObject.Find("Player").GetComponent<PlayerManager>().DisplayEffect(this.GetComponent<DisplayCard>().effectID); //activate the display
    }


    public void OnDrag(PointerEventData eventData)
    {
        //move the card
        this.transform.position = eventData.position;//TODO the anchor point of the card can be different to the mouseclick, so it hops around. Solution: in begin drag save mouse/event pos and take note of difference between trans.pos and keep as offset

        //move cards when hovering over them with a card

        placeholder.transform.SetParent(currentDropzone);//the placeholder gameobject has to be a child of the current hovering dropzone
        int newSiblingIndex = currentDropzone.childCount;//default: last index

        for (int i=0;i<currentDropzone.childCount; i++)//check for each children in currently hovering dropzone
        {
            if (this.transform.position.x<currentDropzone.GetChild(i).position.x)//if the dragged card is left to the child, set the invisible placeholder to that place so that the card moves right
            {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }

        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(lastParent);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());

        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);

        //activate, destroy Effect Card
        if (lastParent!=originalParent)
        {
            Debug.Log(this.GetComponent<DisplayCard>().getAttack() + " on: " + lastParent.name);
            if(this.GetComponent<DisplayCard>().effectID != -1)
            GameObject.Find("Player").GetComponent<PlayerManager>().PlayEffect(this.GetComponent<DisplayCard>().effectID);

            DeckManager.addGraveyard(this.GetComponent<DisplayCard>().card);
            Destroy(this.gameObject);

        }
        
    }

}
