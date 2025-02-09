using System;
using UnityEngine;

public class MatchManager : MonoBehaviour, ISubsystem
{
    private IStateMachineManager stateMachineManager;
    private IGameState gameState;

    private IBattlerStatus playerBattlerStatus;
    private IBattlerStatus enemyBattlerStatus;

    private ITurnManager turnManager;

    public Action<IBattlerStatus, int, Turn> onDrawCards;
    public Action<CardData> onSelectCard;

    private void Awake()
    {
        stateMachineManager = SubsystemLocator.GetSubsystem<StateMachineManager>();
        gameState = SubsystemLocator.GetSubsystem<GameState>();

        stateMachineManager.OnMatchInitialize += InitializeMatch;
        stateMachineManager.OnDrawPhaseInitialize += Draw;
    }

    private void OnDestroy()
    {
        if (stateMachineManager == null) return;
        stateMachineManager.OnMatchInitialize -= InitializeMatch;
    }

    private void InitializeMatch(Action complete)
    {
        playerBattlerStatus = new BattlerStatus(gameState.GetMatchData(), gameState.GetDeckData());
        enemyBattlerStatus = new BattlerStatus(gameState.GetMatchData(), gameState.GetEnemyDeckData());

        turnManager = new TurnManager();
        complete?.Invoke();
    }

    private void Draw(Action complete)
    {
        switch (turnManager.GetCurrentTurn())
        {
            case Turn.Player:
            DrawCards(playerBattlerStatus, complete);
            break;
            case Turn.AI:
            DrawCards(enemyBattlerStatus, complete);
            break;
        }
    }

    private void DrawCards(IBattlerStatus battleData, Action complete)
    {
        int drawCount = battleData.DrawCards();
        onDrawCards.Invoke(battleData, drawCount, turnManager.GetCurrentTurn());
        complete?.Invoke();
    }

    public CardData GetCurrentHandCardByIndex(int index)
    {
        if(turnManager.GetCurrentTurn() == Turn.Player)
        {
            return playerBattlerStatus.GetHandCardByIndex(index);
        }
        return enemyBattlerStatus.GetHandCardByIndex(index);
    }

    public void Initialize()
    {
    }

    public void Shutdown()
    {
    }
}
