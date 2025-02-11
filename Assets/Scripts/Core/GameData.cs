using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour, ISubsystem, IGameData
{
    [SerializeField] private DeckData deckData;
    [SerializeField] private DeckData enemyDeck;
    [SerializeField] private MatchData matchData;


    public DeckData GetDeckData()
    {
        return deckData;
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
