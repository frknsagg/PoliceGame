using System;
using Data.ValueObject;
using Enums;
using UnityEngine.Rendering;

[Serializable]
public class MoneyData
{
    public SerializedDictionary<CollectableTypes, CollectableData> CollectableDatas;
}