public interface IStateMachine
{
    public void ChangeState(IState newState);
    public void Update();
}