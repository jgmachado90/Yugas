using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MatchData", menuName = "ScriptableObjects/MatchData", order = 1)]
public class MatchData : ScriptableObject
{
    public int lifePoints;
    public int maxCards;
    public int handLimit;
}
