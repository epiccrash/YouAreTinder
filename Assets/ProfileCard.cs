using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProfileCard : MonoBehaviour, IEventSystemHandler, IPointerDownHandler
{
    public Image CardPortrait = null;
    public TextMeshProUGUI CardName = null;
    public TextMeshProUGUI CardDescription = null;
    public bool active = false;

    RectTransform rect;
    private bool dragging;
    private Vector2 dragPos;
    public Vector2 startPos;
    public Vector2 returnPos;
    private Vector2 startMousePos;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void InitializeCard(CharacterScript script)
    {
        CardName.text = script.Name +" \\ " + script.Age.ToString();
        CardDescription.text = "";
        foreach (KeyValuePair<string, float> k in script.Preferences)
        {
             CardDescription.text += $"My preference for {k.Key} is {k.Value}. \n";
        }

    }

    public void SetStartPoint(Vector2 position)
    {
        startPos = position;
        returnPos = startPos;
    }

    public void Update()
    { 
        if (dragging && active)
        {
            if (Input.GetMouseButton(0))
            {
                rect.anchoredPosition = new Vector2(Input.mousePosition.x - startMousePos.x, 0) + dragPos;

            }
            else
            {
                ProfileCardManager.Instance.OnCardFinishDragged(Input.mousePosition.x - startMousePos.x);
                dragging = false;
            }
        }
        else
        {
            dragging = false;
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, returnPos, 0.1f);
        }

        if (active)
        {
            ProfileCardManager.Instance.OnCardPosChange(rect.anchoredPosition.x - startPos.x);
        }
        rect.rotation = Quaternion.Euler(0, 0, -(rect.anchoredPosition.x - startPos.x) / 250);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        dragPos = rect.anchoredPosition;
        startMousePos = Input.mousePosition;
        dragging = true;
    }

}
