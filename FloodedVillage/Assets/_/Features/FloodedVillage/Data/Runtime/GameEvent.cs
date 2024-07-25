using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Runtime
{
    [CreateAssetMenu(menuName = "Scriptables/ Events")]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener _listener)
        {
            if (!listeners.Contains(_listener))
            {
                listeners.Add(_listener);
            }
        }

        public void UnregisterListener(GameEventListener _listener)
        {
            if (listeners.Contains(_listener))
            {
                listeners.Remove(_listener);
            }
        }
    }

}
