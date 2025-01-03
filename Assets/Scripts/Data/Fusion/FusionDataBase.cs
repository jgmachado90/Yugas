using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FusionDataBase", menuName = "ScriptableObjects/FusionDataBase", order = 1)]
public class FusionDataBase : ScriptableObject
{ 
   public List<FusionData> FusionData;
}
