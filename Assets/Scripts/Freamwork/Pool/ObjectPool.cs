using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool:MonoSingleton<ObjectPool>
{
    //资源目录文件夹路径
    public string resourceDir="";

    //预设名存储池子
    Dictionary<string, ChildPool> objectPool = new Dictionary<string, ChildPool>();

    public GameObject Spawn(string name, Transform trans)
    {
        if (!objectPool.ContainsKey(name))
        {
            NewChildPool(name, trans);
        }
        return objectPool[name].Spawn();
    }

    public void UnSpawn(GameObject go)
    {
        foreach (ChildPool childPool in objectPool.Values)
        {
            if (childPool.IsHaveItem(go))
            {
                childPool.UnSpawn(go);
            }
        }
    }

    public void UnSpawnAll()
    {
        foreach (ChildPool childPool in objectPool.Values)
        {
            childPool.UnSpawnAll();
        }
    }

    //添加一个新的子池子
    void NewChildPool(string name,Transform trans)
    {
        string path = resourceDir + "/"+ name;
        GameObject go = Resources.Load<GameObject>(path);
        ChildPool childPool = new ChildPool(go, trans);

        objectPool.Add(childPool.Name, childPool);
    }
}
