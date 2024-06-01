using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Commands.Level
{
    public class ObjectSpawnerCommand
    {
        private Transform _objectSpawnLocation1;
        private Transform _objectSpawnLocation2;
        private Transform _objectSpawnLocation3;
        private Transform _objectSpawnLocation4;
        private Transform _objectSpawnLocation5;
        private GameObject _sphere;

        
        internal ObjectSpawnerCommand(Transform objectSpawnLocation1,
            Transform objectSpawnLocation2,
            Transform objectSpawnLocation3,
                Transform objectSpawnLocation4,
                Transform objectSpawnLocation5,
            GameObject sphere)
        {
            _objectSpawnLocation1 = objectSpawnLocation1;
            _objectSpawnLocation2 = objectSpawnLocation2;
            _objectSpawnLocation3 = objectSpawnLocation3;
            _objectSpawnLocation4 = objectSpawnLocation4;
            _objectSpawnLocation5 = objectSpawnLocation5;
            _sphere = sphere;
        }
        
        internal void Execute(ObjectType objectEnum)
        {
            SphereCollider sphereCollider = _sphere.GetComponent<SphereCollider>();
            float radius = sphereCollider.radius;
            Vector3 randomPointInUnitSphere = Random.insideUnitSphere;
            Vector3 randomPointInSphere = _sphere.transform.position + randomPointInUnitSphere * radius;

            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/ObjectPrefabs/{objectEnum}"), randomPointInSphere, Quaternion.identity,
                _sphere.gameObject.transform.parent);






        }
      
    }
}