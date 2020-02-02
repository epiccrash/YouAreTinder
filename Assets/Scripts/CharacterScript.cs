using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int HeightInInches { get; set; }
    public Dictionary<string, float> Preferences { get; set; }
    public ProfilePictureData profile;

    public List<string> bio { get; set;}
    public int StyleInt { get; set; }   // Writing style
    public bool Match { get; set; }     // Are they the match?

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
