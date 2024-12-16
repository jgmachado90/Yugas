using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public Image cardTypeImage;

    public Image cardBackAnimation;

    [Header("CardValues")]
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public Image cardImage;
    public List<Image> levelStars;

    [Header("CardInfoValues")]
    public TextMeshProUGUI cardTypeText;
    public Image attributeImage;
    public TextMeshProUGUI descriptionText;
  
    [Header("GuardianStarInfo")]
    public TextMeshProUGUI guardianStarLabel;
    public TextMeshProUGUI guardianStarText1;
    public TextMeshProUGUI guardianStarText2;
    public Image guardianStarImage1;
    public Image guardianStarImage2;

    [Header("Animation")]
    public RectTransform cardRectTransform;
    public RectTransform cardInfoTransform;

    public RectTransform Cardp0;
    public RectTransform Cardp1;
    public RectTransform Infop0;
    public RectTransform Infop1;

    public void SetLevel(int level)
    {
        for (int i = 0; i < levelStars.Count; i++)
        {
            levelStars[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < level; i++) {
            levelStars[i].gameObject.SetActive(true);   
        }
    }

}
