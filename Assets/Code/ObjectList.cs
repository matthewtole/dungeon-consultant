using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    [CreateAssetMenu]
    public class ObjectList : ScriptableObject
    {
        private readonly List<GameObject> _objects = new List<GameObject>();

        public void Add(GameObject obj)
        {
            if (!_objects.Contains(obj))
            {
                _objects.Add(obj);
            }
        }

        public void Remove(GameObject obj)
        {
            _objects.Remove(obj);
        }

        public GameObject[] ToArray()
        {
            return _objects.ToArray();
        }
    }
}