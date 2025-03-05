using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionHandController
{
    private FusionManager fusionManager;
    private HandController handController;

    public FusionHandController(HandController handController)
    {
        this.handController = handController;
        fusionManager = SubsystemLocator.GetSubsystem<FusionManager>();
        fusionManager.onFusionRegistered += SelectCardForFusion;
        fusionManager.onFusionUnregistered += UnSelectCardForFusion;
        fusionManager.onFusionCompleted += SelectFusionCardsForPlay;
    }

    private void SelectFusionCardsForPlay(CardData data)
    {
       // handController.PlayFusion(fusionManager.fusionIndexs);
    }

    public void OnDestroy()
    {
        fusionManager.onFusionRegistered -= SelectCardForFusion;
        fusionManager.onFusionUnregistered -= UnSelectCardForFusion;
    }

    public void SelectCardForFusion(int index)
    {
        int fusionNumber = fusionManager.fusionIndexs.Count;
        //handController.GetHandCardByIndex(index).SelectForFusion(fusionNumber);
    }

    public void UnSelectCardForFusion(int index)
    {
        int fusionNumber = fusionManager.fusionIndexs.Count;
        //handController.GetHandCardByIndex(index).CancelFusionSelection();

        ReorderFusionNumbers();
    }
    private void ReorderFusionNumbers()
    {
        List<int> fusions = fusionManager.fusionIndexs;
        for (int i = 0; i < fusions.Count; i++)
        {
            int numb = i + 1;
           // handController.handCards[fusions[i]].fusionNumber.text = numb.ToString();
        }
    }
}
