using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : ReusableObject
{
    //撞击特效父物体
    Transform effectParent;

    public override void Spawn()
    {
    }

    public override void UnSpawn()
    {
    }

    private void Start()
    {
        effectParent = GameObject.Find("Effects").transform;
    }

    //撞到玩家
    void HitPlayer()
    {

        //播放撞击特效
        GameObject go= Game.Instance.objectPool.Spawn("FX_ZhuangJi", effectParent);
        go.transform.position = transform.position;
        //播放特效音
        Game.Instance.sound.PlayAudioEffect("Se_UI_Hit");
        //回收
        Game.Instance.objectPool.UnSpawn(gameObject);
    }
}
