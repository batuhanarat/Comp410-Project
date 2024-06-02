using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Runtime.Commands.Level;
using Runtime.Controllers.UI;
using Runtime.Data.UnityObjects;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        [SerializeField] private Transform levelHolder;

        [SerializeField] private byte totalLevelCount;
        
        #region Private Variables

        private OnLevelLoaderCommand _levelLoaderCommand;
        private OnLevelDestroyerCommand _levelDestroyerCommand;
        private ObjectSpawnerCommand _objectSpawner;
        private GameObject _transformOfSpawner;
        private GameObject _sphere;


        private short _currentLevel;
        private LevelData _levelData;

        #endregion
        
        #endregion
        
        private void Awake()
        {
            _levelData = GetLevelData();
            _currentLevel = GetActiveLevel();
            
            Init();
         
        }

        private void Init()
        {
            _levelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
            _levelDestroyerCommand = new OnLevelDestroyerCommand(levelHolder);

        }
        
        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/LevelData/CD_Level").Levels[(byte)(_currentLevel % totalLevelCount)];
        }

        private byte GetActiveLevel()
        {
            return (byte)_currentLevel;
        }
        
           private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += InitalizeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.onSpawnObjects += SpawnObjects;
            CoreGameSignals.Instance.onGetLevelData += GetLevelData;
            CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        }

        private LevelData OnGetLevelData()
        {
            return _levelData;
        }

        private void InitalizeLevel()
        {
            _levelLoaderCommand.Execute();
            _sphere = GameObject.FindWithTag("SphereSpawner");
            
            _transformOfSpawner =  GameObject.FindWithTag("Spawner");
            _objectSpawner = new ObjectSpawnerCommand(_sphere, _transformOfSpawner);
            _levelData = GetLevelData();
        }
    
        private void OnNextLevel()
        {
            _currentLevel++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
           // CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        }

        private void SpawnObjects()
        {
            
            int targetCount = _levelData.ClawObject.Targets.Count;
            int remainingCount = _levelData.ClawObject.RemainingObjects.Count;

            Dictionary<ObjectType, int> allObjects = new Dictionary<ObjectType, int>();
            for (int i = 0; i < targetCount; i++)
            {
                allObjects.Add(_levelData.ClawObject.Targets[i],
                    _levelData.ClawObject.TargetCounts[i]);
            }
            
            for (int j = 0; j < remainingCount; j++)
            {
                allObjects.Add(_levelData.ClawObject.RemainingObjects[j], 
                    _levelData.ClawObject.RemainingObjectsCounts[j]);
            }

            foreach (var _object in allObjects)
            {
                ObjectType objectEnum = _object.Key;
                int objectValue = _object.Value;
                for (int i = 0; i < objectValue; i++)
                {
                    _objectSpawner.Execute(objectEnum);

                }
            }

        }

        
        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
           // CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));

        }

        private byte OnGetLevelValue()
        {
            return (byte)((byte)_currentLevel % totalLevelCount);
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= InitalizeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.onSpawnObjects -= SpawnObjects;
            CoreGameSignals.Instance.onGetLevelValue -= OnGetLevelValue;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        { 
            //CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
        }

    }
}