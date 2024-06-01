using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreGameSignals : MonoBehaviour
{
    #region Singleton

    public static CoreGameSignals Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #endregion

    public UnityAction onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public Func<LevelData> onGetLevelData = delegate { return new LevelData(); };
    public Func<byte> onGetLevelValue = delegate { return 0; };
    public UnityAction onSpawnObjects = delegate {};
  
}
