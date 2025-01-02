using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardAnimator : MonoBehaviour 
{
    private SelectorManager selectorManager;
    public Transform outOfScreenCardPosition;

    public float drawAnimationDelay = 0.2f;
    float drawAnimationDuration = 0.3f;

    private void Start()
    {
        selectorManager = SubsystemLocator.GetSubsystem<SelectorManager>();
    }

    public void DrawCardAnimation(GameObject handCardObject, int i)
    {
        StartCoroutine(CardDrawAnimationCoroutine(handCardObject, outOfScreenCardPosition, i));
    }

    public IEnumerator CardDrawAnimationCoroutine(GameObject handCardObject, Transform outOfScreenCardPosition, int i)
    {
        Vector3 startPosition = outOfScreenCardPosition.position;
        Vector3 targetPosition = selectorManager.GetCardPositionByIndex(i);
        handCardObject.transform.position = startPosition;
        yield return new WaitForSeconds(drawAnimationDelay * i); // Wait before starting the animation

        // Total duration of the animation in seconds.
        float elapsedTime = 0f;

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
