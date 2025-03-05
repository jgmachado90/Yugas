using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager : MonoBehaviour, ISubsystem, IStateMachineManager
{
    public IStateMachine StateMachine { get;private set; }

    public event Action<Action> OnMatchInitialize;
    public event Action<Action> OnDrawPhaseInitialize;

    public event Action OnMainPhaseInitialize;
    public event Action OnBattleInitialize;


    public void Start()
    {
        Invoke("InitializeGame", 1f); 
    }

    public void InitializeGame()
    {
        StateMachine = new StateMachine();
        StateMachine.ChangeState(new InitialPhase(this));
    }

    private void Update()
    {
        StateMachine?.Update();
    }

    public void TriggerMatchInitialization(Action onComplete)
    {
        OnMatchInitialize?.Invoke(onComplete);
    }

    public void TriggerDrawInitialization(Action onComplete)
    {
        OnDrawPhaseInitialize?.Invoke(onComplete);
    }

    public void TriggerMainPhaseInitialization()
    {
        OnMainPhaseInitialize?.Invoke();    
    }

    public void Initialize()
    {

    }

    public void Shutdown()
    {

    }
}
