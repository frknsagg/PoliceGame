using System;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Player",menuName = "CD_Player",order = 1)]

    [Serializable]
    public class CD_Player : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}
