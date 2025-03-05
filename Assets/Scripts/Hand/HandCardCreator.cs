using UnityEngine;

public class HandCardCreator
{
    private Transform parent;
    private MatchManager matchManager;
    private SelectorManager selectorManager;
    private CardFactory cardFactory;
    private IGameData gameData;

    public HandCardCreator(Transform parent, DrawCardAnimator drawCardAnimator)
    {
        selectorManager = SubsystemLocator.GetSubsystem<SelectorManager>();
        cardFactory = SubsystemLocator.GetSubsystem<CardFactory>();
        gameData = SubsystemLocator.GetSubsystem<GameData>(); 
        matchManager = SubsystemLocator.GetSubsystem<MatchManager>();
        this.parent = parent;
    }

    public GameObject CreateAndSetupCard(CardData cardData, int i)
    {
        GameObject handCardObject = CreateCard();
        CardSetup(cardData, handCardObject, i);
        return handCardObject;
    }

    private GameObject CreateCard()
    {
        ICard createdCard = cardFactory.CreateCard(CardFactoryType.HandCard);
        MonoBehaviour cardMonobehaviour = createdCard as MonoBehaviour;
        GameObject handCardObject = cardMonobehaviour.gameObject;
        handCardObject.transform.SetParent(parent);
        handCardObject.GetComponent<RectTransform>().localScale = Vector3.one;
        return handCardObject;
    }

    private void CardSetup(CardData cardData, GameObject handCardObject, int i)
    {
        HandCard handCard = handCardObject.GetComponent<HandCard>();
        handCard.HandCardSetup(cardData);
        handCardObject.transform.position = selectorManager.GetCardPositionByIndex(i);
    }
}
