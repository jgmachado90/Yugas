using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandLoader : MonoBehaviour
{
    private MatchManager matchManager;
    private FusionManager fusionManager;
    private SelectorManager selectorManager;

    public DrawCardAnimator drawCardAnimator;

    public GameObject handCardPrefab;

    public List<HandCard> handCards = new List<HandCard>();

    private void Start()
    {
        matchManager = SubsystemLocator.GetSubsystem<MatchManager>();
        fusionManager = SubsystemLocator.GetSubsystem<FusionManager>();
        selectorManager = SubsystemLocator.GetSubsystem<SelectorManager>();

        matchManager.onDrawCards += DrawCards;
        MatchEvents.onSelectCardForPlay += SelectCardForPlay;
        fusionManager.onFusionRegistered += SelectCardForFusion;
        fusionManager.onFusionUnregistered += UnSelectCardForFusion;
        MatchEvents.onFusionStart += StartFusion;
    }

    private void OnDestroy()
    {
        matchManager.onDrawCards -= DrawCards;
        MatchEvents.onSelectCardForPlay -= SelectCardForPlay;
        fusionManager.onFusionRegistered -= SelectCardForFusion;
        fusionManager.onFusionUnregistered -= UnSelectCardForFusion;
        MatchEvents.onFusionStart -= StartFusion;
    }

    public void StartFusion()
    {
        StartCoroutine("FusionCoroutine");
    }

    public IEnumerator FusionCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        DiscardCards(fusionManager.fusionIndexs);
    }

    public void DiscardCards(List<int> indexes)
    {
        List<HandCard> removedCards = new List<HandCard>();
        foreach (var index in indexes)
        {
            removedCards.Add(handCards[index]);
            handCards.Remove(handCards[index]);
        
        }
    }

    public void SelectCardForFusion(int index)
    {
        int fusionNumber = fusionManager.fusionIndexs.Count;
        handCards[index].SelectForFusion(fusionNumber);
    }

    public void UnSelectCardForFusion(int index)
    {
        int fusionNumber = fusionManager.fusionIndexs.Count;
        handCards[index].CancelFusionSelection();

        ReorderFusionNumbers();
    }

    private void ReorderFusionNumbers()
    {
        List<int> fusions = fusionManager.fusionIndexs;
        for(int i = 0; i < fusions.Count; i++)
        {
            int numb = i + 1;
            handCards[fusions[i]].fusionNumber.text = numb.ToString();
        }   
    }

    public void SelectCardForPlay(int index)
    {
        handCards[index].SelectForPlay();
    }

    private void DrawCards(MatchBattlerStatus battleData, int cardCount, Turn currentTurn)
    {
        int handLimit = matchManager.MatchData.handLimit;
        int cardsInHand = handLimit - cardCount;

        for (int i = 0; i < handLimit; i++)
        {
            GameObject handCardObject = Instantiate(handCardPrefab, transform);
            CardSetup(battleData, cardsInHand, i, handCardObject);

            if (i >= cardsInHand)
                drawCardAnimator.DrawCardAnimation(handCardObject, i);
        }
    }

    private void CardSetup(MatchBattlerStatus battleData, int cardsInHand, int i, GameObject handCardObject)
    {
        HandCard handCard = handCardObject.GetComponent<HandCard>();
        handCard.HandCardSetup(battleData.hand[i]);
        handCards.Add(handCard);
        handCardObject.transform.position = selectorManager.GetCardPositionByIndex(i);
    }
}
