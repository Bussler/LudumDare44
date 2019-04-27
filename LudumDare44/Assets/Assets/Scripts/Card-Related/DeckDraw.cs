using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DeckDraw : MonoBehaviour, IPointerClickHandler
{

    public GameObject playerhand;

    public GameObject cardPrefab;

    public void OnPointerClick(PointerEventData eventData){
        Card curCard = GameObject.Find("DeckManager").GetComponent<DeckManager>().drawCard(0);
        spawnCard(curCard, playerhand.transform);
    }

    public void spawnCard(Card c, Transform hand){
        GameObject card = Instantiate(cardPrefab) as GameObject;
        card.GetComponent<DisplayCard>().card = c;
        card.GetComponent<DisplayCard>().applyInfo();
        card.transform.SetParent(playerhand.transform);
        card.transform.localScale = new Vector3(1f, 1 , 1);
    }
}
