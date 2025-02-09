using System;

public interface IStateMachineManager 
{
    event Action<Action> OnMatchInitialize;
    event Action<Action> OnDrawPhaseInitialize;
    event Action OnMainPhaseInitialize;
    event Action OnBattleInitialize;
}
