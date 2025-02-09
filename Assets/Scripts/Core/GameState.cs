using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour, ISubsystem, IGameState
{
    [SerializeField] private DeckData DeckData;
    [SerializeField] private DeckData enemyDeck;
    [SerializeField] private MatchData matchData;

    public DeckData GetDeckData()
    {
        return DeckData;
    }

    public DeckData GetEnemyDeckData()
    {
        return enemyDeck;
    }

    public MatchData GetMatchData()
    { 
        return matchData;
    }


    public void Initialize()
    {
    }

    public void Shutdown()
    {
    }
}
