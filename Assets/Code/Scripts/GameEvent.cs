using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public interface IGameEventHandler
    {
        void OnEventRaised(GameEvent gameEvent);
    }
    
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        private List<IGameEventHandler> listeners =
            new List<IGameEventHandler>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(this);
        }

        public void RegisterListener(IGameEventHandler listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventHandler listener)
        {
            listeners.Remove(listener);
        }
    }
}