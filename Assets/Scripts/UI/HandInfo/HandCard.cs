using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandCard : MonoBehaviour
{
    public CardSpriteData cardSpriteData;

    public GameObject cardBack;

    public Image cardBackgroundImage;
    public Image cardImage;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;

    public TextMeshProUGUI fusionNumber;
    public Image fusionImage;
    public float fusionOffset = 0.4f;

    public void SelectForFusion(int number)
    {
        fusionImage.gameObject.SetActive(true);
        fusionNumber.text = number.ToString();

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        transform.position = new Vector3(x, y + fusionOffset, z);
    }

    public void CancelFusionSelection()
    {
        fusionImage.gameObject.SetActive(false);

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        transform.position = new Vector3(x, y - fusionOffset, z);
    }

    public void HandCardSetup(CardData cardData)
    {
        cardBackgroundImage.sprite = cardSpriteData.GetSpriteForHandCardType(cardData.cardType);
        cardImage.sprite = cardData.cardImage;
        atkText.text = cardData.atk.ToString(); 
        defText.text = cardData.def.ToString();
    }

    public void TurnBack()
    {
        StartCoroutine("TurnBackCoroutine");
    }

    public IEnumerator TurnBackCoroutine()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
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
        cardBack.SetActive(true);

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
}
