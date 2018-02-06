using System;
using System.Collections;
using System.Collections.Generic;

public static class EventSystem {

    static Dictionary<string, List<Action<object[]>>> eventSystem;

    static EventSystem()
    {
        start();
    }

    public static void Register(string name,Action<object[]> action)
    {
        if (!eventSystem.ContainsKey(name))
        {
            eventSystem[name] = new List<Action<object[]>>();
        }
        eventSystem[name].Add(action);
    }
    public static void Dispatch(string name, object[] args)
    {
        if (!eventSystem.ContainsKey(name))
            return;
        foreach(var action in eventSystem[name])
        {
            action(args);
        }
    }
    public static void Remove(string name, Action<object[]> action)
    {
        if (!eventSystem.ContainsKey(name))
            return;
        eventSystem[name].Remove(action);
    }
    public static void start()
    {
        eventSystem = new Dictionary<string, List<Action<object[]>>>();
    }
}
