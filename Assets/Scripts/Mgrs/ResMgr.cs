using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//资源管理器
public class ResMgr : MonoSingleton<ResMgr>
{
    //异步加载资源
    public void LoadRes<T>(string resName, UnityAction<T> unityAction = null) where T : Object
    {
        StartCoroutine(LoadResCor<T>(resName, unityAction));
    }

    IEnumerator LoadResCor<T>(string resName, UnityAction<T> unityAction = null) where T : Object
    {
        ResourceRequest rr = Resources.LoadAsync<T>(resName);
        yield return rr;

        //如果资源为GameObject则先实例化再返回
        if (rr.asset is GameObject) unityAction?.Invoke(GameObject.Instantiate(rr.asset) as T);
        else unityAction?.Invoke(rr.asset as T);
    }
}
