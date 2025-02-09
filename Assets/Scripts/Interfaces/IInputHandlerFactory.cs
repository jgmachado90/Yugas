public interface IInputHandlerFactory
{
    IInputHandler CreateMainPhaseHandler();
    IInputHandler CreateBattlePhaseHandler();
}