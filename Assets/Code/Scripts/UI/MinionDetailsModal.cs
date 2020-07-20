using Code.Scripts.Minions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UI
{
    public class MinionDetailsModal : MonoBehaviour
    {
        [SerializeField] public Minion minion;

        [SerializeField] protected TextMeshProUGUI textName;
        [SerializeField] protected GameObject followCameraPrefab;

        private GameObject _followCamera;

        void Start()
        {
            textName.text = minion.Name;
            _followCamera = Instantiate(followCameraPrefab, minion.gameObject.transform);
            _followCamera.transform.position += new Vector3(0, 0.2f, 0);
        }

        public void OnClose()
        {
            Destroy(_followCamera);
        }
    }
}
