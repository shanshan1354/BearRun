using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPool
{
    List<GameObject> childPool = new List<GameObject>();

    GameObject prefab;

    Transform parent;

    public string Name
    {
        get { return prefab.name; }
    }

    public ChildPool(GameObject gameObject,Transform trans)
    {
        prefab = gameObject;
        parent = trans;
    }

    //从池子里面取物体(初始化,添加进集合,显示)
    public GameObject Spawn()
    {
        GameObject item = null;
        foreach (GameObject child in childPool)
        {
            if (!child.activeSelf)
            {
                item = child;
            }
        }
        if (item==null)
        {
            item = GameObject.Instantiate(prefab);
            item.transform.parent = parent;
            childPool.Add(item);
        }
        item.SetActive(true);
        item.SendMessage("Spawn", SendMessageOptions.DontRequireReceiver);
        return item;
    }

    //回收物体(初始化脏数,隐藏)
    public void UnSpawn(GameObject item)
    {
        if (IsHaveItem(item))
        {
            item.SendMessage("UnSpawn", SendMessageOptions.DontRequireReceiver);

            item.SetActive(false);
        }
    }

    //回收所有的物体
    public void UnSpawnAll()
    {
        foreach (GameObject child in childPool)
        {
            if (child.activeSelf)
            {
                UnSpawn(child);
            }
        }
    }

    //判断集合中有无此物体
    public bool IsHaveItem(GameObject item)
    {
       return childPool.Contains(item);
    }
}
