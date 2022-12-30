using System;
using System.Collections;
using System.Collections.Generic;
using Extension;
using Signals;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Slider securitySlider;
    public int _securityLevel;
    public int SecurityLevel { get; private set; }

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
    }

    void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onStealFinish -= OnStealComplete;
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
}