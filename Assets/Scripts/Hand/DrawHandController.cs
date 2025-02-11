using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHandController
{
    private HandController handController;

    private MatchManager matchManager;
    private SelectorManager selectorManager;
    private CardFactory cardFactory;
    private IGameData gameData;

    DrawCardAnimator drawCardAnimator;

    public DrawHandController(HandController handController, DrawCardAnimator drawCardAnimator)
    {
        this.drawCardAnimator = drawCardAnimator;
        this.handController = handController;

        selectorManager = SubsystemLocator.GetSubsystem<SelectorManager>();
        matchManager = SubsystemLocator.GetSubsystem<MatchManager>();
        cardFactory = SubsystemLocator.GetSubsystem<CardFactory>();
        gameData = SubsystemLocator.GetSubsystem<GameData>(); 

        matchManager.onDrawCards += DrawCards;
    }


    public void OnDestroy()
    {
        matchManager.onDrawCards -= DrawCards;
    }
    private void DrawCards(IHandState handState, int cardCount, Turn currentTurn)
    {
        int handLimit = gameData.GetMatchData().handLimit;
        int cardsInHand = handLimit - cardCount;

        for (int i = 0; i < handLimit; i++)
        {
            ICard createdCard = cardFactory.CreateCard(CardFactoryType.HandCard);
            MonoBehaviour cardMonobehaviour = createdCard as MonoBehaviour;
            GameObject handCardObject = cardMonobehaviour.gameObject;
            handCardObject.transform.SetParent(handController.transform);
            handCardObject.GetComponent<RectTransform>().localScale = Vector3.one;  
            CardSetup(handState, cardsInHand, i, handCardObject);

            if (i >= cardsInHand)
                drawCardAnimator.DrawCardAnimation(handCardObject, i);
        }
    }
    private void CardSetup(IHandState handState, int cardsInHand, int i, GameObject handCardObject)
    {
        HandCard handCard = handCardObject.GetComponent<HandCard>();
        handCard.HandCardSetup(handState.GetHandCardByIndex(i));
        handController.handCards.Add(handCard);
        handCardObject.transform.position = selectorManager.GetCardPositionByIndex(i);
    }
}
