using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HandCard : MonoBehaviour, IHandCard
{
    public CardSpriteData cardSpriteData;

    [SerializeField] private HandCardAnimator handCardAnimator;

    public Image cardBackgroundImage;
    public Image cardImage;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;

    public Image fusionImage;
    public TextMeshProUGUI fusionNumber;

    public void HandCardSetup(CardData cardData)
    {
        cardBackgroundImage.sprite = cardSpriteData.GetSpriteForHandCardType(cardData.cardType);
        cardImage.sprite = cardData.cardImage;
        atkText.text = cardData.atk.ToString(); 
        defText.text = cardData.def.ToString();
    }
    public void CardSetup(CardData cardData)
    {
        cardBackgroundImage.sprite = cardSpriteData.GetSpriteForHandCardType(cardData.cardType);
        cardImage.sprite = cardData.cardImage;
        atkText.text = cardData.atk.ToString();
        defText.text = cardData.def.ToString();
    }

    public void SelectForPlay()
    {
        handCardAnimator.TurnBack();
    }

    public void SelectForFusion(int _fusionNumber)
    {
        fusionImage.gameObject.SetActive(true);
        fusionNumber.text = _fusionNumber.ToString();
        handCardAnimator.SelectForFusion();
    }

    public void CancelFusionSelection()
    {
        fusionImage.gameObject.SetActive(false);
        handCardAnimator.CancelFusionSelection();
    }
}
