using System;
using UnityEngine;

public class MatchManager : MonoBehaviour, ISubsystem
{
    private IStateMachineManager stateMachineManager;
    private IGameData gameData;
    private IGameState gameState;

    private ITurnManager turnManager;

    public Action<IHandState,int, Turn> onDrawCards;
    public Action<CardData> onSelectCard;

    private void Awake()
    {
        stateMachineManager = SubsystemLocator.GetSubsystem<StateMachineManager>();
        gameData = SubsystemLocator.GetSubsystem<GameData>();
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
        turnManager = new TurnManager();
        complete?.Invoke();
    }

    private void Draw(Action complete)
    {
        switch (turnManager.GetCurrentTurn())
        {
            case Turn.Player:
            DrawCards(gameState.GetHandState(true), complete);
            break;
            case Turn.AI:
            DrawCards(gameState.GetHandState(false), complete);
            break;
        }
    }

    private void DrawCards(IHandState handState, Action complete)
    {
        int drawCount = handState.DrawCards();
        onDrawCards.Invoke(handState, drawCount, turnManager.GetCurrentTurn());
        complete?.Invoke();
    }

    public CardData GetCurrentHandCardByIndex(int index)
    {
        if(turnManager.GetCurrentTurn() == Turn.Player)
        {
            return gameState.GetHandState(true).GetHandCardByIndex(index);
        }
        return gameState.GetHandState(false).GetHandCardByIndex(index);
    }

    public void Initialize()
    {
    }

    public void Shutdown()
    {
    }
}
