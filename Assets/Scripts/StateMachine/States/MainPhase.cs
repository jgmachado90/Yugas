public class MainPhase : IState
{
    private StateMachineManager stateMachineManager;

    public MainPhase(StateMachineManager manager)
    {
        stateMachineManager = manager;
    }

    public void Enter()
    {
        stateMachineManager.TriggerMainPhaseInitialization();
    }

    public void Execute()
    {
       
    }

    public void Exit()
    {
       
    }
}
