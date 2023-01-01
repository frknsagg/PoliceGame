using System;
using Enums;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelInfoData
    {
        public SerializedDictionary<EnemyTypes, int> data;
    }
}
