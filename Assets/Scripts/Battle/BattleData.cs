using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData
{
    public int lifePoints;

    public int cardCount;

    public Deck deck;
    public List<CardData> field = new List<CardData>();
    public List<CardData> hand = new List<CardData>();

    public BattleData(int _lifePoints, DeckData _deck)
    {
        lifePoints = _lifePoints;
        deck = new Deck(_deck.cards);
        cardCount = deck.cards.Count;
    }

    public int DrawCards()
    {
        int count = 0;
        while(hand.Count < 5)
        {
            hand.Add(deck.Pop());
            count++;
        }
        return count;
    }
}
