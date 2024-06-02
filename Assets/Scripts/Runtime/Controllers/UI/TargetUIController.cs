using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class TargetUIController: MonoBehaviour
    {
        [SerializeField] private List<RectTransform> targetLocations = new List<RectTransform>();
        [SerializeField] private Dictionary<ObjectType,TextMeshProUGUI> targetHashmap = new Dictionary<ObjectType,TextMeshProUGUI>();
        [SerializeField] private List<TextMeshProUGUI> targetTexts = new List<TextMeshProUGUI>();
        private List<ObjectType> _targetsList = new List<ObjectType>();
        
        private void OnEnable() {
            
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

            UISignals.Instance.onTargetChanged += UpdateTarget;
            UISignals.Instance.onTargetInitialized += InitializedTargets;
            
        }

        private void InitializedTargets(List<ObjectType> targetEnumList, List<byte> targetValues)
        {
            _targetsList = targetEnumList;

            for (int i = 0; i < targetLocations.Count; i++)
            {
                RectTransform transform = targetLocations[i];
                ObjectType objectEnum = targetEnumList[i];
                Object.Instantiate(Resources.Load<GameObject>($"Prefabs/UIPrefabs/BagObjectPrefabs/{objectEnum}"), transform,
                    false);
                
                targetTexts[i].text = targetValues[i].ToString();
                targetHashmap[objectEnum] = targetTexts[i];

            }
            

        }

        private void CheckWin()
        {
          Debug.Log("Checking win");
            foreach (var text in targetTexts)
            {
                if (int.Parse(text.text.ToString()) != 0)
                {
                    return;
                }
            }
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
            InputSignals.Instance.onDisableInput?.Invoke();
            
            Debug.Log("You should win");

        }

        private void UpdateTarget(ObjectType objectEnum)
        {
            if(!_targetsList.Contains(objectEnum))
            {
                return;
            }
            
            TextMeshProUGUI newText = targetHashmap[objectEnum];
            int newValue =  int.Parse(newText.text.ToString());
            newValue = newValue - 3;
            newText.text = newValue.ToString();

            CheckWin();

        }
        private void UnSubscribeEvents()
        {

            UISignals.Instance.onTargetChanged -= UpdateTarget;
            UISignals.Instance.onTargetInitialized -= InitializedTargets;

        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}