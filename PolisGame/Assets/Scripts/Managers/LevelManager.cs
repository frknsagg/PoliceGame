using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Data.UnityObjects;
using Data.ValueObject;
using Enums;
using Extension;
using Signals;
using UnityEngine;
using UnityEngine.Rendering;

namespace Managers
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        [SerializeField] private EnemySpawnController enemySpawnController;
        private LevelData _levelData;
        private EnemyTypes _types;
        private int count;
        public int _levelId;
        public LevelInfoData _levelInfoData;
        private SerializedDictionary<EnemyTypes, int> _levelInfo;
        public List<GameObject> enemyList;

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnEnable()
        {
            SubscribeEvents();
            _levelData = Resources.Load<CD_Level>("Data/CD_Level").LevelData;
            // _types = _levelData.levelData.DeserializeKey(0);
            // count = _levelData.levelData[_types];
            var b = _levelId % _levelData.levelData.Count;
            _levelInfo = _levelData.levelData[b].data;
            _types = _levelInfo.DeserializeKey(0);
            count = _levelInfo[_types];
            Debug.Log(_levelInfo.Count);
        }

        void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelStart += GetLevelData;
            LevelSignals.Instance.onLevelCreate += LevelCreate;
            LevelSignals.Instance.onLevelCompleted += OnLevelComplete;
        }

        void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelStart -= GetLevelData;
            LevelSignals.Instance.onLevelCreate -= LevelCreate;
            LevelSignals.Instance.onLevelCompleted -= OnLevelComplete;
        }

        IEnumerator DayCompleteController()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                if (enemyList.Count == 0 && GameManager.Instance.isGameStart)
                {
                    LevelSignals.Instance.onLevelCompleted?.Invoke();
                }
            }
        }

        void GetLevelData()
        {
            Debug.Log($"type: {_types} ve sayısı: {count}");
            LevelSignals.Instance.onLevelCreate?.Invoke(_types, count);
            StartCoroutine(DayCompleteController());
        }

        void LevelCreate(EnemyTypes enemyTypes, int enemyCount)
        {
            for (int j = 0; j < _levelInfo.Count; j++)
            {
                // enemyCount = Random.Range(1, 3) * enemyCount*(_levelId+1);
                for (int i = 0; i < enemyCount; i++)
                {
                    enemySpawnController.SpawnEnemy(enemyTypes);
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void OnLevelComplete()
        {
            Debug.Log("level bitti");
            _levelId++;
            StopCoroutine(DayCompleteController());
        }
    }
}