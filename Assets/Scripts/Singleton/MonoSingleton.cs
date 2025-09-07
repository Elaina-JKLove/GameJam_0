using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单例模式基类 - 需要挂载到对象上的单例
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
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
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {
                        GameObject gameObj = new GameObject();
                        instance = gameObj.AddComponent<T>();
                        gameObj.name = typeof(T).ToString();
                        DontDestroyOnLoad(gameObj);
                    }
                }
            }
            return instance;
        }
    }
}
