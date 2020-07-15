using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonManager {
    [CreateAssetMenu]
    public class ObjectList : ScriptableObject {
        protected List<GameObject> objects = new List<GameObject>();

        public void Add(GameObject obj) {
            if (!objects.Contains(obj)) {
                objects.Add(obj);
            }
        }

        public void Remove(GameObject obj) {
            objects.Remove(obj);
        }

        public GameObject[] ToArray() {
            return objects.ToArray();
        }
    }
}