public interface IState
{
    void Enter();    // Chamado ao entrar no estado
    void Execute();  // Chamado a cada atualiza��o no estado
    void Exit();     // Chamado ao sair do estado
}