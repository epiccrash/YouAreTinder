using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EventCreator : MonoBehaviour
{
    // TODO: grab information from the singleton
    // TODO: grab event types from spreadsheet
    public int maxNumEvents;
    public float compatability;
    public float marriageThreshold;
    public float jitter;
    public string p1Name;
    public string p2Name;
    public TimelineUI timeline;

    private string datesFilename = "dates";
    private string badOutcomesFilename = "bad_outcomes";
    private string mehOutcomesFilename = "meh_outcomes";
    private string goodOutcomesFilename = "good_outcomes";
    private string greatOutcomesFilename = "great_outcomes";
    private string goodBreakupFilename = "good_breakup_flavor";
    private string mediumBreakupFilename = "medium_breakup_flavor";
    private string badBreakupFilename = "bad_breakup_flavor";

    private int dateIncre = 10;
    private float greatDateThreshold = 0.7f;
    private float badDateThreshold = 0.2f;

    private string[] datesText;
    private string[] badOutcomesText;
    private string[] mehOutcomesText;
    private string[] goodOutcomesText;
    private string[] greatOutcomesText;
    private string[] goodBreakupText;
    private string[] mediumBreakupText;
    private string[] badBreakupText;

    private string GetDateDescriptor(int dateNum) {
        string text = datesText[Mathf.Clamp((int)Random.Range(0, datesText.Length), 0, datesText.Length - 1)];
        StringBuilder textB = new StringBuilder(text);
        textB.Replace("*", p1Name);
        textB.Replace("^", p2Name);
        textB.Replace("!", dateNum.ToString());
        Debug.Log(textB);
        return textB.ToString().Replace('\n', ' ');
    }
    
    private string GetGoodEvent(int dateNum, float dateSuccessChance) {
        string outcome = "";

        if(dateSuccessChance >= greatDateThreshold) {
            outcome = greatOutcomesText[Mathf.Clamp((int)Random.Range(0, greatOutcomesText.Length), 0, greatOutcomesText.Length - 1)];
        } else {
            outcome = goodOutcomesText[Mathf.Clamp((int)Random.Range(0, goodOutcomesText.Length), 0, goodOutcomesText.Length - 1)];
        }
        Debug.Log(outcome);

        return GetDateDescriptor(dateNum) + " " + outcome;
    }

    private string GetBadEvent(int dateNum, float dateSuccessChance) {
        string outcome = "";

        if(dateSuccessChance >= badDateThreshold) {
            outcome = mehOutcomesText[Mathf.Clamp((int)Random.Range(0, mehOutcomesText.Length), 0, mehOutcomesText.Length - 1)];
        } else {
            outcome = badOutcomesText[Mathf.Clamp((int)Random.Range(0, badOutcomesText.Length), 0, badOutcomesText.Length - 1)];
        }
        Debug.Log(outcome);

        return GetDateDescriptor(dateNum) + " " + outcome;
    }

    private string GetBreakupEvent() {
        string outcome = "";
        
        if(compatability <= badDateThreshold) {
            outcome = badBreakupText[Mathf.Clamp((int)Random.Range(0, badBreakupText.Length), 0, badBreakupText.Length - 1)];
        } else if (compatability <= greatDateThreshold) {
            outcome = mediumBreakupText[Mathf.Clamp((int)Random.Range(0, mediumBreakupText.Length), 0, mediumBreakupText.Length - 1)];
        } else {
            outcome = goodBreakupText[Mathf.Clamp((int)Random.Range(0, goodBreakupText.Length), 0, goodBreakupText.Length - 1)];
        }

        return p1Name + " and " + p2Name + " broke up. " + outcome;
    }

    // 0-indexed
    private int FindEndingEvent() {
        if(compatability < marriageThreshold) {
            float chanceBreakup = Mathf.Clamp(marriageThreshold - compatability, 0.0f, 1.0f);
            int i;
            for(i = 1; i < maxNumEvents; i++) {
                if (Random.Range(0.0f, 1.0f) <= chanceBreakup) {
                    break;
                }
            }
            return i;
        } else {
            float chanceMarriage = Mathf.Clamp(compatability - marriageThreshold, 0.0f, 1.0f);
            int i;
            for(i = 1; i < maxNumEvents; i++) {
                if (Random.Range(0.0f, 1.0f) <= chanceMarriage) {
                    break;
                }
            }
            return i;
        }
    }

    private void GenerateDates()
    {
        int endEvent = FindEndingEvent();
        List<string> events = new List<string>();
        
        int dateNum = 1;
        // Generate 1st (0th) date special case
        if ((endEvent == 1 && compatability < marriageThreshold) || Random.Range(0.0f, 1.0f) > compatability) {
            float dateSuccessChance = compatability + Random.Range(-1.0f * jitter, jitter);
            events.Add(GetBadEvent(dateNum, dateSuccessChance));
        } else {
            float dateSuccessChance = compatability + Random.Range(-1.0f * jitter, jitter);
            events.Add(GetGoodEvent(dateNum, dateSuccessChance));
        }
        dateNum += (int) Random.Range(1, dateIncre);

        // Generate other dates
        for(int i = 1; i < endEvent - 2; i++) {
            float dateSuccessChance = compatability + Random.Range(-1.0f * jitter, jitter);
            if(Random.Range(0.0f, 1.0f) <= dateSuccessChance) {
                events.Add(GetGoodEvent(dateNum, dateSuccessChance));
            } else {
                events.Add(GetBadEvent(dateNum, dateSuccessChance));
            }

            dateNum += (int) Random.Range(1, dateIncre);
        }

        // Generate event before break-up (good or bad depending on final),
        // and generate final outcome
        if(compatability >= marriageThreshold) {
            events.Add(GetGoodEvent(dateNum, 1));
            events.Add(p1Name + " and " + p2Name + " got married!");
        } else {
            if (endEvent != 1) {
                events.Add(GetBadEvent(dateNum, 0));
            }
            events.Add(GetBreakupEvent());
        }

        timeline.TimelineAnimation(events.ToArray());
    }

    // Start is called before the first frame update
    private void Start()
    {
        datesText = ((TextAsset) Resources.Load(datesFilename)).text.Split(","[0]);
        badOutcomesText = ((TextAsset) Resources.Load(badOutcomesFilename)).text.Split(","[0]);
        mehOutcomesText = ((TextAsset) Resources.Load(mehOutcomesFilename)).text.Split(","[0]);
        goodOutcomesText = ((TextAsset) Resources.Load(goodOutcomesFilename)).text.Split(","[0]);
        greatOutcomesText = ((TextAsset) Resources.Load(greatOutcomesFilename)).text.Split(","[0]);
        goodBreakupText = ((TextAsset) Resources.Load(goodBreakupFilename)).text.Split(","[0]);
        mediumBreakupText = ((TextAsset) Resources.Load(mediumBreakupFilename)).text.Split(","[0]);
        badBreakupText = ((TextAsset) Resources.Load(badBreakupFilename)).text.Split(","[0]);

        compatability = Mathf.Clamp(compatability, 0.0f, 1.0f);
        maxNumEvents = Mathf.Clamp(maxNumEvents, 2, int.MaxValue);
        
        GenerateDates();
    }
}
