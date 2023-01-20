using Extension;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private Slider securitySlider;
        public int _securityLevel;
        public float SecurityLevel { get; private set; }
        [SerializeField] private Button startButton;
        public bool isGameStart;
       

        private void Awake()
        {
            Application.targetFrameRate = 60;
            OnLevelStart();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        void SubscribeEvents()
        {
            CoreGameSignals.Instance.onStealFinish += OnStealComplete;
            LevelSignals.Instance.onLevelCompleted += LevelCompleted;
        }

        void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onStealFinish -= OnStealComplete;
            LevelSignals.Instance.onLevelCompleted -= LevelCompleted;
        }

        void OnStealComplete()
        {
            Debug.Log("sinyal çalıştı");
            SecurityLevel -= 10;
            if (SecurityLevel <= 0)
            {
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
            }

            securitySlider.value = SecurityLevel;
        }

        void OnLevelStart()
        {
            SecurityLevel = 100;
            securitySlider.maxValue = SecurityLevel;
            securitySlider.value = SecurityLevel;
        }

        public void OnStartLevel()
        {
            CoreGameSignals.Instance.onLevelStart?.Invoke();
            startButton.gameObject.SetActive(false);
            isGameStart = true;
        }

        void LevelCompleted()
        {
            startButton.gameObject.SetActive(true);
            isGameStart = false;
            
        }
    }
}