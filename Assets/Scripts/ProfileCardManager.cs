using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileCardManager : UnitySingleton<ProfileCardManager>
{
    [SerializeField] private int SuitorProfilesCount = 5;
    [SerializeField] private ProfileCard DefaultProfileCard = null;
    [SerializeField] private RectTransform NewCardSpawnPoint = null;
    [SerializeField] private RectTransform NewCardWaitPoint = null;
    [SerializeField] private RectTransform NewCardTargetPoint = null;
    [SerializeField] private RectTransform SpawnPanel = null;
    [SerializeField] private RectTransform LockedInPanel = null;
    [SerializeField] private Image MatchHeart = null;
    [SerializeField] private float leftDragToDiscard = 0;
    [SerializeField] private float discardOffset = 0;
    [SerializeField] private float rightDragToMatch = 0;
    [SerializeField] private CharacterGenerator chargenerator = null;
    [SerializeField] private StoryGenerator storygenerator = null;
    private List<ProfileCard> ProfileList = null;
    private ProfileCard LockedProfile = null;
    private ProfileCard Match = null;
    private int CurrentDisplayCard;
    bool matched = false;

    IEnumerator OnFinishMatch()
    {
        yield return new WaitForSeconds(1.0f);
        MatchmakingState.Instance.compatibility = storygenerator.GenerateCompatability(LockedProfile.character,  ProfileList[CurrentDisplayCard].character);
        MatchmakingState.Instance.name1 = LockedProfile.character.Name.Replace(((char)13).ToString(), "");
        MatchmakingState.Instance.name2 = ProfileList[CurrentDisplayCard].character.Name.Replace(((char)13).ToString(), "");
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentDisplayCard = SuitorProfilesCount-1;

        LockedProfile = GenerateCard(LockedInPanel); 
        LockedProfile.SetStartPoint(new Vector2(0, 0));

        ProfileList = new List<ProfileCard>();

        int matchingCardNum = Random.Range(0, SuitorProfilesCount);

        for(int i = 0; i < SuitorProfilesCount; i++)
        {
            if (i == matchingCardNum)
            {
                ProfileList.Add(GenerateCardSpecial(SpawnPanel));
            } else
            {
                ProfileList.Add(GenerateCard(SpawnPanel));
            }
            if(i>0)ProfileList[i].scrollbar.value = 0;
        }
        
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
        card.GetComponent<RectTransform>().anchoredPosition = NewCardWaitPoint.anchoredPosition;
        card.SetStartPoint(NewCardWaitPoint.anchoredPosition);
        card.InitializeCard(chargenerator.Generate(null));
        return card;
    }

    ProfileCard GenerateCardSpecial(Transform Parent)
    {
        ProfileCard card = Instantiate(DefaultProfileCard, Vector3.zero, Quaternion.identity, Parent);
        card.GetComponent<RectTransform>().anchoredPosition = NewCardWaitPoint.anchoredPosition;
        card.SetStartPoint(NewCardWaitPoint.anchoredPosition);
        card.InitializeCard(chargenerator.Generate(LockedProfile.character));
        return card;
    }

    public void ShowNextCard()
    {
        CurrentDisplayCard = (CurrentDisplayCard+1) % ProfileList.Count;
        ProfileList[CurrentDisplayCard].active = true;
        ProfileList[CurrentDisplayCard].scrollbar.value = 1;
        ProfileList[CurrentDisplayCard].GetComponent<RectTransform>().anchoredPosition = NewCardSpawnPoint.anchoredPosition;
        ProfileList[CurrentDisplayCard].SetStartPoint(new Vector2(0,0));
        ProfileList[(CurrentDisplayCard+1) % ProfileList.Count].GetComponent<RectTransform>().anchoredPosition = NewCardWaitPoint.anchoredPosition;
        ProfileList[(CurrentDisplayCard+1) % ProfileList.Count].SetStartPoint(NewCardSpawnPoint.anchoredPosition);
        ProfileList[(CurrentDisplayCard+1) % ProfileList.Count].scrollbar.value = 0;
    }

    private void Update()
    {
        if(matched)
        {
            MatchHeart.GetComponent<RectTransform>().localScale = Vector3.Lerp(MatchHeart.GetComponent<RectTransform>().localScale, new Vector3(1,1,1) * 60,0.03f);
        }
    }
}
