using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckState : IDeckState
{
    public List<CardData> cards = new List<CardData>();

    public DeckState(List<CardData> _cards)
    {
        cards = new List<CardData>(_cards);
    }

    public void Push(CardData card)
    {
        cards.Add(card); // Adiciona no final da lista
    }

    public CardData Pop()
    {
        if (cards.Count == 0)
        {
            Debug.LogError("Deck is empty! Cannot pop a card.");
            return null;
        }

        CardData topCard = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1); // Remove o último elemento
        return topCard;
    }

    public CardData Peek()
    {
        if (cards.Count == 0)
        {
            Debug.LogError("Deck is empty! Cannot peek.");
            return null;
        }

        return cards[cards.Count - 1];
    }

    public int GetCardCount() {
        return cards.Count;
    } 

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = Random.Range(i, cards.Count);
            (cards[i], cards[randomIndex]) = (cards[randomIndex], cards[i]);
        }
    }
}
