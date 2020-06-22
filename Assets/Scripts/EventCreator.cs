using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EventCreator : MonoBehaviour
{
    // TODO: grab information from the singleton
    // TODO: grab event types from spreadsheet
    public int maxNumEvents;
    public float marriageThreshold;
    public float jitter;
    public TimelineUI timeline;

    private string p1Name = "";
    private string p2Name = "";
    private float compatibility = 0.0f;
    private float adjustedCompatibility = 0.0f;

    private string datesFilename = "dates";
    private string badOutcomesFilename = "bad_outcomes";
    private string mehOutcomesFilename = "meh_outcomes";
    private string goodOutcomesFilename = "good_outcomes";
    private string greatOutcomesFilename = "great_outcomes";
    private string premarriageFilename = "premarriage_events";
    private string marriageFilename = "marriage_events";
    private string goodBreakupFilename = "good_breakup_flavor";
    private string mediumBreakupFilename = "medium_breakup_flavor";
    private string badBreakupFilename = "bad_breakup_flavor";
    private string breakupImmediateFilename = "breakup_immediate_flavor";

    private int dateIncreMax = 7;
    private float greatDateThreshold = 0.65f;
    private float badDateThreshold = 0.25f;
    private float bias = 0.1f;

    // Used to weigh dates more towards expected outcomes after getting
    // unexpected date outcomes.
    private float waterfallBias = 0.1f;

    private string[] datesText;
    private string[] badOutcomesText;
    private string[] mehOutcomesText;
    private string[] goodOutcomesText;
    private string[] greatOutcomesText;
    private string[] premarriageText;
    private string[] marriageText;
    private string[] goodBreakupText;
    private string[] mediumBreakupText;
    private string[] badBreakupText;
    private string[] breakupImmediateText;

    private Dictionary<string, int> sharedPreferences;
    private List<string> matchedPreferences;
    private List<string> unMatchedPreferences;
    private List<string> events;
    private List<bool> eventOutcomes;

    private string[] numberAdjectives = { "first", "second", "third", "fourth", "fifth", "sixth", "seventh", 
                                          "eighth", "ninth", "tenth", "eleventh", "twelfth", "thirteenth", "fourteenth",
                                          "fifteenth", "sixteenth", "seventeenth", "eighteenth", "nineteenth", "twentieth"};

    private string GetDateDescriptor(int dateNum = 0, string descp = null) {
        string text;
        if (descp == null)
        {  
            text = datesText[Mathf.Clamp((int)Random.Range(0, datesText.Length), 0, datesText.Length - 1)];
        }
        else
        {
            text = descp;
        }

        StringBuilder textB = new StringBuilder(text);
        textB.Replace("*", p1Name);
        textB.Replace("^", p2Name);
        textB.Replace("#!", numberAdjectives[dateNum - 1]);
        return textB.ToString().Replace('\n', ' ');
    }

    private string ConvertDateNumToString(int dateNum)
    {
        string dateNumString = "";

        return dateNumString;
    }
    
    private string GetGoodEvent(int dateNum, float dateSuccessChance) {
        string outcome = "";

        if(dateSuccessChance >= greatDateThreshold) {
            outcome = greatOutcomesText[Mathf.Clamp((int)Random.Range(0, greatOutcomesText.Length - 1), 0, greatOutcomesText.Length - 1)];
        } else {
            outcome = goodOutcomesText[Mathf.Clamp((int)Random.Range(0, goodOutcomesText.Length - 1), 0, goodOutcomesText.Length - 1)];
        }
        if(matchedPreferences.Count != 0)
        {
            print(matchedPreferences[0]);
            string result = PrefBasedEvents.Instance.PrefBasedEventsandOutcomes[matchedPreferences[0].Trim()][0][0];
            matchedPreferences.RemoveAt(0);
            return GetDateDescriptor(dateNum,result) + " " + outcome;
        }
        else
        {
            return GetDateDescriptor(dateNum) + " " + outcome;
        }

    }

    private string GetBadEvent(int dateNum, float dateSuccessChance) {
        string outcome = "";

        if(dateSuccessChance >= badDateThreshold) {
            outcome = mehOutcomesText[Mathf.Clamp((int)Random.Range(0, mehOutcomesText.Length), 0, mehOutcomesText.Length - 1)];
        } else {
            outcome = badOutcomesText[Mathf.Clamp((int)Random.Range(0, badOutcomesText.Length), 0, badOutcomesText.Length - 1)];
        }

        if (unMatchedPreferences.Count != 0)
        {
            print(unMatchedPreferences[0]);
            string result = PrefBasedEvents.Instance.PrefBasedEventsandOutcomes[unMatchedPreferences[0].Trim()][1][0];
            unMatchedPreferences.RemoveAt(0);
            return GetDateDescriptor(dateNum, result) + " " + outcome;
        }

        return GetDateDescriptor(dateNum) + " " + outcome;
    }

    private string GetPremarriageEvent() {
        string premarriage = premarriageText[Mathf.Clamp((int)Random.Range(0, premarriageText.Length), 0, premarriageText.Length - 1)];
        StringBuilder textB = new StringBuilder(premarriage);
        textB.Replace("*", p1Name);
        textB.Replace("^", p2Name);
        premarriage = textB.ToString().Replace('\n', ' ');

        return premarriage;
    }

    private string GetMarriageEvent() {
        string marriage = marriageText[Mathf.Clamp((int)Random.Range(0, marriageText.Length), 0, marriageText.Length - 1)];
        StringBuilder textB = new StringBuilder(marriage);
        textB.Replace("*", p1Name);
        textB.Replace("^", p2Name);
        marriage = textB.ToString().Replace('\n', ' ');

        return marriage;
    }

    private string GetBreakupEvent() {
        string outcome = "";
        
        if(adjustedCompatibility <= badDateThreshold) {
            outcome = badBreakupText[Mathf.Clamp((int)Random.Range(0, badBreakupText.Length), 0, badBreakupText.Length - 1)];
        } else if (adjustedCompatibility <= greatDateThreshold) {
            outcome = mediumBreakupText[Mathf.Clamp((int)Random.Range(0, mediumBreakupText.Length), 0, mediumBreakupText.Length - 1)];
        } else {
            outcome = goodBreakupText[Mathf.Clamp((int)Random.Range(0, goodBreakupText.Length), 0, goodBreakupText.Length - 1)];
        }

        return p1Name + " and " + p2Name + " broke up. " + outcome;
    }

    private string GetBreakupImmediateEvent() {
        string outcome = breakupImmediateText[Mathf.Clamp((int)Random.Range(0, breakupImmediateText.Length), 0, breakupImmediateText.Length - 1)];
       
        return p1Name + " and " + p2Name + " didn't see each other again. " + outcome;
    }

    // 0-indexed
    private int FindEndingEvent(int minNumEvents, float weight) {
        float chanceEnd = Mathf.Clamp(Mathf.Abs(marriageThreshold - adjustedCompatibility) + weight, 0.0f, 1.0f);

        int i;
        for(i = minNumEvents - 1; i < maxNumEvents; i++) {
            if(Random.Range(0.0f, 1.0f) <= chanceEnd) {
                break;
            }
        }

        return i;
    }

    private bool AddDateEvent(int dateNum, float comp, float weight)
    {
        // Purposefully add some spice of life once in a while
        bool flip = (Random.Range(0.0f, 1.0f) <= waterfallBias);

        float dateSuccessChance = comp + Random.Range(-1.0f * jitter, jitter) + bias + weight;

        if(Random.Range(0.0f, 1.0f) <= dateSuccessChance && !flip) {
            events.Add(GetGoodEvent(dateNum, dateSuccessChance));
            eventOutcomes.Add(true);
            return true;
        } else {
            events.Add(GetBadEvent(dateNum, dateSuccessChance));
            eventOutcomes.Add(false);
            return false;
        }
    }

    private void GenerateDates()
    {
        bool willMarry = (compatibility >= marriageThreshold);
        int weightSign = willMarry ? 1 : -1;
        int dateNum = 1;
        int dateIncreMin = willMarry ? 5 : 1;
        bool hadGoodDate = false;

        // Generate the first date to use as a basis for the rest of the relationship
        float weight = weightSign * waterfallBias;
        bool firstDateSuccess = AddDateEvent(dateNum, compatibility, weight);

        // Find how long we want this relationship to last
        // If first date was bad & we expect a break-up, we'll make the relationship shorter
        // If the first date was bad & we expect marriage, we'll want time for them to make up
        // Otherwise, we want at least 3 dates for the arc to feel natural
        int endEvent;
        if(!willMarry && !firstDateSuccess) {
            endEvent = FindEndingEvent(2, 2.0f * waterfallBias);
        } else if(willMarry && !firstDateSuccess) {
            endEvent = FindEndingEvent(4, -2.0f * waterfallBias);
        } else {
            endEvent = FindEndingEvent(3, -2.0f * waterfallBias);
        }
        
        // Generate rest of dates
        // Reset the weight to match the waterfallBias pattern
        hadGoodDate = hadGoodDate || firstDateSuccess;
        weight = firstDateSuccess ^ willMarry ? weightSign * waterfallBias : 0.0f;
        dateNum += (int) Random.Range(dateIncreMin, dateIncreMax);

        // Generate all but last date & ending event(s)
        // Since we want the last date & ending event(s) to match in outcome
        for(int i = 1; i < endEvent - 2; i++) {
            // Make sure we had at least one good date if they're getting married!!
            bool result;
            if(!hadGoodDate && i == endEvent - 3)
            {
                events.Add(GetGoodEvent(dateNum, 1));
                eventOutcomes.Add(true);
                result = true;
            } else
            {
                result = AddDateEvent(dateNum, adjustedCompatibility, weight);
            }

            hadGoodDate = hadGoodDate || result;
            weight = result ^ willMarry ? weight + (weightSign * waterfallBias) : -0.5f * weightSign * waterfallBias;
            dateNum += (int) Random.Range(dateIncreMin, dateIncreMax);
        }

        // Generate event before break-up (good or bad depending on final),
        // and generate final outcome
        if(willMarry) {
            events.Add(GetPremarriageEvent());
            eventOutcomes.Add(true);
            events.Add(GetMarriageEvent());
            eventOutcomes.Add(true);
        } else {
            if (endEvent > 1) {
                events.Add(GetBadEvent(dateNum, 0));
                eventOutcomes.Add(false);

                events.Add(GetBreakupEvent());
                eventOutcomes.Add(false);
            } else {
                events.Add(GetBreakupImmediateEvent());
                eventOutcomes.Add(false);
            }
        }

        timeline.TimelineAnimation(events.ToArray(), eventOutcomes.ToArray());
    }

    private float AdjustCompatability() {
        // Pull randomness-used compatability to be more mid-ranged if it's very high or low
        // Makes for more interesting story-arcs :0
        if(Mathf.Abs(marriageThreshold - compatibility) > waterfallBias * 0.75f) {
            float adjustment = waterfallBias * 0.25f;
            if(compatibility < marriageThreshold) {
                adjustment *= -1.0f;
            }
            return compatibility - adjustment;
        }
        return compatibility;
    }

    // Start is called before the first frame update
    private void Start()
    {
        matchedPreferences = new List<string>();
        unMatchedPreferences = new List<string>();
        p1Name = MatchmakingState.Instance.name1;
        p2Name = MatchmakingState.Instance.name2;
        compatibility = Mathf.Clamp(MatchmakingState.Instance.compatibility, 0.0f, 1.0f);
        sharedPreferences = MatchmakingState.Instance.sharedPreferences;
        datesText = ((TextAsset) Resources.Load(datesFilename)).text.Split(","[0]);
        badOutcomesText = ((TextAsset) Resources.Load(badOutcomesFilename)).text.Split(","[0]);
        mehOutcomesText = ((TextAsset) Resources.Load(mehOutcomesFilename)).text.Split(","[0]);
        goodOutcomesText = ((TextAsset) Resources.Load(goodOutcomesFilename)).text.Split(","[0]);
        greatOutcomesText = ((TextAsset) Resources.Load(greatOutcomesFilename)).text.Split(","[0]);
        premarriageText = ((TextAsset) Resources.Load(premarriageFilename)).text.Split(","[0]);
        marriageText = ((TextAsset) Resources.Load(marriageFilename)).text.Split(","[0]);
        goodBreakupText = ((TextAsset) Resources.Load(goodBreakupFilename)).text.Split(","[0]);
        mediumBreakupText = ((TextAsset) Resources.Load(mediumBreakupFilename)).text.Split(","[0]);
        badBreakupText = ((TextAsset) Resources.Load(badBreakupFilename)).text.Split(","[0]);
        breakupImmediateText = ((TextAsset) Resources.Load(breakupImmediateFilename)).text.Split(","[0]);

        adjustedCompatibility = Mathf.Clamp(AdjustCompatability(), 0.0f, 1.0f);
        maxNumEvents = Mathf.Clamp(maxNumEvents, 2, int.MaxValue);
        foreach(KeyValuePair<string, int> k in sharedPreferences)
        {
            if(k.Value == 1)
            {
                matchedPreferences.Add(k.Key);
            } else if (k.Value == 0) {
                unMatchedPreferences.Add(k.Key);
            }
        }

        events = new List<string>();
        eventOutcomes = new List<bool>();

        GenerateDates();
    }
}
