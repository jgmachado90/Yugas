using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionAnimator : MonoBehaviour
{
    private FusionManager fusionManager;

    public Transform handFusionCardsTransform;
    public Transform currentFusionCardTransform;

    private void Start()
    {
        fusionManager = SubsystemLocator.GetSubsystem<FusionManager>();
        fusionManager.onFusionCompleted += StartFusion;
    }

    private void OnDestroy()
    {
        fusionManager.onFusionCompleted -= StartFusion;
    }
    public void InitializeFusionAnimation(List<CardData> cards)
    {

    }

    public void StartFusion(CardData fusionCard)
    {
        StartCoroutine("FusionCoroutine");
    }

    public IEnumerator FusionCoroutine()
    {
        /*GetFusionHandCards();
        List<HandCard> fusionCards = 
        CardData current = fusionCards[0];

        for (int i = 0; i < fusionCards.Count - 1; i++)
        {
            fusionCards.RemoveAt(0);
            
        }
        */
        yield return new WaitForSeconds(0.1f);
    }

}
