﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileCardManager : UnitySingleton<ProfileCardManager>
{
    [SerializeField] private int SuitorProfilesCount = 5;
    [SerializeField] private ProfileCard DefaultProfileCard = null;
    [SerializeField] private RectTransform NewCardSpawnPoint = null;
    [SerializeField] private RectTransform NewCardTargetPoint = null;
    [SerializeField] private RectTransform SpawnPanel = null;
    [SerializeField] private RectTransform LockedInPanel = null;
    [SerializeField] private Image MatchHeart = null;
    [SerializeField] private float leftDragToDiscard = 0;
    [SerializeField] private float discardOffset = 0;
    [SerializeField] private float rightDragToMatch = 0;
    private List<ProfileCard> ProfileList = null;
    private ProfileCard LockedProfile = null;
    private int CurrentDisplayCard;
    bool matched = false;

    IEnumerator OnFinishMatch()
    {
        yield return new WaitForSeconds(1.0f);
        // HANDLE UPDATING PERSISTENT SINGLETON AND MATCHING HERE
        Debug.Log("haha now change");
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentDisplayCard = SuitorProfilesCount;
        ProfileList = new List<ProfileCard>();
        for(int i = 0; i < SuitorProfilesCount; i++)
        {
            ProfileList.Add(GenerateCard(SpawnPanel));
        }
        LockedProfile = GenerateCard(LockedInPanel);
        LockedProfile.SetStartPoint(new Vector2(0,0));
        ShowNextCard();
    }

    public void OnCardPosChange(float dragDistance)
    {
        if(dragDistance > 0 && !matched)
        {
            MatchHeart.GetComponent<RectTransform>().localScale = new Vector3(1,1,1) * dragDistance/100;
            MatchHeart.color = new Color (1,1,1, dragDistance / rightDragToMatch);
        }
    }

    public void OnCardFinishDragged(float dragDistance)
    {
        if (dragDistance < -leftDragToDiscard)
        {
            ProfileList[CurrentDisplayCard].returnPos = ProfileList[CurrentDisplayCard].startPos - new Vector2(discardOffset, 0);
            ShowNextCard();
        }
        else if (dragDistance > rightDragToMatch)
        {
            matched = true;
            StartCoroutine(OnFinishMatch());
        }
    }

    ProfileCard GenerateCard(Transform Parent)
    {
        ProfileCard card = Instantiate(DefaultProfileCard, Vector3.zero, Quaternion.identity, Parent);
        card.GetComponent<RectTransform>().anchoredPosition = NewCardSpawnPoint.anchoredPosition;
        card.SetStartPoint(NewCardSpawnPoint.anchoredPosition);
        return card;
    }

    public void ShowNextCard()
    {
        CurrentDisplayCard = (CurrentDisplayCard+1) % ProfileList.Count;
        ProfileList[CurrentDisplayCard].active = true;
        ProfileList[CurrentDisplayCard].GetComponent<RectTransform>().anchoredPosition = NewCardSpawnPoint.anchoredPosition;
        ProfileList[CurrentDisplayCard].SetStartPoint(new Vector2(0,0));
    }

    private void Update()
    {
        if(matched)
        {
            MatchHeart.GetComponent<RectTransform>().localScale = Vector3.Lerp(MatchHeart.GetComponent<RectTransform>().localScale, new Vector3(1,1,1) * 60,0.03f);
        }
    }
}
