using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private IInputHandler currentInputHandler;

    private void Start()
    {
        GameManager.Instance.stateMachineManager.OnMainPhaseInitialize += SetMainPhaseInputHandler;
        GameManager.Instance.stateMachineManager.OnBattleInitialize += SetBattleInputHandler;
    }

    private void OnDestroy()
    {
        GameManager.Instance.stateMachineManager.OnMainPhaseInitialize -= SetMainPhaseInputHandler;
        GameManager.Instance.stateMachineManager.OnBattleInitialize -= SetBattleInputHandler;
    }

    private void SetMainPhaseInputHandler()
    {
        SetInputHandler(new MainPhaseInputHandler(GameManager.Instance.selectorManager));
    }

    private void SetBattleInputHandler()
    {
        SetInputHandler(new BattlePhaseInputHandler());
    }


    public void SetInputHandler(IInputHandler inputHandler)
    {
        currentInputHandler = inputHandler;
    }

    private void Update()
    {
        currentInputHandler?.HandleInput();
    }
}
