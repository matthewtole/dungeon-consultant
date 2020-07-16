using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Code.Scripts.UI
{
    public class Modal : MonoBehaviour
    {

        [FormerlySerializedAs("OnClose")] public UnityEvent onClose;

        public void Close()
        {
            Destroy(gameObject);
            onClose.Invoke();
        }

        public void OnDrag(BaseEventData data)
        {
            PointerEventData eventData = (PointerEventData) data;
            transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        }
    }
}