using System.Collections;
using UnityEngine;
public class MainPhaseInputHandler : IInputHandler
{
    private SelectorManager _selectorManager;

    public MainPhaseInputHandler(SelectorManager selectorManager)
    {
        _selectorManager = selectorManager;
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _selectorManager.MoveSelector(1); // Move para a direita
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
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
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Marcar para fusão
        {
            MarkForFusion();
        }
    }

    private void ViewCardDetails()
    {
        var card = _selectorManager.GetSelectedCard();
        MatchEvents.onViewCardDetails?.Invoke(card);
    }

    private void SelectCardForPlay()
    {
        var card = _selectorManager.GetSelectedCard();
        MatchEvents.onSelectCardForPlay?.Invoke(card);
    }

    private void MarkForFusion()
    {
        var card = _selectorManager.GetSelectedCard();
        MatchEvents.onMarkForFusion?.Invoke(card);
    }
}