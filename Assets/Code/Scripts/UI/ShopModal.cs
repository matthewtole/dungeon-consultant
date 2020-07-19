using Code.Scripts.Items;
using Code.Scripts.Items.ShopEntries;
using UnityEngine;

namespace Code.Scripts.UI
{
    public class ShopModal : MonoBehaviour
    {
        [SerializeField] protected Modal modal;
        [SerializeField] protected GameObject itemBuilderPrefab;

        private GameObject _builder = null;

        public void OnBuyItem(ShopEntry entry)
        {
            if (_builder)
            {
                Destroy(_builder);
            }
            _builder = Instantiate(itemBuilderPrefab);
            _builder.GetComponent<ItemBuilder>().Init(entry);
        }
    }
}
