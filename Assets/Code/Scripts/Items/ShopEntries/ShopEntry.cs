using UnityEngine;

namespace Code.Scripts.Items.ShopEntries
{
    [CreateAssetMenu]
    public class ShopEntry : ScriptableObject
    {
        public GameObject prefab;
        public int cost;
        public Sprite shopImage;
        public LayerMask buildLayers;
        public LayerMask noBuildLayers;
        public string label;
    }
}
