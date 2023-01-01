using Enemy;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class HealthBarController : MonoBehaviour
    {
        private EnemyData _data;
        private EnemyTypes _types;
        public Slider slider;
        public Gradient gradient;
        public Image fill;

        private float maxHealth;
     
        public Transform mLookAt;
        [SerializeField] private EnemyManager enemyManager;
    
        private void OnEnable()
        { 
            maxHealth = _data.EnemyTypeDatas[_types].Health;
            SetMaxHealth(maxHealth);
            
        }

        public void SetHealth()
        {
            slider.value = enemyManager.Health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
        private void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;

            fill.color = gradient.Evaluate(1f);
        }

        public void GetData(EnemyData data,EnemyTypes types)
        {
            _data = data;
            _types = types;
        }

        private void FixedUpdate()
        {
            if (mLookAt)
            {
                transform.LookAt(2*transform.position-mLookAt.position);
            }
        }
    }
}
