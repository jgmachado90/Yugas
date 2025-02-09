using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class BattleHud : MonoBehaviour
{
    MatchManager battleManager;

    public TextMeshProUGUI playerCardCount;
    public TextMeshProUGUI AICardCount;

    private void Start()
    {
        battleManager = SubsystemLocator.GetSubsystem<MatchManager>();
        battleManager.onDrawCards += UpdateCardCount;
    }

    private void UpdateCardCount(IBattlerStatus data, int cards, Turn currentTurn)
    {
        switch (currentTurn)
        {
            case Turn.Player:
            UpdateCardCountAnimation(playerCardCount, cards);
            break;
            case Turn.AI:
            UpdateCardCountAnimation(AICardCount, cards);
            break;
        }
    }

    private void UpdateCardCountAnimation(TextMeshProUGUI cardCountText, int numCards)
    {
        int cardCount;
        int.TryParse(cardCountText.text,out cardCount);
        cardCount -= numCards;
        cardCountText.text = cardCount.ToString();
    }
}
