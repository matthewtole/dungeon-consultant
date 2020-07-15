using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DungeonManager {
    public class Modal : MonoBehaviour {
        
        public UnityEvent OnClose;

        public void Close() {
            Destroy(gameObject);
            OnClose.Invoke();
        }

        public void OnDrag(BaseEventData data) {
            PointerEventData eventData = (PointerEventData)data;
            transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        }
    }
}