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

    public DrawCardAnimator drawCardAnimator;
    public List<HandCard> handCards = new List<HandCard>();

    public Action<List<HandCard>> onHandFusion;

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
    }

    public void PlayFusion(List<int> indexes)
    {
        List<HandCard> fusionHandCards = new List<HandCard>();
        foreach (int index in indexes)
        {
            fusionHandCards.Add(handCards[index]);
        }
       // dropHandCardsAnimator.DropHandCards(fusionHandCards);
    }

}
