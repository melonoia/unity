using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFruit : Level
{
    public int numMoves;
    public int targetScore;
    private int movesUsed = 0;
    // Start is called before the first frame update

    void Start()
    {
        type = LevelType.FRUIT;
        hud.SetLevelType(type);
        hud.SetScore(currentScore);
        hud.SetTarget(targetScore);
        hud.SetRemaining(numMoves);
    }

    public void GetFruits()
    {

    }
}
