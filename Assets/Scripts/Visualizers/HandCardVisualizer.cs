using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardVisualizer : MonoBehaviour
{
    public Owner owner;

    private HandCardCreator handCardCreator;

    private ITurnManager turnManager;
    private IHandState handState;
    private IDeckState deckState;

    [SerializeField] private DropHandCardsAnimator dropHandCardsAnimator;

    public DrawCardAnimator drawCardAnimator;

    private void Start()
    {
        handCardCreator = new HandCardCreator(transform, drawCardAnimator);
        IMatchManager matchManager = SubsystemLocator.GetSubsystem<IMatchManager>();
        turnManager = matchManager.GetTurnManager();    
        IGameState gameState = SubsystemLocator.GetSubsystem<IGameState>();
        handState = gameState.GetHandState(owner);
        deckState = gameState.GetDeckState(owner);
        turnManager.OnTurnChanged += ChangeTurn;
        handState.OnPullCard += DrawCard;
    }

    private void ChangeTurn(Owner owner)
    {
        this.owner = owner; 
    }

    private void OnDestroy()
    {
        handState.OnPullCard -= DrawCard;
        turnManager.OnTurnChanged -= ChangeTurn;
    }

    private void DrawCard(CardData card, int index)
    {
        GameObject cardObject = handCardCreator.CreateAndSetupCard(card, index);
        drawCardAnimator.DrawCardAnimation(cardObject,index);
    }
}
