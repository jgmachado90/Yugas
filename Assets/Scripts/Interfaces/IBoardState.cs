public interface IBoardState
{
    public void PlayCard(CardData card, int colIndex, Owner whosTurn);
    public CardData GetCardInIndex(int row, int col);
    public void RemoveCard(int row, int col);

}
