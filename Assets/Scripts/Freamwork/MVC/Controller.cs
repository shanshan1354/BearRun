using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller
{
    //执行事件
    public abstract void Execute(object data);
    //获取Model
    protected T GetModel<T>()
        where T:Model
    {
       T t= MVC.GetModel<T>();
        return t;
    }
    //获取View
    protected T GetView<T>()
        where T : View
    {
        T t = MVC.GetView<T>();
        return t;
    }
    //注册Model
    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }
    //注册View
    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }
    //注册事件
    protected void RegisterController(string commandEvent, Type controllerType)
    {
        MVC.RegisterController(commandEvent, controllerType);
    }

}
