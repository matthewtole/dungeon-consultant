using UnityEngine;

namespace Code.Scripts.UI
{
    public class ModalLauncher : MonoBehaviour
    {
        [SerializeField] protected GameObject modalPrefab;
        [SerializeField] protected Canvas canvas;

        public void Launch()
        {
            Instantiate(modalPrefab, canvas.transform);
        }
    }
}
