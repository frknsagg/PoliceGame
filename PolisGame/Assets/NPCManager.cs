using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

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
