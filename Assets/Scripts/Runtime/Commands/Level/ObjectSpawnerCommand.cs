using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using UnityEngine;

namespace Runtime.Commands.Level
{
    public class ObjectSpawnerCommand
    {
   
        private GameObject _sphere;

        
        internal ObjectSpawnerCommand(GameObject sphere)
        {
           
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