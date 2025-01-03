using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHandCardsAnimator : MonoBehaviour
{
    public Transform dropPosition;
    float dropAnimationDuration = 1f;

    public void DropHandCards()
    {
        StartCoroutine("CardDrawAnimationCoroutine");
    }

    public IEnumerator CardDrawAnimationCoroutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = dropPosition.position;
        // Total duration of the animation in seconds.
        float elapsedTime = 0f;

        while (elapsedTime < dropAnimationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / dropAnimationDuration); // Normalize time to [0, 1]

            // Apply easing function for smoother animation (ease out).
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        // Ensure the card is exactly at the target position at the end.
        transform.position = targetPosition;
    }
}
