using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoCardController : MonoBehaviour
{
    CardFactory cardFactory;

    public CardSpriteData cardSpriteData;

    private CardInfo currentCardInfo;

    bool active = false;
    bool animating = false;

    private void Start()
    {
        cardFactory = SubsystemLocator.GetSubsystem<CardFactory>();
        MatchEvents.onViewCardDetails += ViewCardDetails;
    }

    private void OnDestroy()
    {
        MatchEvents.onViewCardDetails -= ViewCardDetails;
    }

    private CardInfo InstantiateCardInfo()
    {
        ICard createdInfoCard = cardFactory.CreateCard(CardFactoryType.InfoCard);
        GameObject card = (createdInfoCard as MonoBehaviour).gameObject;
        card.transform.SetParent(transform, false);
        return card.GetComponent<CardInfo>();
    }

    public void ViewCardDetails(CardData cardData)
    {
        if (animating) return;
        if (active){
            active = false;
            Destroy(currentCardInfo.gameObject);
            return;
        }
        currentCardInfo = InstantiateCardInfo();
        LoadCard(cardData);
        active = true;
    }


    private void LoadCard(CardData cardData)
    {
        Debug.Log("On load card");
        currentCardInfo.cardTypeImage.sprite = cardSpriteData.GetSpriteForCardType(cardData.cardType);
        currentCardInfo.cardImage.sprite = cardData.cardImage;
        currentCardInfo.cardNameText.text = cardData.cardName;
        currentCardInfo.cardTypeText.text = cardData.cardType.ToString();
        currentCardInfo.attributeImage.sprite = cardSpriteData.GetSpriteForAttribute(cardData.attribute);
        currentCardInfo.descriptionText.text = cardData.description;

        if (cardData.cardType != CardType.Monster) return;

        EnableMonsterLabels();

        currentCardInfo.atkText.text = "ATK: " + cardData.atk.ToString();
        currentCardInfo.defText.text = "DEF: " + cardData.def.ToString();

        // Definir as imagens dos Guardian Stars
        currentCardInfo.guardianStarImage1.sprite = cardSpriteData.GetSpriteForGuardianStar(cardData.primaryStar);
        currentCardInfo.guardianStarImage2.sprite = cardSpriteData.GetSpriteForGuardianStar(cardData.secondaryStar);

        currentCardInfo.guardianStarText1.text = cardData.primaryStar.ToString();
        currentCardInfo.guardianStarText2.text = cardData.secondaryStar.ToString();

        currentCardInfo.SetLevel(cardData.level);

        animating = true;
        StartCoroutine("CardGettingClose");
    }

    public IEnumerator CardGettingClose()
    {
        currentCardInfo.cardBackAnimation.gameObject.SetActive(true);
        float duration = 0.7f;
        Vector3 cardStart = currentCardInfo.Cardp0.position;
        Vector3 cardEnd = currentCardInfo.Cardp1.position;
        float elapsedTime = 0f;

        Vector3 cardCurrentPosition = currentCardInfo.cardRectTransform.position;
        Vector3 cardInfoCurrentPosition = currentCardInfo.cardInfoTransform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // Normalize time to [0, 1]

            // Apply easing function for smoother animation (ease out).
            t = t * t * (3f - 2f * t);

            currentCardInfo.cardRectTransform.position = Vector3.Lerp(currentCardInfo.Cardp0.position, currentCardInfo.Cardp1.position, t);
            currentCardInfo.cardInfoTransform.position = Vector3.Lerp(currentCardInfo.Infop0.position, currentCardInfo.Infop1.position, t);

            yield return null;
        }
        StartCoroutine("CardDetailsAnimation");
    }

    public IEnumerator CardDetailsAnimation()
    {
        RectTransform rectTransform = currentCardInfo.cardTypeImage.rectTransform;
        float duration = 0.3f;
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
        currentCardInfo.cardBackAnimation.gameObject.SetActive(false);

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
        animating = false;
    }
    

    private void EnableMonsterLabels()
    {
        currentCardInfo.atkText.enabled = true;
        currentCardInfo.defText.enabled = true;

        currentCardInfo.guardianStarLabel.enabled = true;

        currentCardInfo.guardianStarImage1.enabled = true;
        currentCardInfo.guardianStarImage2.enabled = true;

        currentCardInfo.guardianStarText1.enabled = true;
        currentCardInfo.guardianStarText2.enabled = true;
    }
}
