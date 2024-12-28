using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandLoader : MonoBehaviour
{
    public GameObject handCardPrefab;

    public List<HandCard> handCards = new List<HandCard>();

    public Transform OutOfScreenCardPosition;


    public float drawAnimationDelay = 0.2f;
    float drawAnimationDuration = 0.3f;

    private void Start()
    {
        GameManager.Instance.battleManager.onDrawCards += InstantiateHandCards;
        MatchEvents.onSelectCardForPlay += SelectCardForPlay;
        GameManager.Instance.fusionManager.onFusionRegistered += SelectCardForFusion;
        GameManager.Instance.fusionManager.onFusionUnregistered += UnSelectCardForFusion;
    }

    private void OnDestroy()
    {
        GameManager.Instance.battleManager.onDrawCards -= InstantiateHandCards;
        MatchEvents.onSelectCardForPlay -= SelectCardForPlay;
        GameManager.Instance.fusionManager.onFusionRegistered -= SelectCardForFusion;
        GameManager.Instance.fusionManager.onFusionUnregistered -= UnSelectCardForFusion;
    }

    public void SelectCardForFusion(int index)
    {
        Debug.Log("select Card for fusion");
        int fusionNumber = GameManager.Instance.fusionManager.fusionIndexs.Count;
        handCards[index].SelectForFusion(fusionNumber);
    }

    public void UnSelectCardForFusion(int index)
    {
        Debug.Log("unselect Card for fusion");
        int fusionNumber = GameManager.Instance.fusionManager.fusionIndexs.Count;
        handCards[index].CancelFusionSelection();

        ReorderFusionNumbers();
    }

    private void ReorderFusionNumbers()
    {
        List<int> fusions = GameManager.Instance.fusionManager.fusionIndexs;
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
                ? GameManager.Instance.selectorManager.GetCardPositionByIndex(i)
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
        Vector3 targetPosition = GameManager.Instance.selectorManager.GetCardPositionByIndex(i);

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
