using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoFactory : MonoBehaviour
{
    public CardSpriteData cardSpriteData;

    public GameObject cardInfoPrefab;
    private CardInfo cardInfo;

    private bool loaded;

    private void Start()
    {
        cardInfo = InstantiateCardInfo();
        cardInfo.gameObject.SetActive(false);
        //LoadCard(cardData);
        MatchEvents.onViewCardDetails += ViewCardDetails;
    }

    private void OnDestroy()
    {
        MatchEvents.onViewCardDetails -= ViewCardDetails;
    }

    private CardInfo InstantiateCardInfo()
    {
        GameObject card = Instantiate(cardInfoPrefab, transform);
        return card.GetComponent<CardInfo>();
    }

    public void ViewCardDetails(CardData cardData)
    {
        if (loaded)
        {
            loaded = false;
            cardInfo.gameObject.SetActive(false);
            return;
        }
        cardInfo.gameObject.SetActive(true);
        loaded = true;
        LoadCard(cardData);
    }


    private void LoadCard(CardData cardData)
    {
        Debug.Log("On load card");
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

        StartCoroutine("CardDetailsAnimation");
    }

    public IEnumerator CardDetailsAnimation()
    {
        cardInfo.cardBackAnimation.gameObject.SetActive(true);
        RectTransform rectTransform = cardInfo.cardTypeImage.rectTransform;
        float duration = 0.5f;
        int startX = 1;
        int endX = 0;
        float elapsedTime = 0f;

        // Get the current scale
        Vector3 currentScale = rectTransform.localScale;

        while (elapsedTime < duration)
        {
            // Lerp the scale
            float newX = Mathf.Lerp(startX, endX, elapsedTime / duration);
            rectTransform.localScale = new Vector3(newX, currentScale.y, currentScale.z);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        rectTransform.localScale = new Vector3(endX, currentScale.y, currentScale.z);
        cardInfo.cardBackAnimation.gameObject.SetActive(false);

        startX = 0;
        endX = 1;
        elapsedTime = 0f;
        currentScale = rectTransform.localScale;

        while (elapsedTime < duration)
        {
            // Lerp the scale
            float newX = Mathf.Lerp(startX, endX, elapsedTime / duration);
            rectTransform.localScale = new Vector3(newX, currentScale.y, currentScale.z);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

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
