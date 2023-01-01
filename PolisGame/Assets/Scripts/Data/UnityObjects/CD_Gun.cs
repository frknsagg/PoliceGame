using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Gun",menuName = "CD_Objects/CD_Gun", order = 1)]
    public class CD_Gun : ScriptableObject
    {
        public GunData GunData;
    }
}
