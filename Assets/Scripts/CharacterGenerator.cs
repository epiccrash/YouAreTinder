using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    private string prefsFilename = "preferences";
    private string humanMaleNames = "human_male_names";
    private string humanFemaleNames = "human_female_names";

    // Picture selection here

    [Header("Characterization Limits")]
    [SerializeField]
    private int min_age;
    [SerializeField]
    private int max_age;
    [SerializeField]
    private int min_height_in_inches;
    [SerializeField]
    private int max_height_in_inches;

    private TextAsset prefs;
    private string[] prefsList;

    private TextAsset male_names;
    private string[] male_names_list;

    private TextAsset female_names;
    private string[] female_names_list;

    [SerializeField]
    private GameObject characterTemplate;

    //is the next one the new target character
    private bool isTarget = true;
    private List<string> targetCharacterPrefsList = new List<string>(); 

    void Awake()
    {
        prefs = (TextAsset) Resources.Load(prefsFilename);
        prefsList = prefs.text.Split(","[0]);

        male_names = (TextAsset) Resources.Load(humanMaleNames);
        male_names_list = male_names.text.Split(","[0]);

        female_names = (TextAsset) Resources.Load(humanFemaleNames);
        female_names_list = female_names.text.Split(","[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            Generate();
        }
    }

    void resetTarget()
    {
        targetCharacterPrefsList.RemoveRange(0,targetCharacterPrefsList.Count);
        isTarget = true;
    }

    void Generate()
    {
        int count = 0;//The number of common preference with the target
        string[] nameList = Random.Range(0, 2) == 0 ? male_names_list : female_names_list;
        string name = nameList[Random.Range(0, nameList.Length)];

        int age = Random.Range(min_age, max_age + 1);
        int height = Random.Range(min_height_in_inches, max_height_in_inches + 1);

        Dictionary<string, float> prefsDict = new Dictionary<string, float>();
        string pref;
        int numLikes = Random.Range(1, 5);
        for (int i = 0; i < numLikes; i++)
        {
            if (!isTarget && count < 3)
            {
                count++;
                pref = targetCharacterPrefsList[Random.Range(0, targetCharacterPrefsList.Count)];
            }
            else
            {
                pref = prefsList[Random.Range(0, prefsList.Length)];
            }
            while (prefsDict.ContainsKey(pref))
            { 
                pref = prefsList[Random.Range(0, prefsList.Length)];

            }
            float severity = Random.Range(1, 11) * 0.1f;
            if (isTarget)
            {
                targetCharacterPrefsList.Add(pref);
            }
            prefsDict.Add(pref, severity);
        }

        int numDislikes = Random.Range(1, 5);
        for (int i = 0; i < numDislikes; i++)
        {
            if (!isTarget && count < 3)
            {
                count++;
                pref = targetCharacterPrefsList[Random.Range(0, targetCharacterPrefsList.Count)];
            }
            else
            {
                pref = prefsList[Random.Range(0, prefsList.Length)];
            }
            while (prefsDict.ContainsKey(pref))
            {
                pref = prefsList[Random.Range(0, prefsList.Length)];
            }
            float severity = Random.Range(-10, -1) * 0.1f;
            if (isTarget)
            {
                targetCharacterPrefsList.Add(pref);
                isTarget = false;
            }
            prefsDict.Add(pref, severity);
        }

        GameObject character = Instantiate(characterTemplate);
        character.GetComponent<CharacterScript>().Name = name;
        character.GetComponent<CharacterScript>().Age = age;
        character.GetComponent<CharacterScript>().HeightInInches = height;
        character.GetComponent<CharacterScript>().Preferences = prefsDict;
        character.GetComponent<CharacterScript>().PrintVariables();
    }
}
