using System.Collections;
using UnityEngine;

public class DrawCardAnimator : MonoBehaviour
{
    private SelectorManager selectorManager;
    public Transform outOfScreenCardPosition;

    public float drawAnimationDelay = 0.2f;
    public float drawAnimationDuration = 0.3f;

    private void Start()
    {
        selectorManager = SubsystemLocator.GetSubsystem<SelectorManager>();
    }

    public void DrawCardAnimation(GameObject handCardObject, int i)
    {
        // Inicia uma nova corrotina para cada carta comprada
        StartCoroutine(CardDrawAnimationCoroutine(handCardObject, i));
    }

    private IEnumerator CardDrawAnimationCoroutine(GameObject handCardObject, int i)
    {
        Vector3 startPosition = outOfScreenCardPosition.position;
        Vector3 targetPosition = selectorManager.GetCardPositionByIndex(i);
        handCardObject.transform.position = startPosition;

        // Espera antes de começar a animação (efeito cascata)
        yield return new WaitForSeconds(drawAnimationDelay * i);

        float elapsedTime = 0f;

        while (elapsedTime < drawAnimationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / drawAnimationDuration);
            t = t * t * (3f - 2f * t); // Ease-out suave
            handCardObject.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Garante a posição final exata
        handCardObject.transform.position = targetPosition;
    }
}
