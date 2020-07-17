using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Items.ShopEntries;
using Code.Scripts.UI;
using UnityEngine;

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
