using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScenes : Controller
{
    public override void Execute(object data)
    {
        ScenesArgs e = data as ScenesArgs;
        switch (e.scenesIndex)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                RegisterView(GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerMove>());
                RegisterView(GameObject.FindGameObjectWithTag(Tag.player).GetComponent<PlayerAnim>());
                break;
            default:
                break;
        }
    }
}
