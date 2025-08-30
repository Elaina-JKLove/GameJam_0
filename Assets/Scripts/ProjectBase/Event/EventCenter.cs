using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    Event_None,
    Event_Input_HorizontalMovement,
    Event_Input_Jump,
}

public class EventCenter : Singleton<EventCenter>
{
    readonly Dictionary<EventType, UnityAction<object>> eventDic = new();

    //添加事件监听
    public void AddEventListener(EventType eventType, UnityAction<object> unityAction)
    {
        //如果事件存在，直接增加监听
        if (eventDic.ContainsKey(eventType)) eventDic[eventType] += unityAction;
        //否则创建此事件并监听
        else eventDic.Add(eventType, unityAction);
    }

    //移除事件监听
    public void RemoveEventListener(EventType eventType, UnityAction<object> unityAction)
    {
        //只有事件存在才能被移除
        if (eventDic.ContainsKey(eventType))
        {
            //如果不传入具体监听，则直接删除此事件
            if (unityAction == null) eventDic.Remove(eventType);
            //否则删除此事件的此监听
            else eventDic[eventType] -= unityAction;
        }
    }

    //事件触发
    public void EventTrigger(EventType eventType, object obj = null)
    {
        //如果事件存在则触发对应委托，触发者可通过object传递额外信息
        if (eventDic.ContainsKey(eventType)) eventDic[eventType]?.Invoke(obj);
    }

    public void Clear() => eventDic.Clear();
}
