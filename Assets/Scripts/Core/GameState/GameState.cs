using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour, ISubsystem, IGameState
{
    private IGameData gameData;

    private IBoardState boardState;
    
    private IHandState playerHandState;
    private IHandState enemyHandState;

    private IDeckState playerDeckState;
    private IDeckState enemyDeckState;

    public int playerLifePoints;
    public int enemyLifePoints;


    public IDeckState GetDeckState(Owner owner)
    {
        return owner == Owner.Player ? playerDeckState : enemyDeckState;
    }

    public IHandState GetHandState(Owner owner) 
    {
        return owner == Owner.Player ? playerHandState : enemyHandState;
    }

    public IBoardState GetBoardState()
    {
        return boardState;
    }

    public void Initialize()
    {
        gameData = SubsystemLocator.GetSubsystem<GameData>();
        boardState = new BoardState();

        playerDeckState = new DeckState(gameData.GetDeckData().cards);
        enemyDeckState = new DeckState(gameData.GetEnemyDeckData().cards);

        playerHandState = new HandState(gameData.GetMatchData(), this, playerDeckState);
        enemyHandState = new HandState(gameData.GetMatchData(), this, enemyDeckState);
    }


    public void Shutdown()
    {
   
    }
}
