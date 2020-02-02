using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchmakingState : UnitySingletonPersistent<MatchmakingState>
{
    public float compatibility = 0;
    public string name1 = "";
    public string name2 = "";
}
