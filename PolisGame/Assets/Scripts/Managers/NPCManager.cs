using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

namespace Managers
{
    public class NPCManager : MonoBehaviour,IRobable
    {
        [SerializeField] private GameObject moneyPrefab;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public int GiveMoney()
        {
            return 10;
        }

        public void StealMoneyAnimation(Transform robberTrans)
        {
            var obj = Instantiate(moneyPrefab, transform.position, quaternion.identity);
            obj.transform.DOMove(robberTrans.position, 2f);
        }
    }
}
