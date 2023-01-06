using System;
using Extension;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PrefsManager : MonoSingleton<PrefsManager>
    {
        void SaveLevelData(float security, int money, int levelId)
        {
            SaveMoney(money);
        }

        public void SaveMoney(int money)
        {
            var tempMoney = PlayerPrefs.GetInt("Money") + money;
            PlayerPrefs.SetInt("Money", tempMoney);
        }

        public void SaveSecurityLevel(float security)
        {
            PlayerPrefs.SetFloat("SecurityLevel", security);
        }

        public void SaveLevelId(int levelId)
        {
            PlayerPrefs.SetInt("LevelID", levelId);
        }

        public int GetMoney()
        {
            return PlayerPrefs.GetInt("Money");
        }

        public float GetSecurityLevel()
        {
            return PlayerPrefs.GetFloat("SecurityLevel");
        }

        public int GetLevelID()
        {
            return PlayerPrefs.GetInt("LevelID");
        }

        private void OnEnable()
        {
            PrefsSignals.Instance.OnSaveLevelData += SaveLevelData;
        }

        private void OnDisable()
        {
            PrefsSignals.Instance.OnSaveLevelData -= SaveLevelData;
        }
    }
}