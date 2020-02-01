using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject character1;
    [SerializeField]
    private GameObject character2;

    private Dictionary<string, float> c1Preferences;
    private Dictionary<string, float> c2Preferences;

    private void init()
    {
        c1Preferences = character1.GetComponent<CharacterScript>().Preferences;
        c2Preferences = character1.GetComponent<CharacterScript>().Preferences;
    }

    public float GenerateCompatability()
    {
        init();
        float result = Random.Range(6, 11)*0.1f;
        float temp = 0f;
        int count = 1;
        foreach (KeyValuePair<string, float> k in c1Preferences)
        {
            //If there are common preference 
            if (c2Preferences.ContainsKey(k.Key))
            {
                count += 1;
                temp += Mathf.Abs(c2Preferences[k.Key] - k.Value)*0.3f;
                //0-2,the higher the abs value, the less compatable
            }
        }
        result -= temp / count;
        return result;
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
