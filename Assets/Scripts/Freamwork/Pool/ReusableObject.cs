using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReusableObject : MonoBehaviour, IReusable
{
    //取出物体
    public abstract void Spawn();
    //回收物体
    public abstract void UnSpawn();
}
