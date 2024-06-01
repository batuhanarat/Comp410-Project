using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnLevelInitialize()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 0);
            LevelData? _levelData = CoreGameSignals.Instance.onGetLevelData?.Invoke();
            List<ObjectType> targetList = _levelData.Value.ClawObject.Targets;
            List<byte> targetValues = _levelData.Value.ClawObject.TargetCounts;
            UISignals.Instance.onTargetInitialized?.Invoke(targetList, targetValues);

        }

        private void OnLevelSuccessful()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
        }

        private void OnLevelFailed()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
        }

        public void NextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void RestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            
          
          //InputSignals.Instance.onEnableInput?.Invoke();
           // var currentLevel =  CoreGameSignals.Instance.onGetLevelValue?.Invoke();
        }

        public void Play()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(1);
            InputSignals.Instance.onEnableInput?.Invoke();
            
            
            
            var currentLevel =  CoreGameSignals.Instance.onGetLevelValue?.Invoke();
            Debug.Log("Current Level" +  currentLevel);
         
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            UISignals.Instance.onPlay?.Invoke();

            CoreGameSignals.Instance.onSpawnObjects?.Invoke();
            
          
        }

       

        private void OnReset()
        {
            CoreUISignals.Instance.onCloseAllPanels?.Invoke();
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
        }
    }
}