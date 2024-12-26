using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    private int selectorIndex;

    private BattleManager battleManager;
    public Action<int> onSelectorMoved;

    public List<Transform> handCardPositions;

    private void Start()
    {
        battleManager = GameManager.Instance.battleManager;
    }

    public void MoveSelector(int value)
    {
        if (value < 0)
        {
            selectorIndex--;
            selectorIndex = selectorIndex < 0 ? battleManager.handLimit - 1 : selectorIndex;
        }
        else
        {
            selectorIndex++;
            selectorIndex = selectorIndex > battleManager.handLimit - 1 ? 0 : selectorIndex;
        }
        onSelectorMoved?.Invoke(selectorIndex);
    }

    public CardData GetSelectedCard()
    {
        return battleManager.GetCurrentHandCardByIndex(selectorIndex);
    }

    public int GetSelectorIndex()
    {
        return selectorIndex;
    }

    public Vector3 GetSelectedCardPosition()
    {
        return handCardPositions[selectorIndex].position;
    }

    public Vector3 GetCardPositionByIndex(int index)
    {
        return handCardPositions[index].position;
    }
}
