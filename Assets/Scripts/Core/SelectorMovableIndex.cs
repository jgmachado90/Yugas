using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMovableIndex : MonoBehaviour
{
    private SelectorManager selectorManager;
    public Vector3 selectorOffset;

    void Start()
    {
        gameObject.SetActive(false);
        selectorManager = SubsystemLocator.GetSubsystem<SelectorManager>();
        selectorManager.onSelectorMoved += MoveSelector;
    }

    private void MoveSelector(int index)
    {
        gameObject.SetActive(true);
        Vector3 SelectorPosition = selectorManager.GetCardPositionByIndex(index);
        transform.position = SelectorPosition + selectorOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
