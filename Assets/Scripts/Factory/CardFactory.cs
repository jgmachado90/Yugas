using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour, ISubsystem
{
    public GameObject handCardPrefab;

    public GameObject CreateCard()
    {
        GameObject newCard = Instantiate(handCardPrefab);
        return newCard;
    }
    

    public void Initialize()
    {
      
    }

    public void Shutdown()
    {
        
    }
}
