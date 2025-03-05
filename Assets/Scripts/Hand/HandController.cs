using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class HandController : IHandController
{
    private IMatchManager matchManager;
    private ITurnManager turnManager;

    private IStateMachineManager stateMachineManager;


    private IDeckState deckState;
    private IHandState handState;

    public Owner owner;

    public Action<List<HandCard>> onHandFusion;

    public HandController(MatchManager matchManager, Owner owner)
    {
        this.matchManager = matchManager;
        this.owner = owner;
        deckState = SubsystemLocator.GetSubsystem<IGameState>().GetDeckState(owner);
        handState = SubsystemLocator.GetSubsystem<IGameState>().GetHandState(owner);
        stateMachineManager = SubsystemLocator.GetSubsystem<StateMachineManager>();
        turnManager = matchManager.GetTurnManager();
        stateMachineManager.OnDrawPhaseInitialize += DrawCards;
    }

    public void Shutdown(){
        stateMachineManager.OnDrawPhaseInitialize -= DrawCards;
    }

    public void DrawCards(Action complete)
    {
        if (!turnManager.IsMyTurn(owner)) return;
        Debug.Log("DrawPhase Initialize" + owner);
        int count = 0;
       
        while (handState.GetHandCount() < handState.GetHandLimit())
        {
            Debug.Log("DrawCard");
            CardData card = deckState.Pop();
            handState.AddCardInHand(card);
            count++;
        }
        complete?.Invoke();
    }

    public void DiscardCard(int index)
    {
        //Todo - remove from hand list the card of the current index
    }

    public CardData GetHandCardByIndex(int index)
    {
        return handState.GetHandCardByIndex(index);
    }
}
