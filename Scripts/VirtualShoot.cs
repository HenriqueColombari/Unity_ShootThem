using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualShoot : MonoBehaviour, IPointerDownHandler
{
    Animator animCtrlChild;
    RectTransform rectParent;
    RectTransform rectChild;

    Vector2 input = Vector2.zero;

    [SerializeField]
    private GameObject Ship;

    void Start()
    {
        rectParent = GetComponent<RectTransform>();
        
    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        Ship.GetComponent<ShipController>().setInputShoot(true);
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
