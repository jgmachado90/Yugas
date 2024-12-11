public interface IState
{
    void Enter();    // Chamado ao entrar no estado
    void Execute();  // Chamado a cada atualização no estado
    void Exit();     // Chamado ao sair do estado
}