using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour, ISubsystem
{
    private IInputHandler currentInputHandler;

    private StateMachineManager stateMachineManager;
    private SelectorManager selectorManager;

    private void Start()
    {
        stateMachineManager = SubsystemLocator.GetSubsystem<StateMachineManager>();
        selectorManager = SubsystemLocator.GetSubsystem<SelectorManager>();

        stateMachineManager.OnMainPhaseInitialize += SetMainPhaseInputHandler;
        stateMachineManager.OnBattleInitialize += SetBattleInputHandler;
    }

    private void OnDestroy()
    {
        stateMachineManager.OnMainPhaseInitialize -= SetMainPhaseInputHandler;
        stateMachineManager.OnBattleInitialize -= SetBattleInputHandler;
    }

    private void SetMainPhaseInputHandler()
    {
        SetInputHandler(new MainPhaseInputHandler(selectorManager));
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

    public void Initialize()
    {

    }

    public void Shutdown()
    {
  
    }
}
