using System;
using Data.ValueObject;
using Enums;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour, ICollectable
    {
        private MoneyData moneyData;
        private CollectableTypes collectableTypes;

        private void OnEnable()
        {
            moneyData = Resources.Load<CD_Collectable>("Data/CD_Collectable").collectableData;
        }

        public int CollectMoney()
        {
            return moneyData.CollectableDatas[collectableTypes].MoneyCount;
        }
    }
}