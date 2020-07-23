using UnityEngine;

namespace Code.Scripts.Game_Events
{
    public class GameEventTrigger : MonoBehaviour
    {
        [SerializeField] protected GameEvent gameEvent;

        public void Trigger()
        {
            gameEvent.Raise();
        }
    }
}
