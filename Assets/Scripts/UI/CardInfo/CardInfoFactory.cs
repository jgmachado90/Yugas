using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoFactory : MonoBehaviour
{
    public CardData cardData;
    public CardSpriteData cardSpriteData;

    public GameObject cardInfoPrefab;
    private CardInfo cardInfo;

    private void Start()
    {
        cardInfo = InstantiateCardInfo();
        LoadCard(cardData);
    }

    private CardInfo InstantiateCardInfo()
    {
        GameObject card = Instantiate(cardInfoPrefab, transform);
        return card.GetComponent<CardInfo>();
    }

    private void LoadCard(CardData cardData)
    {
        cardInfo.cardTypeImage.sprite = cardSpriteData.GetSpriteForCardType(cardData.cardType);
        cardInfo.cardImage.sprite = cardData.cardImage;
        cardInfo.cardNameText.text = cardData.cardName;
        cardInfo.cardTypeText.text = cardData.cardType.ToString();
        cardInfo.attributeImage.sprite = cardSpriteData.GetSpriteForAttribute(cardData.attribute);
        cardInfo.descriptionText.text = cardData.description;

        if (cardData.cardType != CardType.Monster) return;

        EnableMonsterLabels();

        cardInfo.atkText.text = "ATK: " + cardData.atk.ToString();
        cardInfo.defText.text = "DEF: " + cardData.def.ToString();

        // Definir as imagens dos Guardian Stars
        cardInfo.guardianStarImage1.sprite = cardSpriteData.GetSpriteForGuardianStar(cardData.primaryStar);
        cardInfo.guardianStarImage2.sprite = cardSpriteData.GetSpriteForGuardianStar(cardData.secondaryStar);

        cardInfo.guardianStarText1.text = cardData.primaryStar.ToString();
        cardInfo.guardianStarText2.text = cardData.secondaryStar.ToString();

        cardInfo.SetLevel(cardData.level);  
    }

    private void EnableMonsterLabels()
    {
        cardInfo.atkText.enabled = true;
        cardInfo.defText.enabled = true;

        cardInfo.guardianStarLabel.enabled = true;

        cardInfo.guardianStarImage1.enabled = true;
        cardInfo.guardianStarImage2.enabled = true;

        cardInfo.guardianStarText1.enabled = true;
        cardInfo.guardianStarText2.enabled = true;
    }
}
