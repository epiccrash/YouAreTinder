using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int HeightInInches { get; set; }
    public Dictionary<string, float> Preferences { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PrintVariables()
    {
        print("Name: " + Name 
            + "; Age: " + Age 
            + "; Height: " + HeightInInches / 12 + "'" + HeightInInches % 12 + "\"");

        foreach (KeyValuePair<string, float> k in Preferences)
        {
            print("Preference: " + k);
        }
    }
}
