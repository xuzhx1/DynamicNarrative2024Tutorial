using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 缓存池容器
/// </summary>
public class PoolData
{
    //挂载的父节点
    public GameObject parent;

    public List<GameObject> poolList;

    public PoolData(GameObject obj,GameObject pool)
    {
        parent = new GameObject(obj.name);
        parent.transform.SetParent(pool.transform);
        poolList = new List<GameObject>();
        PushObject(obj);
    }

    /// <summary>
    /// 放回池中
    /// </summary>
    public void PushObject(GameObject obj)
    {
        // Debug.Log(obj.name);
        poolList.Add(obj);
        obj.transform.SetParent(parent.transform);
        obj.SetActive(false);
    }

    /// <summary>
    /// 获取物体
    /// </summary>
    public GameObject GetObject()
    {
        //Debug.Log("poolList前 " + poolList.Count);
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        //Debug.Log("poolList后 "+poolList.Count);
        return obj;
    }
}


/// <summary>
/// 缓冲池基础类
/// 采用单例
/// </summary>
public class PoolManager : BaseManager<PoolManager>
{
    public Dictionary<string, PoolData> dicPool = new Dictionary<string, PoolData>();
    public GameObject poolObj;
    /// <summary>
    /// 获取物体
    /// 异步加载
    /// 回调推荐使用lambda表达式
    /// </summary>
    public void GetObject(string name,UnityAction<GameObject> callback)
    {
        if(dicPool.ContainsKey(name) && dicPool[name].poolList.Count > 0)
        {
            callback(dicPool[name].GetObject());
        }
        else
        {
            ResManager.GetInstance().LoadAsync<GameObject>("Prefabs/" + name, (o) =>
            {
                o.name = name;
                callback(o);
            });
        }

    }

    /// <summary>
    /// 放回池中
    /// </summary>
    public void PushObject(string name, GameObject obj)    
    {
        if (poolObj == null)
        {
            // poolObj = new GameObject("Pool");
            poolObj = ResManager.GetInstance().Load<GameObject>("Prefabs/Pool/Pool");
        }

        if (dicPool.ContainsKey(name))
        {
            dicPool[name].PushObject(obj);
        }
        else
        {
            dicPool.Add(name, new PoolData(obj, poolObj));
        }
    }

    /// <summary>
    /// 清空缓存池
    /// </summary>
    public void Clear()
    {
        dicPool.Clear();
        poolObj = null;
    }
}
