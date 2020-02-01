using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProfileCard : EventTrigger
{
    RectTransform rect;
    private bool dragging;
    private Vector2 dragPos;
    private Vector2 startPos;
    private Vector2 startMousePos;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
    }

    public void Update()
    {
        if (dragging && Input.GetMouseButton(0))
        {
            rect.anchoredPosition = new Vector2(Input.mousePosition.x - startMousePos.x,0) + dragPos;  
        }
        else
        {
            dragging = false;
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, startPos, 0.1f);
        }
        
        rect.rotation = Quaternion.Euler(0,0,-(rect.anchoredPosition.x - startPos.x)/250);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        dragPos = rect.anchoredPosition;
        startMousePos = Input.mousePosition;
        dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //dragging = false;
    }
}
