using System.Collections.Generic;

public class BattlerStatus : IBattlerStatus
{
    public int lifePoints;
    public int cardCount;
    public int handLimit;

    public Deck deck;

    public List<CardData> field = new List<CardData>();
    public List<CardData> hand = new List<CardData>();

    public BattlerStatus(MatchData matchData , DeckData _deck)
    {
        lifePoints = matchData.lifePoints;
        deck = new Deck(_deck.cards);
        cardCount = _deck.cards.Count;
        handLimit = matchData.handLimit;
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