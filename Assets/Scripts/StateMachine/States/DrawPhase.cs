public class DrawPhase : IState
{
    private StateMachineManager stateMachineManager;

    private int pendingTasks;

    public DrawPhase(StateMachineManager manager)
    {
        stateMachineManager = manager;
    }

    public void Enter()
    {
        pendingTasks++;
        stateMachineManager.TriggerDrawInitialization(OnTaskCompleted);
    }

    public void Execute()
    {
        if (pendingTasks <= 0)
        {
            stateMachineManager.StateMachine.ChangeState(new MainPhase(stateMachineManager));
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