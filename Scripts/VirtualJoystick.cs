using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    Animator animCtrlChild;
    RectTransform rectParent;
    RectTransform rectChild;

    Vector2 input = Vector2.zero;

    void Start()
    {
        rectParent = GetComponent<RectTransform>();
        animCtrlChild = transform.GetChild(0).GetComponent<Animator>();
        rectChild = animCtrlChild.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointRect;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (rectParent, eventData.position, eventData.pressEventCamera, out pointRect);

        input.x = pointRect.x / (rectParent.sizeDelta.x / 2f);
        input.y = pointRect.y / (rectParent.sizeDelta.y / 2f);

        input = input.magnitude > 1f ? input.normalized : input;

        rectChild.anchoredPosition = new Vector2(input.x * (rectParent.sizeDelta.x / 2.5f),
                                                    input.y * (rectParent.sizeDelta.x / 2.5f));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        animCtrlChild.SetBool("pressed", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        rectChild.anchoredPosition = Vector2.zero;
        animCtrlChild.SetBool("pressed", false);
    }

    public float GetHorizontal()
    {
        return input.x;
    }

    public float GetVertical()
    {
        return input.y;
    }

    public Vector2 GetInput()
    {
        return input;
    }
}
