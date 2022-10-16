using System;
using System.Collections.Generic;

public enum GameEvent
{
 OnDamageThief

}

public static class EventManager
{
    private static Dictionary<GameEvent, Action> eventTable
        = new Dictionary<GameEvent, Action>();

    public static void AddHandler(GameEvent gameEvent, Action action)
    {
        if (!eventTable.ContainsKey(gameEvent)) eventTable[gameEvent] = action;
        else eventTable[gameEvent] += action;
    }

    public static void RemoveHandler(GameEvent gameEvent, Action action)
    {
        if (eventTable[gameEvent] != null)
            eventTable[gameEvent] -= action;    
        if (eventTable[gameEvent] == null)
            eventTable.Remove(gameEvent);
    }

    public static void Broadcast(GameEvent even){
        if(eventTable[even] != null)
            eventTable[even]();
    }

}