using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCardFactory : MonoBehaviour, IInfoCardFactory
{
    public GameObject infoCardPrefab;
    public ICard CreateCard()
    {
        GameObject newCard = Instantiate(infoCardPrefab);
        ICard card = newCard.GetComponent<ICard>();
        return card;
    }
}
