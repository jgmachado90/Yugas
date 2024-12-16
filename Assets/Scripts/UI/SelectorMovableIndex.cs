using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMovableIndex : MonoBehaviour
{
    public Vector3 selectorOffset;

    void Start()
    {
        GameManager.Instance.selectorManager.onSelectorMoved += MoveSelector;
    }

    private void MoveSelector(int index)
    {
        Vector3 SelectorPosition = GameManager.Instance.selectorManager.GetCardPositionByIndex(index);
        transform.position = SelectorPosition + selectorOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
