using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    public abstract string Name { get; }
    //事件列表
    [HideInInspector]
    public List<string> attentionList = new List<string>();
    //处理事件
    public abstract void HandleEvent(string eventName, object data);
    //注册事件列表
    public virtual void RegisterAttentionEvent()
    {

    }
    //获取Model
    protected T GetModel<T>()
        where T : Model
    {
        T t = MVC.GetModel<T>();
        return t;
    }
    //发送事件
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
