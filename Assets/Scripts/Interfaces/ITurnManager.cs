using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnManager
{
    public event Action<Owner> OnTurnChanged;

    bool IsMyTurn(Owner owner);
    Owner GetCurrentTurn();
    void NextTurn();
}
