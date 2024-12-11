public class InitialPhase : IState
{
    private StateMachineManager stateManager;

    private int pendingTasks;


    public InitialPhase(StateMachineManager manager)
    {
        stateManager = manager;
    }

    public void Enter()
    {
        pendingTasks++;
        stateManager.TriggerMatchInitialization(OnTaskCompleted);
    }

    public void Execute()
    {
        if (pendingTasks <= 0)
        {
            stateManager.StateMachine.ChangeState(new DrawPhase(stateManager));
        }
    }

    public void Exit()
    {
    
    }

    private void OnTaskCompleted()
    {
        pendingTasks--;
    }
}
