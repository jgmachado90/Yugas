using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLoader : MonoBehaviour
{
    public GameObject handCardPrefab;

    public List<HandCard> handCards = new List<HandCard>();

    public List<Transform> targetCardPositions = new List<Transform>();
    public Transform OutOfScreenCardPosition;

    public float drawAnimationDelay = 0.2f;
    float drawAnimationDuration = 0.3f;

    private void Start()
    {
        GameManager.Instance.battleManager.onDrawCards += InstantiateHandCards;
    }

    private void InstantiateHandCards(BattleData battleData, int cardCount)
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
                ? targetCardPositions[i].position
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
        Vector3 targetPosition = targetCardPositions[i].position;

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
