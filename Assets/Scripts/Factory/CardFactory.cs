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

    private Dictionary<CardFactoryType, ICardFactory> factoryMap;

    public void Initialize()
    {
        factoryMap = new Dictionary<CardFactoryType, ICardFactory>
        {
            { CardFactoryType.HandCard, handCardFactory },
            { CardFactoryType.DetailedCard, detailedCardFactory },
            { CardFactoryType.InfoCard, infoCardFactory }
        };
    }

    public ICard CreateCard(CardFactoryType type)
    {
        if (factoryMap.ContainsKey(type))
            return factoryMap[type].CreateCard();
        return null;
    }

    public void Shutdown()
    {
        
    }
}
