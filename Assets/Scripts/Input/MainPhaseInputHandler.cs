using System;
using System.Collections;
using UnityEngine;
public class MainPhaseInputHandler : IInputHandler
{
    private SelectorManager selectorManager;
    private FusionManager fusionManager;

    bool playingCard = false;

    public MainPhaseInputHandler(SelectorManager selectorManager)
    {
        this.selectorManager = selectorManager;
        fusionManager = SubsystemLocator.GetSubsystem<FusionManager>(); 
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(!playingCard)
                selectorManager.MoveSelector(1); // Move para a direita
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(!playingCard)
                selectorManager.MoveSelector(-1); // Move para a esquerda
        }
        if (Input.GetKeyDown(KeyCode.D)) // Ver detalhes da carta
        {
            ViewCardDetails();
        }
        if (Input.GetKeyDown(KeyCode.S)) // Selecionar carta para jogar
        {
            SelectCardForPlay();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Marcar para fusão
        {
            MarkForFusion();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // Marcar para fusão
        {
            UnMarkForFusion();
        }
    }

    private void ViewCardDetails()
    {
        var card = selectorManager.GetSelectedCard();
        MatchEvents.onViewCardDetails?.Invoke(card);
    }

    private void SelectCardForPlay()
    {
        if(fusionManager.fusionCards.Count > 0)
        {
            MatchEvents.onFusionStart.Invoke();
            return;
        }

        int index = selectorManager.GetSelectorIndex();
        MatchEvents.onSelectCardForPlay?.Invoke(index);
        playingCard = true;
    }

    private void MarkForFusion()
    {
        int index = selectorManager.GetSelectorIndex();
        var card = selectorManager.GetSelectedCard();
        MatchEvents.onMarkForFusion?.Invoke(card, index);
    }

    private void UnMarkForFusion()
    {
        int index = selectorManager.GetSelectorIndex();
        var card = selectorManager.GetSelectedCard();
        MatchEvents.onUnMarkForFusion?.Invoke(card, index);
    }
}