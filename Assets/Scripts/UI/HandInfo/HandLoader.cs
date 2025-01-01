using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandLoader : MonoBehaviour
{
    private BattleManager battleManager;
    private FusionManager fusionManager;
    private SelectorManager selectorManager;

    public GameObject handCardPrefab;

    public List<HandCard> handCards = new List<HandCard>();

    public Transform OutOfScreenCardPosition;


    public float drawAnimationDelay = 0.2f;
    float drawAnimationDuration = 0.3f;

    private void Start()
    {
        battleManager = SubsystemLocator.GetSubsystem<BattleManager>();
        fusionManager = SubsystemLocator.GetSubsystem<FusionManager>();
        selectorManager = SubsystemLocator.GetSubsystem<SelectorManager>();

        battleManager.onDrawCards += InstantiateHandCards;
        MatchEvents.onSelectCardForPlay += SelectCardForPlay;
        fusionManager.onFusionRegistered += SelectCardForFusion;
        fusionManager.onFusionUnregistered += UnSelectCardForFusion;
        MatchEvents.onFusionStart += StartFusion;
    }

    private void OnDestroy()
    {
        battleManager.onDrawCards -= InstantiateHandCards;
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
        Debug.Log("select Card for fusion");
        int fusionNumber = fusionManager.fusionIndexs.Count;
        handCards[index].SelectForFusion(fusionNumber);
    }

    public void UnSelectCardForFusion(int index)
    {
        Debug.Log("unselect Card for fusion");
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
        handCards[index].TurnBack();
    }

    private void InstantiateHandCards(BattleData battleData, int cardCount, Turn currentTurn)
    {
        int cardsInHand = 5 - cardCount;
        for (int i = 0; i < 5; i++)
        {
            GameObject handCardObject = Instantiate(handCardPrefab, transform);
            HandCard handCard = handCardObject.GetComponent<HandCard>();
            handCard.HandCardSetup(battleData.hand[i]);
            handCards.Add(handCard);

            // Set the initial position of the card.
            handCardObject.transform.position = i < cardsInHand
                ? selectorManager.GetCardPositionByIndex(i)
                : OutOfScreenCardPosition.position;

            // Animate only if the card is outside of the hand initially.
            if (i >= cardsInHand)
            {
                StartCoroutine(CardDrawAnimationCoroutine(handCardObject, i));
            }
        }
    }
    public IEnumerator CardDrawAnimationCoroutine(GameObject handCardObject, int i)
    {
        yield return new WaitForSeconds(drawAnimationDelay * i); // Wait before starting the animation

         // Total duration of the animation in seconds.
        float elapsedTime = 0f;

        Vector3 startPosition = OutOfScreenCardPosition.position;
        Vector3 targetPosition = selectorManager.GetCardPositionByIndex(i);

        while (elapsedTime < drawAnimationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / drawAnimationDuration); // Normalize time to [0, 1]

            // Apply easing function for smoother animation (ease out).
            t = t * t * (3f - 2f * t);

            handCardObject.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        // Ensure the card is exactly at the target position at the end.
        handCardObject.transform.position = targetPosition;
    }

}
