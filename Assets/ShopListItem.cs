using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Items.ShopEntries;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ShopListItem : MonoBehaviour
{
    [SerializeField] protected ShopEntry entry;
    [SerializeField] protected TMP_Text labelText;
    [SerializeField] protected TMP_Text priceText;
    [SerializeField] protected Image iconImage;
    
    void Start()
    {
    }
    
#if UNITY_EDITOR
    private void Update()
    {
        if (entry && labelText && priceText && iconImage)
        {
            labelText.SetText(entry.label);
            priceText.SetText(entry.cost.ToString());
            iconImage.sprite = entry.shopImage;
        }
    }
#endif
}
