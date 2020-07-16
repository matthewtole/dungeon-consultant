using System;
using Code.Scripts.Raiders;
using UnityEngine;

namespace Code.Scripts.UI
{
    public class RaiderListModal : MonoBehaviour
    {
        [SerializeField] protected GameObject itemPrefab;

        [SerializeField] protected Transform contentPanel;

        [SerializeField] protected Modal modal;

        [SerializeField] protected ObjectList raiderList;

        [SerializeField] protected GameObject infoPanel;

        private Canvas _canvas;

        void Start()
        {
            _canvas = GetComponentInParent<Canvas>();

            // TODO: Clean out the existing items in the panel
            Array.ForEach(raiderList.ToArray(), raider =>
            {
                GameObject item = Instantiate(itemPrefab, contentPanel);
                item.GetComponent<RaiderListItem>().SetRaider(raider.GetComponent<Raider>());
                item.GetComponent<RaiderListItem>().modal = this;
            });
        }

        public void OnView(Raider raider)
        {
            modal.Close();
            GameObject panel = Instantiate(infoPanel, _canvas.transform);
            panel.GetComponent<RaiderInfoPanel>().raider = raider;
        }
    }
}