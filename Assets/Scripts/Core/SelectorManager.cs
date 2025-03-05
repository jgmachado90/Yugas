using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour, ISubsystem, ISelectorManager
{
    private int selectorIndex;

    private IGameData gameData;
    private MatchManager matchManager;

    public Action<int> onSelectorMoved;

    public List<Transform> handCardPositions;

    private void Start()
    {
        gameData = SubsystemLocator.GetSubsystem<GameData>();
        matchManager = SubsystemLocator.GetSubsystem<MatchManager>();
    }

    public void MoveSelector(int value)
    {
        if (value < 0)
        {
            selectorIndex--;
            selectorIndex = selectorIndex < 0 ? gameData.GetMatchData().handLimit - 1 : selectorIndex;
        }
        else
        {
            selectorIndex++;
            selectorIndex = selectorIndex > gameData.GetMatchData().handLimit - 1 ? 0 : selectorIndex;
        }
        onSelectorMoved?.Invoke(selectorIndex);
    }

    public CardData GetSelectedCard()
    {
        IHandController handController = matchManager.GetCurrentHandController();
        return handController.GetHandCardByIndex(selectorIndex);
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

    public void Initialize()
    {
    }

    public void Shutdown()
    {
    }
}
