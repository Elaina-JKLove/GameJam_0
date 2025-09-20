using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//缓存池管理器
public class PoolMgr : Singleton<PoolMgr>
{
    Transform pool;//作为各类缓存池对象的父对象
    Dictionary<PoolObjType, PoolObj> Dic_Pool = new();



    #region  Public Methods

    public void GetObj(PoolObjType poolObjType, UnityAction<GameObject> unityAction = null)
    {
        if (Dic_Pool.ContainsKey(poolObjType) && Dic_Pool[poolObjType].poolList.Count > 0)
        {
            unityAction(Dic_Pool[poolObjType].GetObj());
        }
        else
        {
            //若缓存池中不存在所请求的对象，则生成一个对象并返回
            ResMgr.Instance.LoadRes<GameObject>("Prefabs/PoolObjs/" + poolObjType.ToString(), (obj) =>
            {
                obj.name = poolObjType.ToString();
                unityAction?.Invoke(obj);
            });
        }
    }

    public void PushObj(PoolObjType poolObjType, GameObject obj)
    {
        if (!pool) { pool = new GameObject("Pool").transform; }

        if (Dic_Pool.ContainsKey(poolObjType))
        {
            Dic_Pool[poolObjType].PushObj(obj);
        }
        else
        {
            //若缓存池中不存在所归还对象所属的List，则创建List并将其加入
            Dic_Pool.Add(poolObjType, new PoolObj(poolObjType, pool, obj));
        }
    }

    public void Clear()
    {
        Dic_Pool.Clear();
        pool = null;
    }

    #endregion
}

//缓存池对象类
public class PoolObj
{
    Transform parent;//作为此类缓存池对象的父对象
    public List<GameObject> poolList;



    #region  Public Methods

    public PoolObj(PoolObjType poolObjType, Transform pool, GameObject obj)
    {
        parent = new GameObject(poolObjType.ToString()).transform;
        parent.SetParent(pool);
        poolList = new List<GameObject>();
        PushObj(obj);
    }

    public GameObject GetObj()
    {
        GameObject obj = null;

        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.transform.SetParent(null);
        obj.SetActive(true);
        return obj;
    }

    public void PushObj(GameObject obj)
    {
        poolList.Add(obj);
        obj.transform.SetParent(parent);
        obj.SetActive(false);
    }

    #endregion
}
