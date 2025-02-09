using UnityEngine;

public interface ISelectorManager
{
    public void MoveSelector(int value);
    public CardData GetSelectedCard();

    public Vector3 GetCardPositionByIndex(int index);

    public int GetSelectorIndex();

    public Vector3 GetSelectedCardPosition();



}