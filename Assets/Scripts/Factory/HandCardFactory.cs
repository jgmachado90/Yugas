using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardFactory : MonoBehaviour, IHandCardFactory
{
    public GameObject handCardPrefab;
    public ICard CreateCard()
    {
        GameObject newCard = Instantiate(handCardPrefab);
        ICard card = newCard.GetComponent<ICard>();
        return card;
    }
}
