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

        //StartCoroutine("CardDetailsAnimation");
    }


    public IEnumerator CardDetailsAnimation()
    {
        // Set up the scale animation variables
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(1, 1, 0); // Scaling only the X-axis to 0
        float duration = 0.5f; // Half a second for the scale animation
        float elapsedTime = 0;

        // Shrink the card background (1 -> 0)
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            cardInfo.cardBackAnimation.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null; // Wait for the next frame
        }

        cardInfo.cardBackAnimation.transform.localScale = targetScale;
        cardInfo.cardBackAnimation.enabled = false; // Disable the animation

        // Reset elapsedTime for the reverse animation
        elapsedTime = 0;

        // Expand the card background back to original scale (0 -> 1)
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            cardInfo.cardBackAnimation.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null; // Wait for the next frame
        }

        cardInfo.cardBackAnimation.transform.localScale = originalScale; // Ensure it finishes at the original scale
        yield return null;  
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
