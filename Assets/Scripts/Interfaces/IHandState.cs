using System;

public interface IHandState
{
    public event Action<CardData, int> OnPullCard;
    public CardData GetHandCardByIndex(int index);
    public int GetHandCount();

    public int GetHandLimit();

    public void AddCardInHand(CardData card);

}
