using UnityEngine;

public class InputManager : MonoBehaviour, ISubsystem
{
    private IInputHandlerFactory inputHandlerFactory;

    private IInputHandler currentInputHandler;

    private IStateMachineManager stateMachineManager;

    private void Start()
    {
        stateMachineManager = SubsystemLocator.GetSubsystem<StateMachineManager>();
        inputHandlerFactory = SubsystemLocator.GetSubsystem<InputHandlerFactory>();

        stateMachineManager.OnMainPhaseInitialize += SetMainPhaseInputHandler;
        stateMachineManager.OnBattleInitialize += SetBattleInputHandler;
    }

    private void OnDestroy()
    {
        stateMachineManager.OnMainPhaseInitialize -= SetMainPhaseInputHandler;
        stateMachineManager.OnBattleInitialize -= SetBattleInputHandler;
    }

    private void SetMainPhaseInputHandler()
    {
        SetInputHandler(inputHandlerFactory.CreateMainPhaseHandler());
    }

    private void SetBattleInputHandler()
    {
        SetInputHandler(inputHandlerFactory.CreateBattlePhaseHandler());
    }


    public void SetInputHandler(IInputHandler inputHandler)
    {
        currentInputHandler = inputHandler;
    }

    private void Update()
    {
        currentInputHandler?.HandleInput();
    }

    public void Initialize()
    {

    }

    public void Shutdown()
    {
  
    }
}
