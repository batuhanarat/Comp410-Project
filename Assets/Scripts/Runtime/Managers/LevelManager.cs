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
        private GameObject _transformOfSpawner2; 
        private GameObject _transformOfSpawner3; 
        private GameObject _transformOfSpawner4; 
        private GameObject _transformOfSpawner5; 


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
            return Resources.Load<CD_Level>("Data/LevelData/CD_Level").Levels[_currentLevel];
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
            CoreGameSignals.Instance.onGetLevelData += OnGetLevelData;
            CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        }

        private LevelData OnGetLevelData()
        {
            return _levelData;
        }

        private void InitalizeLevel(byte level)
        {
            _levelLoaderCommand.Execute(level);

          if (level == 0)
            {
                _transformOfSpawner  = GameObject.FindWithTag("Spawner");
                _transformOfSpawner2  = GameObject.FindWithTag("Spawner2");
                _transformOfSpawner3  = GameObject.FindWithTag("Spawner3");
                _transformOfSpawner4  = GameObject.FindWithTag("Spawner4");
                _transformOfSpawner5  = GameObject.FindWithTag("Spawner5");
                GameObject sphere = GameObject.FindWithTag("SphereSpawner");
                    _objectSpawner = new ObjectSpawnerCommand(_transformOfSpawner.transform,_transformOfSpawner2.transform,
                _transformOfSpawner3.transform,_transformOfSpawner4.transform,_transformOfSpawner5.transform,sphere);
            }
            
            
            
             SpawnObjects();

        }

        private void OnNextLevel()
        {
            _currentLevel++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            //CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
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
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        }

        private byte OnGetLevelValue()
        {
            return (byte)((byte)_currentLevel % totalLevelCount);
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;
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
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
        }

    }
}