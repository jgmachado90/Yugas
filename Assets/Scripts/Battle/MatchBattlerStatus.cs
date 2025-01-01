using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchBattlerStatus
{
    public int lifePoints;

    public int cardCount;
    public int handLimit;

    public Deck deck;
    public List<CardData> field = new List<CardData>();
    public List<CardData> hand = new List<CardData>();

    public MatchBattlerStatus(int _lifePoints, int _handLimit, DeckData _deck)
    {
        lifePoints = _lifePoints;
        deck = new Deck(_deck.cards);
        cardCount = deck.cards.Count;
        handLimit = _handLimit; 
    }

    public int DrawCards()
    {
        int count = 0;
        while(hand.Count < handLimit)
        {
            hand.Add(deck.Pop());
            count++;
        }
        return count;
    }
}
