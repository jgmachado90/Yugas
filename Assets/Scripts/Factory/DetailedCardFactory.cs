using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailedCardFactory : MonoBehaviour, IDetailedCardFactory
{
    public GameObject detailedCardPrefab;
    public ICard CreateCard()
    {
        GameObject newCard = Instantiate(detailedCardPrefab);
        ICard card = newCard.GetComponent<ICard>();
        return card;
    }
}
