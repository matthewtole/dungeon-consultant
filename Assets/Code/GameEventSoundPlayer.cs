using System;
using Code.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Code
{
    [Serializable]
    public struct EventSound
    {
        public GameEvent gameEvent;
        public AudioClip sound;
    }

    public class GameEventSoundPlayer : MonoBehaviour, IGameEventHandler
    {
        [SerializeField] protected AudioSource source;
        [SerializeField] protected EventSound[] sounds;

        private void OnEnable()
        {
            Array.ForEach(sounds, l => l.gameEvent.RegisterListener(this));
        }

        private void OnDisable()
        {
            Array.ForEach(sounds, l => l.gameEvent.UnregisterListener(this));
        }

        public void OnEventRaised(GameEvent gameEvent)
        {
            EventSound[] matches = Array.FindAll(sounds, l => l.gameEvent.Equals(gameEvent));
            Array.ForEach(matches, l => source.PlayOneShot(l.sound));
        }
    }
}