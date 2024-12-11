using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandCard : MonoBehaviour
{
    public CardSpriteData cardSpriteData;

    public Image cardBackgroundImage;
    public Image cardImage;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;

    public void HandCardSetup(CardData cardData)
    {
        cardBackgroundImage.sprite = cardSpriteData.GetSpriteForHandCardType(cardData.cardType);
        cardImage.sprite = cardData.cardImage;
        atkText.text = cardData.atk.ToString(); 
        defText.text = cardData.def.ToString();
    }
}
