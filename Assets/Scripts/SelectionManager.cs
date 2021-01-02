using UnityAtoms.BaseAtoms;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] protected GameObjectEvent selectionAddedEvent;
    [SerializeField] protected GameObjectEvent selectionRemovedEvent;

    void Start()
    {
        selectionAddedEvent.Register(OnSelectionAdded);
        selectionRemovedEvent.Register(OnSelectionRemoved);
    }

    private void OnDestroy()
    {
        selectionAddedEvent.Unregister(OnSelectionAdded);
        selectionRemovedEvent.Unregister(OnSelectionRemoved);
    }

    void OnSelectionAdded(GameObject selectable)
    {
        selectable.GetComponent<Selectable>().OnSelect();
    }

    void OnSelectionRemoved(GameObject selectable)
    {
        selectable.GetComponent<Selectable>().OnDeselect();
    }

}
