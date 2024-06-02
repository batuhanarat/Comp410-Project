using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Signals;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;

    private float _timeLeft;
    private float _elapsedTime = 0;
    private bool _isRunning = false;
    private float _startTime = 100;

    private void Awake()
    {
        timerText.text = _startTime.ToString();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        UISignals.Instance.onPlay += StartTimer;
        CoreGameSignals.Instance.onLevelFailed += StopTimer;
        CoreGameSignals.Instance.onLevelSuccessful += StopTimer;
        UISignals.Instance.onTimerHelpPowerUpFired += ExtraSeconds;
    }

    private void ExtraSeconds()
    {
        _timeLeft += 10f;
    }


    private void StopTimer()
    {
        _isRunning = false;
    }

    private void StartTimer()
    {
        Debug.Log("Timer is running");

        _isRunning = true;
        _timeLeft = _startTime;
        
    }
    

    void Update()
    {
        if (_isRunning)
        {

            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= 1f)
            {
                _timeLeft -= 1f;
                _elapsedTime = 0f;
                timerText.text = _timeLeft.ToString();
            }

            if (_timeLeft == 0f)
            {
                _isRunning = false;
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
            }
            
        }
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        UISignals.Instance.onPlay -= StartTimer;
        CoreGameSignals.Instance.onLevelFailed -= StopTimer;
        UISignals.Instance.onTimerHelpPowerUpFired -= ExtraSeconds;

    }
}
