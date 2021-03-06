﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGenerator : MonoBehaviour
{

    private Dictionary<string, float> c1Preferences;
    private Dictionary<string, float> c2Preferences;
    private List<string> sharedPreferences;
    public Dictionary<string, int> generateSharedPreferences()
    {
        Dictionary<string, int> result = new Dictionary<string, int>();
        //Have to called generate compatability before generateSharedPreferences
        foreach (string s in sharedPreferences)
        {
            if(c1Preferences[s] * c2Preferences[s] < 0)
            {
                result[s] = 0; // different prefernces
            } else if (c1Preferences[s] < 0)
            {
                result[s] = -1; //Both dislike
            }
            else
            {
                result[s] = 1; // both like
            }
        }
        return result;
    }
    public float GenerateCompatability(CharacterScript c1, CharacterScript c2)
    {
        c1Preferences = c1.Preferences;
        c2Preferences = c2.Preferences;
        sharedPreferences = new List<string>();
        float result = Random.Range(5, 9)*0.1f;
        float temp = 0.0f;
        int count = 1;
        foreach (KeyValuePair<string, float> k in c1Preferences)
        {

            //If there are common preference 
            if (c2Preferences.ContainsKey(k.Key))
            {
                count += 1;
                // Now creates bias based on how much a person cares about something
                if ((k.Value > 0.5f && c2Preferences[k.Key] > 0.5f) ||
                    (k.Value < -0.5f && c2Preferences[k.Key] < -0.5f) ||
                    (k.Value > 0.5f && c2Preferences[k.Key] < -0.5f) ||
                    (k.Value < -0.5f && c2Preferences[k.Key] > 0.5f))
                {
                    temp += Mathf.Abs(c2Preferences[k.Key] - k.Value) * 0.3f;
                } else if (k.Value == c2Preferences[k.Key]) {
                    temp += 0.12f;
                } else
                {
                    temp += Mathf.Abs(c2Preferences[k.Key] - k.Value) * 0.1f;
                }
                //0-2,the higher the abs value, the less compatable
                sharedPreferences.Add(k.Key);
            }
        }
      
        result -= temp / count;

        // Age gap modifiers
        int ageGap = Mathf.Abs(c1.Age - c2.Age);
        if (ageGap >= 5)
        {
            result -= 0.01f * Mathf.Abs(c1.Age - c2.Age);
        }

        result = Mathf.Clamp(result, 0, 1);

        print(result);

        if (c2.Match)
        {
            return Mathf.Max(result, 0.75f);
        }
        return result;
    }
}
