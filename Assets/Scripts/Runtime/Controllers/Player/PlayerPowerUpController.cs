using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Runtime.Controllers.UI
{
    public class PlayerPowerUpController: MonoBehaviour
    {
        #region Singleton

        public static PlayerPowerUpController Instance;

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
        
        [SerializeField] private List<Transform> powerUpPanelTransforms = new List<Transform>();

        private void OnEnable()
        {
            SubscribeMethods();
        }

        private void SubscribeMethods()
        {
            UISignals.Instance.onTornadoPowerUpFired += UseTornadoPowerUp;
            UISignals.Instance.onTimerHelpPowerUpFired += UseExtraTimePowerUp;
            UISignals.Instance.onMagnetPowerUpFired += UseMagnetPowerUp;
            
        }

        private void UseMagnetPowerUp()
        {
            Dictionary<ObjectType, int> uniqueChildsWithThreeElems = new Dictionary<ObjectType, int>();

            for (int i = 0; i < transform.childCount - 2; i++)
            {
                ObjectType childEnum;
                Enum.TryParse(transform.GetChild(i).tag, out childEnum);
                
                if (uniqueChildsWithThreeElems.ContainsKey(childEnum)) continue;

                ObjectType child2Enum;
                Enum.TryParse(transform.GetChild(i + 1).tag, out child2Enum);
                
                if (child2Enum != childEnum) continue;

                ObjectType child3Enum;
                Enum.TryParse(transform.GetChild(i + 2).tag, out child3Enum);
                
                if (child3Enum != childEnum) continue;

                uniqueChildsWithThreeElems[childEnum] = i;
            }

            if (uniqueChildsWithThreeElems.Count == 0) return;
            KeyValuePair<ObjectType, int> selectedKeyValue = GetRandomKeyValuePair(uniqueChildsWithThreeElems);

            Transform deletedObject1 = transform.GetChild(selectedKeyValue.Value);
            Destroy(deletedObject1.gameObject);
            
            Transform deletedObject2 = transform.GetChild(selectedKeyValue.Value + 1);
            Destroy(deletedObject2.gameObject);
            Transform deletedObject3 = transform.GetChild(selectedKeyValue.Value + 2);
            Destroy(deletedObject3.gameObject);
            
            
            UISignals.Instance.onTargetChanged?.Invoke(selectedKeyValue.Key);

            /*
            int objectIndex  =  transform.childCount / 3; 
            int randomInt = Random.Range(0,objectIndex);
            
            ObjectType myEnum;
            Transform deletedObject1 = transform.GetChild(randomInt * 3 );
            Enum.TryParse(deletedObject1.tag, out myEnum);

            Destroy(deletedObject1.gameObject);

            Transform deletedObject2 = transform.GetChild(randomInt * 3 + 1);
            Destroy(deletedObject2.gameObject);

            Transform deletedObject3 = transform.GetChild(randomInt * 3 + 2);
            Destroy(deletedObject3.gameObject);

            UISignals.Instance.onTargetChanged?.Invoke(myEnum);
            */

        }
        
        private KeyValuePair<ObjectType, int> GetRandomKeyValuePair(Dictionary<ObjectType, int> dictionary)
        {
            // Convert the keys of the dictionary to a list
            List<ObjectType> keys = new List<ObjectType>(dictionary.Keys);

            // Get a random index
            int randomIndex = Random.Range(0, keys.Count);

            // Use the random index to get a random key
            ObjectType randomKey = keys[randomIndex];

            // Get the corresponding value
            int randomValue = dictionary[randomKey];

            // Return the random key-value pair
            return new KeyValuePair<ObjectType, int>(randomKey, randomValue);
        }

        private void UseExtraTimePowerUp()
        {
            Debug.Log("naber");
            
        }

        private PowerUpType[] powerUps = new PowerUpType[]
        {
            PowerUpType.Tornado,
            PowerUpType.Dynamite,
            PowerUpType.Magnet
        };

        private Dictionary<PowerUpType, int> powerUpWithValues = new Dictionary<PowerUpType, int>()
        {  
            {PowerUpType.Tornado,3},
            {PowerUpType.Dynamite,3},
            {PowerUpType.Magnet,3}
        };

        public void UseTornadoPowerUp()
        {
            // Log the start of the method
            Debug.Log("UseTornadoPowerUp method called.");

            // Check if there are children
            if (transform.childCount == 0)
            {
                Debug.LogWarning("No children found under this GameObject.");
                return;
            }

            // Iterate over each child and apply force if they have a Rigidbody
            foreach (Transform child in transform)
            {
                
                float randomFloat = Random.Range(10f, 20.0f);
                
                Vector3 randomDirection = Random.insideUnitSphere.normalized;

                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Debug.Log($"Applying force to {child.name}.");
                    rb.AddForce(new Vector3(randomDirection.x,Math.Abs(randomDirection.y), randomDirection.z) * randomFloat,
                        ForceMode.Impulse);
                }
                else
                {
                    Debug.LogWarning($"{child.name} does not have a Rigidbody component.");
                }
            }
        }

        
        public void UsePowerUp(PowerUpType powerUp)
        {
            switch (powerUp)
            {
                case  PowerUpType.Tornado:
                    foreach (Transform child in transform)
                    {
                        child.GetComponent<Rigidbody>().AddForce(Vector3.up * 10.0f);
                    }
                    break;
                case  PowerUpType.Dynamite:
                    
                    break;
                
                case  PowerUpType.Magnet:
                    
                    break;
                
                default:
                    break;
                
            }
            
        }
        private void OnDisable()
        {
            UnSubscribeMethods();
        }

        private void UnSubscribeMethods()
        {
            UISignals.Instance.onTornadoPowerUpFired -= UseTornadoPowerUp;
            UISignals.Instance.onTimerHelpPowerUpFired -= UseExtraTimePowerUp;
            UISignals.Instance.onMagnetPowerUpFired -= UseMagnetPowerUp;
        }

    }
}