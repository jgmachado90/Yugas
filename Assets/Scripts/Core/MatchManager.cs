using System;
using UnityEngine;

public class MatchManager : MonoBehaviour, ISubsystem, IMatchManager
{
    private IGameData gameData;
    private IGameState gameState;

    private IHandController playerHandController;
    private IHandController enemyHandController;

    private IPlayActionController playActionController;

    private ITurnManager turnManager;

    private IStateMachineManager stateMachineManager;

    public Action<CardData> onSelectCard;

    private void Awake()
    {
        stateMachineManager = SubsystemLocator.GetSubsystem<StateMachineManager>();
        gameData = SubsystemLocator.GetSubsystem<GameData>();
        gameState = SubsystemLocator.GetSubsystem<GameState>();
        turnManager = new TurnManager();
        stateMachineManager.OnMatchInitialize += InitializeMatch;
    }

    private void OnDestroy()
    {
        if (stateMachineManager == null) return;
        stateMachineManager.OnMatchInitialize -= InitializeMatch;
    }

    private void InitializeMatch(Action complete)
    {
        playerHandController = new HandController(this, Owner.Player);
        enemyHandController = new HandController(this, Owner.AI);
        complete?.Invoke();
    }

    public ITurnManager GetTurnManager()
    {
        return turnManager;
    }

    public IHandController GetCurrentHandController()
    {
        return turnManager.GetCurrentTurn() == Owner.Player ? playerHandController : enemyHandController;
    }

    public void Initialize()
    {
    }

    public void Shutdown()
    {
        playerHandController.Shutdown();
        enemyHandController.Shutdown();
    }
}
