using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioGenerator : MonoBehaviour
{
    //TODO:need to be init
    Dictionary<string, List<List<string>>> database = new Dictionary<string, List<List<string>>>();
    //key : preference name, 
    //Value : index 0 -high, index 1-low index 3- possibility neture? 
    //The list within the list, index 0,1,2 -writing style 


public List<string> GenerateBio(Dictionary<string,float> prefsDict)
    {
        List<string> bioList = new List<string>();
        int styleInt = Random.Range(0, 3);//writing style
        foreach (KeyValuePair<string, float> k in prefsDict)
        {
            //determined the high low value of this preference 
            int level;
            if(k.Value >= 0.5)
            {
                level = 0; //high
            } else if (k.Value <= -0.5)
            {
                level = 1;
            }
            else
            {   //if we have average options, comment me out
                //level = 2;
                continue;
            }
            bioList.Add(database[k.Key][level][styleInt]);
        }
        return bioList;
    }

   
}
