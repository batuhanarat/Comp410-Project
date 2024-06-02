using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Core
{
    public class TornadoPowerUp: IPowerUp
    {
        private List<GameObject> _objects;
        private Vector3 _force = new Vector3(5, 5, 5);
        
        public TornadoPowerUp(List<GameObject> objects)
        {
            _objects = objects;
        }
        
        public void Execute()
        {
            foreach (var _object in _objects)
            {
                _object.GetComponent<Rigidbody>().AddForce(_force);
            }
        }
    }
}