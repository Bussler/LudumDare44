using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {

    public List<Card> PlayerDeck;

    private List<Card> OriginalDeck;

    public static List<Card> Graveyard=new List<Card>();

    private void Start()
    {
        OriginalDeck = new List<Card>(PlayerDeck);
        shuffle(PlayerDeck);
    }

    public Card drawCard(int i)
    {
        Card curCard = PlayerDeck[i];
        PlayerDeck.RemoveAt(i);
        if (PlayerDeck.Count <= 0)
        {
            resetDeck();
        }
        return curCard;
    }

    public static void addGraveyard(Card card)
    {
        Graveyard.Add(card);
    }

    public void shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

    }

    public void refillDeck()
    {
        PlayerDeck = new List<Card>(Graveyard);
        shuffle(PlayerDeck);//shuffle
        Graveyard.Clear();
    }

    public void resetDeck()
    {
        PlayerDeck = new List<Card>(OriginalDeck);
        shuffle(PlayerDeck);//shuffle
    }

}
