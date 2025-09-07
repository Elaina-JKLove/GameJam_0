using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单例模式基类
public class Singleton<T> where T : new()
{
    static T instance;

    static object singletonLock = new object();//线程安全

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (singletonLock)
                {
                    if (instance == null) instance = new T();
                }
            }
            return instance;
        }
    }
}
