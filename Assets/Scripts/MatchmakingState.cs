using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchmakingState : UnitySingletonPersistent<MatchmakingState>
{
    public float compatibility = 0;
    public string name1 = "";
    public string name2 = "";
<<<<<<< HEAD
    public Dictionary<string, int> sharedPreferences;
=======
    public Sprite profile1 = null;
    public Sprite profile2 = null;
    public Dictionary<string, float> sharedPreferences;
>>>>>>> accf35133de968edc9d899522b2993c0dadf73e9
    //string : preferene name
    //Float: -1 both dislike, 0 one like one dislike, 1 both like
}
