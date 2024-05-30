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

        [SerializeField] private GameObject cakePrefab;
        #endregion
        
        private void OnEnable() {
            
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

            UISignals.Instance.onAddToBag += OnAddToBag;
            
        }
          private void OnAddToBag(int index, ObjectType objectEnum) {

            RectTransform transform = bagObjectSpawnPoints[index];

            Object.Instantiate(cakePrefab, transform.position,transform.rotation, transform);        
        }

        // reference
        private void OnBlast(List<ObjectType> bagArray) {

            // delete all object ui
            foreach (var objectSpawnPoint in bagObjectSpawnPoints) {

                if (objectSpawnPoint.transform.childCount <= 0) return;
                Object.Destroy(objectSpawnPoint.transform.GetChild(0).gameObject);
            }

            // add all current object ui
            for (int i = 0; i < bagArray.Count; i++) {

                RectTransform transform = bagObjectSpawnPoints[i];

                Object.Instantiate(cakePrefab, transform.position,transform.rotation, transform); 
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