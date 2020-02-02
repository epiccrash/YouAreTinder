using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProfileCard : MonoBehaviour, IEventSystemHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image CardPortrait = null;
    public TextMeshProUGUI CardName = null;
    public TextMeshProUGUI CardDescription = null;
    public Image[] CardDarkBackings;
    public Image[] CardLightBackings;
    public bool active = false;
    public ScrollRect scrollbar = null;
    public CharacterScript character = null;
    public Image outline = null;
    public Image darken = null;
    bool mousein = false;
    public bool locked = false;
    public Texture2D lockedMouseTex = null;
    public Texture2D unlockedMouseTex = null;

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
        Color proxy = script.profile.LightColor;

        outline.enabled = false;
        character = script;
        CardName.text = script.Name +" \\ " + script.Age.ToString();
        proxy.a = 1;
        CardName.color = proxy;
        CardDescription.text = "";

        foreach( Image c in CardLightBackings)
        {
            c.color = proxy;
        }

        foreach (Image c in CardDarkBackings)
        {
            proxy = script.profile.DarkColor;
            proxy.a = 1;
            c.color = proxy;
        }

        CardPortrait.sprite = script.profile.PlayerIcon;

        foreach(string s in script.bio)
        {
            CardDescription.text += s + "\n";
        }
    }

    public void SetStartPoint(Vector2 position)
    {
        startPos = position;
        returnPos = startPos;
    }

    public void Update()
    { 
        if (dragging && !locked)
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
            if(locked && !Input.GetMouseButton(0))
            {
                dragging = false;
            }
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, returnPos, 0.1f);
        }

        if (active)
        {
            ProfileCardManager.Instance.OnCardPosChange(rect.anchoredPosition.x - startPos.x, Input.mousePosition.x - startMousePos.x);
            outline.enabled = mousein || dragging;
            outline.GetComponent<RectTransform>().localScale = new Vector3(1,1,1) * (dragging ? 1.01f : 1);
            GetComponent<RectTransform>().localScale = new Vector3(1,1,1) * (dragging ? 0.97f : 1);
        }

        if(active && dragging)
        {
            Cursor.SetCursor(locked ? lockedMouseTex : unlockedMouseTex, new Vector2(), CursorMode.Auto);
        }

        if(!Input.GetMouseButton(0))
        {
            Cursor.SetCursor(null, new Vector2(), CursorMode.Auto);
        }

        rect.rotation = Quaternion.Euler(0, 0, -(rect.anchoredPosition.x - startPos.x) / 250);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        dragPos = rect.anchoredPosition;
        startMousePos = Input.mousePosition;
        dragging = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mousein = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mousein = false;
    }
}
