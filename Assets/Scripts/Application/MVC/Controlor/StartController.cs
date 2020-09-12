using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : Controller
{
    public override void Execute(object data)
    {
        //注册Model
        RegisterModel(new GameModel());

        //注册Controller
        RegisterController(Consts.E_EnterScenes, typeof(EnterScenes));
        RegisterController(Consts.E_ExitScenes, typeof(ExitScenes));
    }
}
