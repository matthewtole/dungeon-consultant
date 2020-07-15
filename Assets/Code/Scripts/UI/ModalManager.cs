using DungeonManager;
using UnityEngine;

public class ModalManager : MonoBehaviour
{
    protected Modal activeModal = null;

    [SerializeField]
    protected Canvas canvas;

    [SerializeField]
    protected GameObject raiderModal;

    public void ShowRaiderModal() {
        GameObject modal = Instantiate(raiderModal, canvas.transform);
        activeModal = modal.GetComponent<Modal>();
    }
}
