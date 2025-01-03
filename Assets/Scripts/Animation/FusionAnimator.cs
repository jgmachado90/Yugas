using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionAnimator : MonoBehaviour
{
    private FusionManager fusionManager;
    private void Start()
    {
        fusionManager = SubsystemLocator.GetSubsystem<FusionManager>();
        MatchEvents.onFusionStart += StartFusion;
    }

    private void OnDestroy()
    {
        MatchEvents.onFusionStart -= StartFusion;
    }
    public void InitializeFusionAnimation(List<CardData> cards)
    {

    }

    public void StartFusion()
    {
        StartCoroutine("FusionCoroutine");
    }

    public IEnumerator FusionCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
    }

}
