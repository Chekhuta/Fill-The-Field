using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class WindowButton : Button
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable)
        {
            return;
        }
        RectTransform[] elementsRect = GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < elementsRect.Length; i++)
        {
            elementsRect[i].localPosition = new Vector3(elementsRect[i].localPosition.x, elementsRect[i].localPosition.y - 4, elementsRect[i].localPosition.z);
        }
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable)
        {
            return;
        }
        RectTransform[] elementsRect = GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < elementsRect.Length; i++)
        {
            elementsRect[i].localPosition = new Vector3(elementsRect[i].localPosition.x, elementsRect[i].localPosition.y + 4, elementsRect[i].localPosition.z);
        }
        base.OnPointerUp(eventData);
    }
}
