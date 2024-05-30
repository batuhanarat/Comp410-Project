using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;

public class BagSignals : MonoBehaviour
{
    #region Singleton

    public static BagSignals Instance;

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
    

    public UnityAction<ObjectType> onItemSelected = delegate { };


  
}