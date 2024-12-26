using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI playerCardCount;
    public TextMeshProUGUI AICardCount;

    private void Start()
    {
        GameManager.Instance.battleManager.onDrawCards += UpdateCardCount;
    }

    private void UpdateCardCount(BattleData data, int cards, Turn currentTurn)
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
