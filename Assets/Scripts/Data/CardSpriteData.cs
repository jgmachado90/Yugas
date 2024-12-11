using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct SpritePair<TKey>
{
    public TKey key;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "CardSpriteData", menuName = "ScriptableObjects/CardSpriteData", order = 1)]
public class CardSpriteData : ScriptableObject
{
    public List<SpritePair<Attribute>> attributeSprites;
    public List<SpritePair<CardType>> cardTypeSprites;
    public List<SpritePair<GuardianStar>> guardianStarSprites;
    public List<SpritePair<CardType>> handCardTypeSprites;

    private Dictionary<Attribute, Sprite> attributeSpriteDictionary;
    private Dictionary<CardType, Sprite> cardTypeSpriteDictionary;
    private Dictionary<GuardianStar, Sprite> guardianStarSpriteDictionary;
    private Dictionary<CardType, Sprite> handCardTypeSpriteDictionary;

    private void OnEnable()
    {
        attributeSpriteDictionary = CreateDictionary(attributeSprites);
        cardTypeSpriteDictionary = CreateDictionary(cardTypeSprites);
        handCardTypeSpriteDictionary = CreateDictionary(handCardTypeSprites);
        guardianStarSpriteDictionary = CreateDictionary(guardianStarSprites);
    }

    private Dictionary<TKey, Sprite> CreateDictionary<TKey>(List<SpritePair<TKey>> spritePairs)
    {
        var dictionary = new Dictionary<TKey, Sprite>();
        foreach (var pair in spritePairs)
        {
            if (!dictionary.ContainsKey(pair.key))
            {
                dictionary[pair.key] = pair.sprite;
            }
        }
        return dictionary;
    }

    public Sprite GetSpriteForAttribute(Attribute attribute)
    {
        return GetSprite(attribute, attributeSpriteDictionary, nameof(Attribute));
    }

    public Sprite GetSpriteForCardType(CardType cardType)
    {
        return GetSprite(cardType, cardTypeSpriteDictionary, nameof(CardType));
    }

    public Sprite GetSpriteForHandCardType(CardType cardType)
    {
        return GetSprite(cardType, handCardTypeSpriteDictionary, nameof(CardType));
    }

    public Sprite GetSpriteForGuardianStar(GuardianStar guardianStar)
    {
        return GetSprite(guardianStar, guardianStarSpriteDictionary, nameof(GuardianStar));
    }

    private Sprite GetSprite<TKey>(TKey key, Dictionary<TKey, Sprite> dictionary, string keyTypeName)
    {
        if (dictionary.TryGetValue(key, out var sprite))
        {
            return sprite;
        }
        Debug.LogWarning($"No sprite found for {keyTypeName} {key}");
        return null;
    }
}
