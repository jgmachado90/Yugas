using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameData
{
    DeckData GetDeckData();
    DeckData GetEnemyDeckData();

    MatchData GetMatchData();
}
