using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using UnityEngine;

public class MatchManager : MonoBehaviour, ISubsystem
{
    private StateMachineManager stateMachineManager;
    private PlayerState playerState;

    [SerializeField] private MatchData matchData;
    public MatchData MatchData {  get { return matchData; } }
    public DeckData enemyDeck;
    private Match match;
    public Match Match {  get { return match; } }

    private TurnManager turnManager;

    public Action<MatchBattlerStatus, int, Turn> onDrawCards;
    public Action<CardData> onSelectCard;

    private void Awake()
    {
        stateMachineManager = SubsystemLocator.GetSubsystem<StateMachineManager>();
        playerState = SubsystemLocator.GetSubsystem<PlayerState>();

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
        match = new Match(matchData, playerState.DeckData, enemyDeck);
        turnManager = new TurnManager();
        complete?.Invoke();
    }

    private void Draw(Action complete)
    {
        switch (turnManager.GetCurrentTurn())
        {
            case Turn.Player:
            DrawCards(match.PlayerBattlerStatus, complete);
            break;
            case Turn.AI:
            DrawCards(match.EnemyBattlerStatus, complete);
            break;
        }
    }

    private void DrawCards(MatchBattlerStatus battleData, Action complete)
    {
        int drawCount = battleData.DrawCards();
        onDrawCards.Invoke(battleData, drawCount, turnManager.GetCurrentTurn());
        complete?.Invoke();
    }

    public CardData GetCurrentHandCardByIndex(int index)
    {
        if(turnManager.GetCurrentTurn() == Turn.Player)
        {
            return match.PlayerBattlerStatus.hand[index];
        }
        return match.EnemyBattlerStatus.hand[index];
    }

    public void Initialize()
    {
    }

    public void Shutdown()
    {
    }
}
