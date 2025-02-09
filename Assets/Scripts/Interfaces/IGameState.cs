using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    DeckData GetDeckData();
    DeckData GetEnemyDeckData();

    MatchData GetMatchData();
}
