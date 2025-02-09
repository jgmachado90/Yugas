using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Turn
{
    Player,
    AI
}


public class TurnManager : ITurnManager
{
    public Turn currentTurn = Turn.Player;

    public Turn GetCurrentTurn()
    {
        return currentTurn;
    }

    public void NextTurn()
    {
        currentTurn = (currentTurn == Turn.Player) ? Turn.AI : Turn.Player;
    }
}
