using System;

public interface IDeckState
{
    public event Action<CardData> OnPopCard;

    public void Push(CardData card);
    public CardData Pop();
    public CardData Peek();

    public void Shuffle();

    public int GetCardCount();
}
