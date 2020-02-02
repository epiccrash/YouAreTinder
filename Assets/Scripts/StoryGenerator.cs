using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGenerator : MonoBehaviour
{

    private Dictionary<string, float> c1Preferences;
    private Dictionary<string, float> c2Preferences;

    public float GenerateCompatability(CharacterScript c1, CharacterScript c2)
    {
        c1Preferences = c1.Preferences;
        c2Preferences = c2.Preferences;

        float result = Random.Range(6, 9)*0.1f;
        float temp = 0.0f;
        int count = 1;
        foreach (KeyValuePair<string, float> k in c1Preferences)
        {

            //If there are common preference 
            if (c2Preferences.ContainsKey(k.Key))
            {
                count += 1;
                temp += Mathf.Abs(c2Preferences[k.Key] - k.Value)*0.3f;
                //0-2,the higher the abs value, the less compatable
                Debug.Log(c2Preferences[k.Key]+" " + " "+ k.Value);
            }
        }
        Debug.Log(temp / count);
        result -= temp / count;
        Debug.Log(result);
        return result;
    }
}
