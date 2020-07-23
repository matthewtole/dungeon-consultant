using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts
{
    
    [Serializable]
    public struct EventListener
    {
        public GameEvent gameEvent;
        public UnityEvent response;
    }
    public class GameEventListener : MonoBehaviour, IGameEventHandler
    {
        [SerializeField] protected EventListener[] listeners;

        private void OnEnable()
        {
            Array.ForEach(listeners, l => l.gameEvent.RegisterListener(this));
        }

        private void OnDisable()
        {
            Array.ForEach(listeners, l => l.gameEvent.UnregisterListener(this));
        }

        public  void OnEventRaised(GameEvent gameEvent)
        {
            EventListener[] matches = Array.FindAll(listeners, l => l.gameEvent.Equals(gameEvent));
            Array.ForEach(matches, l => l.response.Invoke());
        }
    }
}