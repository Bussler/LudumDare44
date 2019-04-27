using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DisplayCard : MonoBehaviour
{//script to apply the stored info in the card scriptablee obj

    public Card card;

    public Text title;
    public string imageName;
    public Text desc;
    public Text attack;

    public enum EffectType
    {
        Nothing,
        Draw,
        Discard,
        DiscardDraw,
        Skip
    }

    //public List<BattlecryEffect> BEffects = new List<BattlecryEffect>();
    //public List<DiscardEffect> DEffects = new List<DiscardEffect>();

    public void applyInfo()
    {

        title.text = card.cardName;
        desc.text = card.description;
        attack.text = card.attack.ToString();
        imageName = card.imageName;

        this.GetComponent<Draggable>().myType = card.myType;

        //set image
        Sprite image = Resources.Load<Sprite>("tcgcardspack/" + imageName);
        this.transform.FindChild("CardImage").GetComponent<Image>().sprite = image;

    }

    public int getAttack()
    {
        return card.attack;
    }

    public Card getCard()
    {
        return card;
    }


}
