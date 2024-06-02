using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Commands.Level
{
    public class ObjectSpawnerCommand
    {
   
        private GameObject _sphere;
        private GameObject _spawner;

        
        internal ObjectSpawnerCommand(GameObject sphere, GameObject spawner)
        {
           
            _sphere = sphere;
            _spawner = spawner;
        }
        
        internal void Execute(ObjectType objectEnum)
        {
            SphereCollider sphereCollider = _sphere.GetComponent<SphereCollider>();
            float radius = sphereCollider.radius;
            Vector3 randomPointInUnitSphere = Random.insideUnitSphere;
            Vector3 randomPointInSphere = _sphere.transform.position + randomPointInUnitSphere * radius;

            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/ObjectPrefabs/{objectEnum}"), randomPointInSphere, Quaternion.identity,
                _spawner.gameObject.transform);






        }
      
    }
}