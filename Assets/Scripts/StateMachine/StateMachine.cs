public class StateMachine
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();       // Sair do estado atual
        currentState = newState;    // Alterar para o novo estado
        currentState?.Enter();      // Entrar no novo estado
    }

    public void Update()
    {
        currentState?.Execute();    // Atualizar o estado atual
    }
}