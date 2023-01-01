using System;
using Data.UnityObjects;
using UnityEngine;


namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Level",menuName = "CD_Data/CD_Level",order = 2)]
    [Serializable]
    public class CD_Level : ScriptableObject
    {
        public LevelData LevelData;
    }
}
