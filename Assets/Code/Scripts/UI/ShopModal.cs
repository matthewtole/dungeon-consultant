using Code.Scripts.Items;
using Code.Scripts.Items.ShopEntries;
using UnityEngine;

namespace Code.Scripts.UI
{
    public class ShopModal : MonoBehaviour
    {
        [SerializeField] protected Modal modal;

        private ShopEntry _currentlyBuilding;
        private GameObject _gameObject;
        
        public void OnBuyItem(ShopEntry entry)
        {
            if (_currentlyBuilding != null)
            {
                OnBuildCancel();
            }
            
            _gameObject = Instantiate(entry.prefab);
            MoveableItem moveable = _gameObject.GetComponent<MoveableItem>();
            if (!moveable)
            {
                Debug.LogError("Tried to build an item that isn't moveable!");
                Destroy(_gameObject);
                return;
            }

            _currentlyBuilding = entry;
            moveable.onMoveCompleted.AddListener(OnBuildComplete);
            moveable.onMoveCancelled.AddListener(OnBuildCancel);
            moveable.Move();
            
        }

        private void OnBuildCancel()
        {
            _currentlyBuilding = null;
            Destroy(_gameObject);
        }

        private void OnBuildComplete()
        {
            // TODO: Spend the money!
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ShopEntry tmp = _currentlyBuilding;
                _currentlyBuilding = null;
                OnBuyItem(tmp);
            }
            else
            {
                _currentlyBuilding = null;
            }
        }
    }
}
