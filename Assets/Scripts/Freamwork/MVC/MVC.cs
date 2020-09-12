using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MVC 
{
    public static Dictionary<string, Model> modelDict = new Dictionary<string, Model>();
    public static Dictionary<string, View> viewDict = new Dictionary<string, View>();
    public static Dictionary<string, Type> commandDict = new Dictionary<string, Type>();
    //注册Model
    public static void RegisterModel(Model model)
    {
        modelDict.Add(model.Name, model);
    }
    //注册View
    public static void RegisterView(View view)
    {
        if (viewDict.ContainsKey(view.Name))
        {
            viewDict.Remove(view.Name);
        }
        view.RegisterAttentionEvent();
        viewDict.Add(view.Name, view);
    }
    //注册事件
    public static void RegisterController(string commandEvent, Type controllerType)
    {
        commandDict.Add(commandEvent, controllerType);
    }
    //获取Model
    public static T GetModel<T>()
    where T:Model
    {
        foreach (var model in modelDict.Values)
        {
            if (model is T)
            {
                return (T)model;
            }
        }
        
        return null;
    }
    //获取View
    public static T GetView<T>()
    where T : View
    {
        foreach (var view in viewDict.Values)
        {
            if (view is T)
            {
                return (T)view;
            }
        }
        return null;
    }
    //发送事件
    public static void SendEvent(string eventName,object data=null)
    {
        //Controller执行
        if (commandDict.ContainsKey(eventName))
        {
            Type t = commandDict[eventName];
            Controller controller = Activator.CreateInstance(t) as Controller;
            controller.Execute(data);
        }

        //View执行
        foreach (var view in viewDict.Values)
        {
            if (view.attentionList.Contains(eventName))
            {
                view.HandleEvent(eventName, data);
            }
        }
    }
}
