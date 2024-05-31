using UnityEngine;
using Runtime.Enums;
using Runtime.Signals;
using System.Collections.Generic;

namespace Runtime.Controllers.UI
{
    public class BagUIController : MonoBehaviour
    {

        #region Serialized Variables

        [SerializeField] private List<RectTransform> bagObjectSpawnPoints = new List<RectTransform>();

        #endregion
        
        private void OnEnable() {
            
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

            UISignals.Instance.onAddToBag += OnAddToBag;
            UISignals.Instance.onBlast += OnBlast;
            
        }
          private void OnAddToBag(int index, ObjectType objectEnum) {

            RectTransform transform = bagObjectSpawnPoints[index];

            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/UIPrefabs/BagObjectPrefabs/{objectEnum}"), transform,
                false);
        }

        // reference
        private void OnBlast(List<ObjectType> bagArray) {
                    Debug.Log("first" +bagArray.Count);

            // delete all object ui
            foreach (var objectSpawnPoint in bagObjectSpawnPoints) {

                if (objectSpawnPoint.childCount <= 1) break;
                
                Object.Destroy(objectSpawnPoint.GetChild(1).gameObject);
            }
          
            Debug.Log("second" + bagArray.Count);

            // add all current object ui
            for (int i = 0; i < bagArray.Count; i++) {

                RectTransform transform = bagObjectSpawnPoints[i];
                ObjectType objectEnum = bagArray[i];
                Object.Instantiate(Resources.Load<GameObject>($"Prefabs/UIPrefabs/BagObjectPrefabs/{objectEnum}"), transform,
                    false);
            }
        } 

        private void UnSubscribeEvents()
        {

            UISignals.Instance.onAddToBag -= OnAddToBag;
            UISignals.Instance.onBlast -= OnBlast;

            
        }
          private void OnDisable() {
            
            UnSubscribeEvents();
        }

      
    }
}