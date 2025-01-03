using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHandController
{
    private HandController handController;

    private MatchManager matchManager;
    private SelectorManager selectorManager;
    private CardFactory cardFactory;

    DrawCardAnimator drawCardAnimator;

    public DrawHandController(HandController handController, DrawCardAnimator drawCardAnimator)
    {
        this.drawCardAnimator = drawCardAnimator;
        this.handController = handController;

        selectorManager = SubsystemLocator.GetSubsystem<SelectorManager>();
        matchManager = SubsystemLocator.GetSubsystem<MatchManager>();
        cardFactory = SubsystemLocator.GetSubsystem<CardFactory>();

        matchManager.onDrawCards += DrawCards;
    }


    public void OnDestroy()
    {
        matchManager.onDrawCards -= DrawCards;
    }
    private void DrawCards(MatchBattlerStatus battleData, int cardCount, Turn currentTurn)
    {
        int handLimit = matchManager.MatchData.handLimit;
        int cardsInHand = handLimit - cardCount;

        for (int i = 0; i < handLimit; i++)
        {
            GameObject handCardObject = cardFactory.CreateCard();
            handCardObject.transform.SetParent(handController.transform);
            handCardObject.GetComponent<RectTransform>().localScale = Vector3.one;  
            CardSetup(battleData, cardsInHand, i, handCardObject);

            if (i >= cardsInHand)
                drawCardAnimator.DrawCardAnimation(handCardObject, i);
        }
    }
    private void CardSetup(MatchBattlerStatus battleData, int cardsInHand, int i, GameObject handCardObject)
    {
        HandCard handCard = handCardObject.GetComponent<HandCard>();
        handCard.HandCardSetup(battleData.hand[i]);
        handController.handCards.Add(handCard);
        handCardObject.transform.position = selectorManager.GetCardPositionByIndex(i);
    }
}
