using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarButton : Button
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable())
        {
            return;
        }
        RectTransform buttonRect = GetComponent<RectTransform>();
        buttonRect.localPosition = new Vector3(buttonRect.localPosition.x, buttonRect.localPosition.y - 2, buttonRect.localPosition.z);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!IsInteractable())
        {
            return;
        }
        RectTransform buttonRect = GetComponent<RectTransform>();
        buttonRect.localPosition = new Vector3(buttonRect.localPosition.x, buttonRect.localPosition.y + 2, buttonRect.localPosition.z);
        base.OnPointerUp(eventData);
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        if (state == SelectionState.Disabled)
        {
            Color buttonColor = image.color;
            buttonColor.a = 0.5f;
            image.color = buttonColor;
        }

        if (state == SelectionState.Normal)
        {
            Color buttonColor = image.color;
            buttonColor.a = 1f;
            image.color = buttonColor;
        }
        base.DoStateTransition(state, instant);
    }
}
