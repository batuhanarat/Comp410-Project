using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Controllers.UI
{
    public class PlayerBagController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Transform> stageImages = new List<Transform>();

        #region Private Variables

        private List<ObjectType> _bagArray = new List<ObjectType>();
        private Dictionary< ObjectType, int > _bagHashMap= new Dictionary<ObjectType, int>();
        private int _currentIndex;
        private const int MAX_BAG_CAPACITY = 7;   
        private const int BLAST_REQUIRED_COUNT = 3;
        
        #endregion
        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void CheckBag()
        {
            Debug.Log($"hashmap");

            if (_bagHashMap == null || _bagHashMap.Count == 0)
            {
                Debug.Log($"The dictionary is empty.");

            }
            else
            {
                foreach (KeyValuePair<ObjectType, int> kvp in _bagHashMap)
                {
                    Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
                }
            }

            Debug.Log($"Array");

            if (_bagArray == null || _bagArray.Count == 0)
            {
                Debug.Log("This list is empty");
            }
            else
            {
            foreach (ObjectType item in _bagArray)
            {
                Debug.Log($"Item: {item}");
            }
            }
        }

        private void SubscribeEvents()
        {
            BagSignals.Instance.onItemSelected += OnAddToBag;
        }


        private void OnAddToBag(ObjectType objectEnum)
        {
            CheckBag();
            if (!_bagHashMap.ContainsKey(objectEnum))
             {
                _bagHashMap[objectEnum] = 0;
            }
            _bagHashMap[objectEnum]++;
            _bagArray.Insert(_currentIndex,objectEnum); 

            //Populate in bag ui
            UISignals.Instance.onAddToBag?.Invoke(_currentIndex, objectEnum);
            Debug.Log($"Populating in bag ui");

            _currentIndex++;

            if (_bagHashMap[objectEnum] == BLAST_REQUIRED_COUNT)
            {
                Blast( objectEnum);
            }
            else if(_currentIndex == MAX_BAG_CAPACITY){
                Debug.Log($"YOU LOSE");
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
            }

            CheckBag();


        }

        private void Blast(ObjectType objectEnum)
        {

            for (int i = 0; i < BLAST_REQUIRED_COUNT; i++)
            {
                _bagArray.Remove(objectEnum);
            }

            _currentIndex = _bagArray.Count;
            _bagHashMap[objectEnum] = 0;
            UISignals.Instance.onBlast?.Invoke(_bagArray);
            UISignals.Instance.onTargetChanged?.Invoke(objectEnum);

            Debug.Log($"Updating after blasting");
        }


        private void UnSubscribeEvents()
        {
            BagSignals.Instance.onItemSelected -= OnAddToBag;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}