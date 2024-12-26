using System;
using System.Collections;
using UnityEngine;
public class MainPhaseInputHandler : IInputHandler
{
    private SelectorManager _selectorManager;

    bool playingCard = false;

    public MainPhaseInputHandler(SelectorManager selectorManager)
    {
        _selectorManager = selectorManager;
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(!playingCard)
                _selectorManager.MoveSelector(1); // Move para a direita
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(!playingCard)
                _selectorManager.MoveSelector(-1); // Move para a esquerda
        }
        if (Input.GetKeyDown(KeyCode.D)) // Ver detalhes da carta
        {
            ViewCardDetails();
        }
        if (Input.GetKeyDown(KeyCode.S)) // Selecionar carta para jogar
        {
            SelectCardForPlay();
        }
        if (Input.GetKeyDown(KeyCode.X)) // Selecionar carta para jogar
        {
            CancelPlayingCard();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Marcar para fusão
        {
            MarkForFusion();
        }
    }

    private void CancelPlayingCard()
    {
        throw new NotImplementedException();
    }

    private void ViewCardDetails()
    {
        var card = _selectorManager.GetSelectedCard();
        MatchEvents.onViewCardDetails?.Invoke(card);
    }

    private void SelectCardForPlay()
    {
        int index = _selectorManager.GetSelectorIndex();
        MatchEvents.onSelectCardForPlay?.Invoke(index);
        playingCard = true;
    }

    private void MarkForFusion()
    {
        var card = _selectorManager.GetSelectedCard();
        MatchEvents.onMarkForFusion?.Invoke(card);
    }
}