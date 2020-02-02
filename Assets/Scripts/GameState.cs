using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : UnitySingletonPersistent<GameState>
{
    public int pointsToWin = 4;
    public int currentPoints = 0;
}
