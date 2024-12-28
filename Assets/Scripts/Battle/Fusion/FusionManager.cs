using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour
{
    public FusionDataBase fusionData;

    public CardData currentFusionResult;

    public List<CardData> fusionCards;
    public List<int> fusionIndexs;

    public Action<int> onFusionRegistered;

    private void Start()
    {
        MatchEvents.onFusionStart += TryFusion;
        MatchEvents.onMarkForFusion += MarkForFusion;
        MatchEvents.onCancelFusion += CancelFusion;
    }

    private void OnDestroy()
    {
        MatchEvents.onFusionStart -= TryFusion;
        MatchEvents.onMarkForFusion -= MarkForFusion;
        MatchEvents.onCancelFusion -= CancelFusion;
    }

    private void MarkForFusion(CardData card, int index)
    {
        if (fusionIndexs.Contains(index)) return;
        fusionCards.Add(card);
        fusionIndexs.Add(index);
        onFusionRegistered.Invoke(index);
    }

    private void CancelFusion()
    {
        fusionCards.Clear();
    }

    public void TryFusion()
    {
        if (fusionCards == null || fusionCards.Count < 2)
            currentFusionResult = null;

        CardData currentCard = fusionCards[0];

        for (int i = 1; i < fusionCards.Count; i++)
        {
            CardData nextCard = fusionCards[i];
            FusionData bestFusion = null;

            foreach (var fusion in fusionData.FusionData)
            {
                if ((IsFusionValid(currentCard, nextCard, fusion) ||
                    IsFusionValid(nextCard, currentCard, fusion)) &&
                   (bestFusion == null || fusion.fusionCardData.atk > bestFusion.fusionCardData.atk))
                {
                    bestFusion = fusion;
                }
            }

            if (bestFusion != null)
            {
                currentCard = bestFusion.fusionCardData;
            }
            else
            {
                currentFusionResult = fusionCards[fusionCards.Count-1];
            }
        }

        currentFusionResult = currentCard;
    }

    private bool IsFusionValid(CardData card1, CardData card2, FusionData fusion)
    {
        return DoesCardMatchFusionCriteria(card1, fusion.fusionMonster) &&
               DoesCardMatchFusionCriteria(card2, fusion.fusionMonster2) &&
               card1.atk >= fusion.fusionMonster.minAtk &&
               card2.atk >= fusion.fusionMonster2.minAtk &&
               fusion.fusionCardData.atk >= Mathf.Max(card1.atk, card2.atk);
    }

    private bool DoesCardMatchFusionCriteria(CardData card, FusionMonster fusionMonster)
    {
        if (fusionMonster.monsterNames.Contains(card.cardName) ||
            fusionMonster.monsterSpecifications.Exists(spec => card.specifications.Contains(spec)) ||
            fusionMonster.monsterType.Contains(card.monsterType))
        {
            return true;
        }

        return false;
    }
}
