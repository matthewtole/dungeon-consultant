using Code.Scripts.Items;
using Code.Scripts.Items.ShopEntries;
using UnityEngine;

namespace Code.Scripts.UI
{
    public class ShopModal : MonoBehaviour
    {
        [SerializeField] protected Modal modal;
        [SerializeField] protected GameObject itemBuilderPrefab;

        public void OnBuyItem(ShopEntry entry)
        {
            modal.Close();
            GameObject builder = Instantiate(itemBuilderPrefab);
            builder.GetComponent<ItemBuilder>().Init(entry);
        }
    }
}
