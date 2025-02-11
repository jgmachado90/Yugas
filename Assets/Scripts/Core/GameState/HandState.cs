using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandState : IHandState
{
    private GameState gameState; 
    private int handLimit;

    IDeckState deck;

    private List<CardData> hand = new List<CardData>();

    public HandState(MatchData matchData, GameState gameState, IDeckState deck)
    {
        handLimit = matchData.handLimit;
        this.gameState = gameState;
        this.deck = deck;   
    }

    public int DrawCards()
    {
        int count = 0;
        while (hand.Count < handLimit)
        {
            hand.Add(deck.Pop());
            count++;
        }
        return count;
    }

    public CardData GetHandCardByIndex(int index)
    {
        return hand[index];
    }

}
