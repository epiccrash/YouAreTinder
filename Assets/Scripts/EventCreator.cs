using System.Collections;
using System.Collections.Generic;
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

    // 0-indexed
    private int FindBreakupEvent() {
        float chanceBreakup = Mathf.Clamp(marriageThreshold - compatability, 0.0f, 1.0f);
        int i;
        for(i = 1; i < maxNumEvents; i++) {
            if (Random.Range(0.0f, 1.0f) <= chanceBreakup) {
                break;
            }
        }

        return i;
    }
    
    private string GetGoodFirstDate() {
        return p1Name + " and " + p2Name + " had a great first date!";
    }
    
    private string GetBadFirstDate() {
        return p1Name + " and " + p2Name + " had a bad first date ...";
    }

    private string GetGoodEvent() {
        return p1Name + " and " + p2Name + " had a great date!";
    }

    private string GetBadEvent() {
        return p1Name + " and " + p2Name + " had a bad date...";
    }

    // Start is called before the first frame update
    private void Start()
    {
        compatability = Mathf.Clamp(compatability, 0.0f, 1.0f);
        maxNumEvents = Mathf.Clamp(maxNumEvents, 2, int.MaxValue);
        int breakupEvent = FindBreakupEvent();
        List<string> events = new List<string>();
        
        // Generate 1st (0th) date special case
        if (breakupEvent == 1 || Random.Range(0.0f, 1.0f) > compatability) {
            events.Add(GetBadFirstDate());
        } else {
            events.Add(GetGoodFirstDate());
        }

        // Generate other dates
        for(int i = 1; i < breakupEvent - 2; i++) {
            float dateSuccessChance = compatability + Random.Range(-1.0f * jitter, jitter);
            if(Random.Range(0.0f, 1.0f) <= dateSuccessChance) {
                events.Add(GetGoodEvent());
            } else {
                events.Add(GetBadEvent());
            }
        }

        // Generate event before break-up (good or bad depending on final),
        // and generate final outcome
        if(compatability >= marriageThreshold) {
            events.Add(GetGoodEvent());
            events.Add(p1Name + " and " + p2Name + " got married!");
        } else {
            if (breakupEvent != 1) {
                events.Add(GetBadEvent());
            }
            events.Add(p1Name + " and " + p2Name + " broke up ...");
        }

        timeline.TimelineAnimation(events.ToArray());
    }
}
