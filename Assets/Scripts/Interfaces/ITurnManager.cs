using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnManager
{
    Turn GetCurrentTurn();
    void NextTurn();
}
