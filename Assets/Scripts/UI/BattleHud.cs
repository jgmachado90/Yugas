using TMPro;
using UnityEngine;

public class BattleHud : MonoBehaviour
{
    private IStateMachineManager stateMachineManager;

    private IGameState gameState;

    public TextMeshProUGUI playerCardCount;
    public TextMeshProUGUI AICardCount;

    private void Start()
    {
        gameState = SubsystemLocator.GetSubsystem<IGameState>();
        stateMachineManager = SubsystemLocator.GetSubsystem<IStateMachineManager>();
        stateMachineManager.OnMainPhaseInitialize += UpdateCardCount;
    }

    private void UpdateCardCount()
    {
        int playerCount = gameState.GetDeckState(Owner.Player).GetCardCount();
        int AICount = gameState.GetDeckState(Owner.AI).GetCardCount();

        UpdateCardCountAnimation(playerCardCount, playerCount);
        UpdateCardCountAnimation(AICardCount, AICount);
    
    }

    private void UpdateCardCountAnimation(TextMeshProUGUI cardCountText, int numCards)
    {
        int cardCount;
        int.TryParse(cardCountText.text,out cardCount);
        cardCount -= numCards;
        cardCountText.text = cardCount.ToString();
    }
}
