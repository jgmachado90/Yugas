using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public enum Turn
{
    Player,
    AI
}

public class BattleManager : MonoBehaviour
{
    private StateMachineManager stateMachineManager;

    private BattleData playerBattleData;
    private BattleData enemyBattleData;

    public Turn currentTurn = Turn.Player;

    public DeckData enemyDeck;

    public int handLimit = 5;

    public Action<BattleData, int, Turn> onDrawCards;
    public Action<CardData> onSelectCard;

    private void Awake()
    {
        stateMachineManager = GameManager.Instance.stateMachineManager;
        Debug.Log("BattleManagerSubscribe");
        stateMachineManager.OnMatchInitialize += InitializeBattle;
        stateMachineManager.OnDrawPhaseInitialize += Draw;
    }

    private void OnDestroy()
    {
        if (stateMachineManager == null) return;
        stateMachineManager.OnMatchInitialize -= InitializeBattle;
    }

    private void InitializeBattle(Action complete)
    {
        Debug.Log("Initializing Battle");
        DeckData playerDeck = GameManager.Instance.playerState.DeckData;
        playerBattleData = new BattleData(8000, playerDeck);
        enemyBattleData = new BattleData(8000, enemyDeck);
        complete?.Invoke();
    }

    private void Draw(Action complete)
    {
        Debug.Log("DrawPhase");
        switch (currentTurn)
        {
            case Turn.Player:
            DrawCards(playerBattleData, complete);
            break;
            case Turn.AI:
            DrawCards(enemyBattleData, complete);
            break;
        }
    }

    private void DrawCards(BattleData battleData, Action complete)
    {
        int drawCount = battleData.DrawCards();
        onDrawCards.Invoke(battleData, drawCount, currentTurn);
        complete?.Invoke();
    }

    public CardData GetCurrentHandCardByIndex(int index)
    {
        if(currentTurn == Turn.Player)
        {
            return playerBattleData.hand[index];
        }
        return enemyBattleData.hand[index];
    }


    private void PrintCards(List<CardData> cards)
    {
        foreach(var card in cards)
        {
            Debug.Log(card.cardName);
        }
    
    }
}
