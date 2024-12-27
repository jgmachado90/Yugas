using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour
{

    public List<CardData> cardsToFusion;

    private void Start()
    {
        CardData result = TryFusion(cardsToFusion);
        Debug.Log(result.cardName);
    }

    public FusionDataBase fusionData;
    public CardData TryFusion(List<CardData> FusionList)
    {
        if (FusionList == null || FusionList.Count < 2)
            return null;

        CardData currentCard = FusionList[0]; // Come�a com a primeira carta.

        for (int i = 1; i < FusionList.Count; i++)
        {
            CardData nextCard = FusionList[i];
            FusionData bestFusion = null;

            // Procura a melhor fus�o poss�vel para as duas cartas.
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
                // Se uma fus�o falha, retorna null.
                return null;
            }
        }

        return currentCard; // Retorna o resultado final da �ltima fus�o.
    }

    private bool IsFusionValid(CardData card1, CardData card2, FusionData fusion)
    {
        // Verifica se os crit�rios de fus�o s�o atendidos para as duas cartas.
        return DoesCardMatchFusionCriteria(card1, fusion.fusionMonster) &&
               DoesCardMatchFusionCriteria(card2, fusion.fusionMonster2) &&
               card1.atk >= fusion.fusionMonster.minAtk &&
               card2.atk >= fusion.fusionMonster2.minAtk &&
               fusion.fusionCardData.atk >= Mathf.Max(card1.atk, card2.atk);
    }

    private bool DoesCardMatchFusionCriteria(CardData card, FusionMonster fusionMonster)
    {
        // Verifica se pelo menos um dos crit�rios � v�lido.
        if (fusionMonster.monsterNames.Contains(card.cardName) ||
            fusionMonster.monsterSpecifications.Exists(spec => card.specifications.Contains(spec)) ||
            fusionMonster.monsterType.Contains(card.monsterType))
        {
            return true;
        }

        return false;
    }
}
