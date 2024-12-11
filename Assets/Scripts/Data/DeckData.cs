using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckData", menuName = "ScriptableObjects/DeckData", order = 1)]
public class DeckData : ScriptableObject
{
    public List<CardData> cards;
}
