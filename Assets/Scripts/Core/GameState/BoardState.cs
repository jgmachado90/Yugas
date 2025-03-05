using System;

public class BoardState : IBoardState
{
    private CardData[,] boardCards = new CardData[4, 5];

    public void PlayCard(CardData card, int colIndex, Owner whosTurn)
    {
        bool isCreature = card.cardType != CardType.TrapCard && card.cardType != CardType.SpellCard;

        int row = GetBoardRow(whosTurn, isCreature);

        boardCards[row, colIndex] = card;
    }

    public CardData GetCardInIndex(int row, int col)
    {
        if (row < 0 || row >= 4 || col < 0 || col >= 5)
            throw new IndexOutOfRangeException("Invalid board position.");

        return boardCards[row, col];
    }

    public void RemoveCard(int row, int col)
    {
        if (row < 0 || row >= 4 || col < 0 || col >= 5)
            throw new IndexOutOfRangeException("Invalid board position.");

        boardCards[row, col] = null;
    }


    private int GetBoardRow(Owner whosTurn, bool isCreature)
    {
        if (whosTurn == Owner.Player)
            return isCreature ? 1 : 0;
        else
            return isCreature ? 2 : 3;
    }
}
