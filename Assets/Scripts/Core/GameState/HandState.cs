using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandState : IHandState
{
    private GameState gameState; 
    private int handLimit;

    IDeckState deck;

    public event Action<CardData, int> OnPullCard;

    private List<CardData> hand = new List<CardData>();

    public HandState(MatchData matchData, GameState gameState, IDeckState deck)
    {
        handLimit = matchData.handLimit;
        this.gameState = gameState;
        this.deck = deck;   
    }

    public CardData GetHandCardByIndex(int index)
    {
        return hand[index];
    }

    public int GetHandCount()
    {
        return hand.Count; 
    }

    public int GetHandLimit()
    { 
        return handLimit;
    }

    public void AddCardInHand(CardData card)
    {
        hand.Add(card);
        OnPullCard?.Invoke(card, hand.Count-1);
    }

}
