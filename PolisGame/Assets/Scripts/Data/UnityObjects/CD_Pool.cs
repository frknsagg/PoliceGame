using Data.ValueObject;
using Enums;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Pool", menuName = "CD_Objects/CD_Pool", order = 3)]
    public class CD_Pool : ScriptableObject
    {
        public SerializedDictionary<PoolType, PoolValueData> PoolValueDatas;
    }
}