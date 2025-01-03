using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FusionMonster
{
    public List<string> monsterNames;
    public List<string> monsterSpecifications;
    public List<MonsterType> monsterType;
    public int minAtk;
}

[CreateAssetMenu(fileName = "FusionData", menuName = "ScriptableObjects/FusionData", order = 1)]
public class FusionData : ScriptableObject
{
    public CardData fusionCardData;

    [Header("Monster1")]
    public FusionMonster fusionMonster;
    [Header("Monster2")]
    public FusionMonster fusionMonster2;

}
