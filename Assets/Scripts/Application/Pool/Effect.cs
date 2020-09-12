using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : ReusableObject
{
    //等待时间
    float time = 1;
    
    public override void Spawn()
    {
        StartCoroutine("Recycle");
    }

    public override void UnSpawn()
    {
        StopAllCoroutines();
    }
    //等待1秒后进行回收
    IEnumerator Recycle()
    {
        yield return new WaitForSeconds(time);
        Game.Instance.objectPool.UnSpawn(gameObject);
    }
}
