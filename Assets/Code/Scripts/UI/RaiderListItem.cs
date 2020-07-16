using Code.Scripts.Raiders;
using TMPro;
using UnityEngine;

namespace Code.Scripts.UI
{
    public class RaiderListItem : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI textName;

        public RaiderListModal modal;

        private Raider _raider;

        public void SetRaider(Raider raider)
        {
            _raider = raider;
            textName.SetText(raider.characterName);
        }

        public void OnView()
        {
            modal.OnView(_raider);
        }
    }
}