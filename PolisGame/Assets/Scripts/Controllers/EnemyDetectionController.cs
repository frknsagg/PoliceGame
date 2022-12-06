using UnityEngine;

namespace Controllers
{
    public class EnemyDetectionController : MonoBehaviour
    {
        private EnemyManager _manager;
        private void Awake()
        {
            _manager = gameObject.GetComponentInParent<EnemyManager>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController player))
            {
                _manager.PlayerTarget = player.transform;
            }
            if (other.TryGetComponent(out NPCManager manager))
            {
                _manager.RobbableTargets.Add(manager.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController player))
            {
                _manager.PlayerTarget = null;
            }
            if (other.TryGetComponent(out NPCManager manager))
            {
                _manager.RobbableTargets.Remove(manager.transform);
            }
        }
    }
}
