using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //玩家位置
    Transform playerTrans;
    //相机偏移量
    Vector3 offset;
    //目标位置
    Vector3 targetPos;
    //跟随速度
    float followSpeed = 10;
    void Awake()
    {
        playerTrans = GameObject.FindGameObjectWithTag(Tag.player).transform;
        
        offset = transform.position - playerTrans.position;
    }

    void Update()
    {
        targetPos = playerTrans.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
