using Enemy;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class StealBarController : MonoBehaviour
    {
        private EnemyData _data;
        private EnemyTypes _types;
        public Slider slider;
        public Gradient gradient;
        public Image fill;

        private float maxHealth;
     
        public Transform mLookAt;
        private Transform _localTrans;
        [SerializeField] private EnemyManager enemyManager;
    
        private void Start()
        {

            maxHealth = _data.EnemyTypeDatas[_types].TheftTime;
            SetMaxHealth(maxHealth);
            _localTrans = GetComponent<Transform>();
        }

        public void SetHealth(float time)
        {
            slider.value = time;
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
                _localTrans.LookAt(2*_localTrans.position-mLookAt.position);
            }
        }
    }
}
