using System;
using Controllers.Player;
using Data.UnityObjects;
using Data.ValueObject;
using Signals;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private CharacterAnimationController animator;
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private CircleLineController circleLineController;
        [SerializeField] private GameObject player;
        #endregion
        
        private float _attackRange;
        public float Health { get; set; }
        public int collectedMoney;

        private void Awake()
        {
            player.SetActive(false);
        }

        private void OnEnable()
        {
            playerData = Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
            SubscribeEvents();
            Health = playerData.health;
            _attackRange = playerData.attackRange;
            SendDataToControllers();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelFailed += Death;
            CoreGameSignals.Instance.onLevelStart += OnLevelStart;
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelFailed -= Death;
            CoreGameSignals.Instance.onLevelStart -= OnLevelStart;
        }

        private void Death()
        {
            playerController.enabled = false;
            rigBuilder.enabled = false;
           animator.DeathAnimation(); 
        }

        private void SendDataToControllers()
        {
            circleLineController.Init(_attackRange);
            playerController.Init(_attackRange);
            
        }

        private void OnLevelStart()
        {
            player.SetActive(true);
        }
    }
}
