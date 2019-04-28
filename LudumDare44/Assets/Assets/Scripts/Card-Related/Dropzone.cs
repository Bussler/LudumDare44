using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Dropzone : MonoBehaviour,  IDropHandler, IPointerEnterHandler, IPointerExitHandler{//IPointerHandler to test if a mouse is entering the object zone

    public Draggable.typeOfCard myDropType = Draggable.typeOfCard.Monster;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Dropped");
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();// pointerDrag is the dragged object
        if (d!=null&&myDropType==d.myType)
        {
            d.lastParent = this.transform;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //glow if valid
        //Debug.Log("Enter");
        //when entering a valid dropzone change the placeholderparent of the card to this obj, so that the cards can move away
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();// pointerDrag is the dragged object
        if (d != null && myDropType == d.myType)
        {
            d.currentDropzone = this.transform;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //turn glow off, apply effect
        //Debug.Log("Exit");
        //when exiting the valid dropzone
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();// pointerDrag is the dragged object
        if (d != null && myDropType == d.myType&&d.currentDropzone==this.transform)
        {
            d.currentDropzone = d.lastParent;
        }
    }
}
