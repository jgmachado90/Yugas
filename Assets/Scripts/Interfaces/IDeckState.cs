public interface IDeckState
{
    public void Push(CardData card);
    public CardData Pop();
    public CardData Peek();

    public void Shuffle();

    public int GetCardCount();
}
