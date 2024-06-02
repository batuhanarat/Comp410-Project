using UnityEngine;
using UnityEngine.Events;
using Runtime.Enums;
using System.Collections.Generic;

namespace Runtime.Signals
{
    public class UISignals: MonoBehaviour
    {
        #region Singleton

        public static UISignals Instance;

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
        
        public UnityAction onPlay = delegate { };
        public UnityAction<int, ObjectType> onAddToBag = delegate { };
        public UnityAction<List<ObjectType>> onBlast = delegate { };
        public UnityAction<List<ObjectType>, List<byte>> onTargetInitialized = delegate { };
        public UnityAction<ObjectType> onTargetChanged = delegate { };
        public UnityAction onTornadoPowerUpFired = delegate { };
        public UnityAction onMagnetPowerUpFired = delegate { };
        public UnityAction onTimerHelpPowerUpFired = delegate { };


        public UnityAction onRotateRight = delegate { };
        public UnityAction onRotateLeft = delegate { };



    }
}