using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardFactoryType
{
    HandCard,
    DetailedCard,
    InfoCard
}

public class CardFactory : MonoBehaviour, ISubsystem
{
    public HandCardFactory handCardFactory;
    public DetailedCardFactory detailedCardFactory;
    public InfoCardFactory infoCardFactory;

    private List<ICard> createdCards = new List<ICard>();


    public ICard CreateCard(CardFactoryType Type)
    {
        ICard createdCard = null;
        if (Type == CardFactoryType.HandCard)
        {
            createdCard = handCardFactory.CreateCard();
        }
        else if(Type == CardFactoryType.DetailedCard)
        {
            createdCard = detailedCardFactory.CreateCard();
        }
        else if(Type == CardFactoryType.InfoCard)
        {
            createdCard = infoCardFactory.CreateCard();
        }
        createdCards.Add(createdCard);
        return createdCard;
    }
    

    public void Initialize()
    {
      
    }

    public void Shutdown()
    {
        
    }
}
