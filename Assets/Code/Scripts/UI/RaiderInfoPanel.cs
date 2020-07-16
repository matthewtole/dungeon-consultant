using Code.Scripts.Raiders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.UI
{
    public class RaiderInfoPanel : MonoBehaviour
    {
        [SerializeField] public Raider raider;

        [SerializeField] protected TextMeshProUGUI textName;
        [SerializeField] protected Camera followCameraPrefab;

        [SerializeField] protected Slider healthSlider;
        [SerializeField] protected Slider fearSlider;

        protected Camera followCamera;

        void Start()
        {
            textName.text = raider.characterName;
            followCamera = Instantiate(followCameraPrefab, raider.gameObject.transform);
            followCamera.transform.position += new Vector3(0, 0.3f, 0);
            healthSlider.maxValue = raider.maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            healthSlider.value = raider.currentHealth;
            fearSlider.value = raider.currentFear;
        }

        public void OnClose()
        {
            Destroy(followCamera);
        }
    }
}