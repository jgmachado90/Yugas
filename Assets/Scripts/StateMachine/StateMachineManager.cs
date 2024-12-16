using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }

    public event Action<Action> OnMatchInitialize;
    public event Action<Action> OnDrawPhaseInitialize;
    public event Action OnMainPhaseInitialize;
    public event Action OnBattleInitialize;

    private void Start()
    {
        Debug.Log("Initialize StateMachine");
        StateMachine = new StateMachine();
        StateMachine.ChangeState(new InitialPhase(this));
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void TriggerMatchInitialization(Action onComplete)
    {
        Debug.Log("OnMatchInitialize");
        OnMatchInitialize?.Invoke(onComplete);
    }

    public void TriggerDrawInitialization(Action onComplete)
    {
        Debug.Log("OnDrawInitialize");
        OnDrawPhaseInitialize?.Invoke(onComplete);
    }

    public void TriggerMainPhaseInitialization()
    {
        Debug.Log("On main phase initialize");
        OnMainPhaseInitialize?.Invoke();    
    }
}
