using System;
using Controllers.Player;
using Signals;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private CharacterAnimationController animator;
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private PlayerController playerController;
        public int collectedMoney;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelFailed += Death;
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelFailed -= Death;
        }

        private void Death()
        {
            playerController.enabled = false;
            rigBuilder.enabled = false;
           animator.DeathAnimation(); 
        }
    }
}
