using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

[CreateAssetMenu(fileName = "CD_Collectable", menuName = "CD_Collectable/CD_Collectable", order = 0)]
public class CD_Collectable : ScriptableObject
{
    public MoneyData collectableData;
}