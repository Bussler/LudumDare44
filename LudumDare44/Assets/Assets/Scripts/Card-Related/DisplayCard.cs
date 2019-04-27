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

    /*public enum EffectType //Link all the effects here
    {
        Nothing,
        Test
    }

    public List<Cardeffect> BEffects = new List<Cardeffect>();*/

    public int effectID;

    public void applyInfo()
    {

        title.text = card.cardName;
        desc.text = card.description;
        attack.text = card.attack.ToString();
        imageName = card.imageName;

        this.GetComponent<Draggable>().myType = card.myType;

        //set image
        Sprite image = Resources.Load<Sprite>("tcgcardspack/" + imageName);
        this.transform.Find("CardImage").GetComponent<Image>().sprite = image;

        effectID = card.effectID;
        //createEffects();

    }

    /*private void createEffects()
    {
        foreach (EffectType et in card.effects)
        {
            switch (et)
            {
                case EffectType.Test: BEffects.Add(new TestEffect());
                    break;

                default:
                    break;
            }
        }
    }

    public void applyEffects()
    {
        foreach(Cardeffect ce in BEffects)
        {
            ce.Apply();
        }
    }*/

    public int getAttack()
    {
        return card.attack;
    }

    public Card getCard()
    {
        return card;
    }


}
