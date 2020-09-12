using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : View
{
    Animation anim;
    Action playAnim;

    public override string Name { get { return Consts.V_PlayerAnim; } }

    private void Awake()
    {
        anim = GetComponent<Animation>();
        playAnim = PlayRun;
    }
    private void Update()
    {
        if (playAnim!=null)
        {
            playAnim();
        }

    }

    //跳
    void PlayRun()
    {
        anim.Play("run");
    }
    //跳
    void PlayJump()
    {
        anim.Play("jump");
        if (anim["jump"].normalizedTime > 0.95f)
        {
            playAnim = PlayRun;
        }
    }
    //翻滚
    void PlayRoll()
    {
        anim.Play("roll");
        if (anim["roll"].normalizedTime > 0.95f)
        {
            playAnim = PlayRun;
        }
    }
    //左跳
    void PlayLeft()
    {
        anim.Play("left_jump");
        if (anim["left_jump"].normalizedTime > 0.95f)
        {
            playAnim = PlayRun;
        }
    }
    //右跳
    void PlayRight()
    {
        anim.Play("right_jump");
        if (anim["right_jump"].normalizedTime > 0.95f)
        {
            playAnim = PlayRun;
        }
    }

    void AnimManager(PlayerAction action)
    {
        switch (action)
        {
            case PlayerAction.Null:
                break;
            case PlayerAction.Jump:
                playAnim = PlayJump;
                break;
            case PlayerAction.Roll:
                playAnim = PlayRoll;
                break;
            case PlayerAction.Left:
                playAnim = PlayLeft;
                break;
            case PlayerAction.Right:
                playAnim = PlayRight;
                break;
            default:
                break;
        }
    }

    public override void HandleEvent(string eventName, object data)
    {
        throw new NotImplementedException();
    }
}
