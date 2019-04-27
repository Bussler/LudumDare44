using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DeckDraw : MonoBehaviour, IPointerClickHandler
{

    public Transform playerhand;

    public GameObject cardPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Haha");
        Card curCard = GameObject.Find("DeckManager").GetComponent<DeckManager>().drawCard(0);
        spawnCard(curCard, playerhand);


    }

    public void spawnCard(Card c, Transform hand)
    {
        GameObject card = Instantiate(cardPrefab) as GameObject;
        card.GetComponent<DisplayCard>().card = c;
        card.GetComponent<DisplayCard>().applyInfo();

        card.transform.SetParent(playerhand);

    }

}
