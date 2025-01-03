using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private FusionHandController fusionHandController;
    private DrawHandController drawHandController;

    [SerializeField] private DropHandCardsAnimator dropHandCardsAnimator;

    [SerializeField] private Transform playCardsTransform;

    public DrawCardAnimator drawCardAnimator;
    public List<HandCard> handCards = new List<HandCard>();

    private void Start()
    {
        fusionHandController = new FusionHandController(this);
        drawHandController = new DrawHandController(this, drawCardAnimator);

        MatchEvents.onSelectCardForPlay += SelectCardForPlay;
    }

    private void OnDestroy()
    {
        MatchEvents.onSelectCardForPlay -= SelectCardForPlay;
        fusionHandController.OnDestroy();
        drawHandController.OnDestroy();
    }

    public void DiscardCard(int index)
    {
        //Todo - remove from hand list the card of the current index
    }

    public void SelectCardForPlay(int index)
    {
        handCards[index].SelectForPlay();
        List<int> cardsForPlay = new List<int>();
        cardsForPlay.Add(index);
        PlayCards(cardsForPlay);
    }

    public void PlayCards(List<int> indexes)
    {
        foreach (int index in indexes)
        {
            handCards[index].transform.SetParent(playCardsTransform);
        }
        dropHandCardsAnimator.DropHandCards();
    }

}
