using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Consts
{
    //退出场景事件
    public const string E_ExitScenes = "E_ExitScenes";
    //进入场景事件
    public const string E_EnterScenes = "E_EnterScenes";
    public const string E_StartController = "E_StartController";

    //View
    public const string V_PlayerMove = "V_PlayerMove";
    public const string V_PlayerAnim = "V_PlayerAnim";
    //Model
    public const string M_GameModel = "M_GameModel";

}
//玩家动画
public enum PlayerAction
{
    Null,
    Jump,
    Roll,
    Left,
    Right
}
