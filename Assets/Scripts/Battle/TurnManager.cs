using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Owner
{
    Player,
    AI
}


public class TurnManager : ITurnManager
{
    public Owner currentTurn = Owner.Player;


    public event Action<Owner> OnTurnChanged;

    public bool IsMyTurn(Owner owner)
    {
        return currentTurn == owner ? true : false;
    }   

    public Owner GetCurrentTurn()
    {
        return currentTurn;
    }

    public void NextTurn()
    {
        currentTurn = (currentTurn == Owner.Player) ? Owner.AI : Owner.Player;
        OnTurnChanged?.Invoke(currentTurn);
    }
}
