using System.Collections.Generic;

public static class GameEvents
{
    private static Dictionary<string, System.Action> _events 
        = new Dictionary<string, System.Action>();

    public static void Subscribe(string eventName, System.Action callback)
    {
        if (!_events.ContainsKey(eventName))
            _events[eventName] = null;
        _events[eventName] += callback;
    }

    public static void Unsubscribe(string eventName, System.Action callback)
    {
        if (_events.ContainsKey(eventName))
            _events[eventName] -= callback;
    }

    public static void Fire(string eventName)
    {
        if (_events.ContainsKey(eventName))
            _events[eventName]?.Invoke();
    }
}