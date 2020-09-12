using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]
public class Game : MonoSingleton<Game>
{
    [HideInInspector]
    public ObjectPool objectPool;
    [HideInInspector]
    public Sound sound;
    [HideInInspector]
    public StaticData staticData;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnScenesLoaded;

        objectPool = ObjectPool.Instance;
        sound = Sound.Instance;
        staticData = StaticData.Instance;

        //注册StartController
        MVC.RegisterController(Consts.E_StartController, typeof(StartController));
        //启动StartController进行游戏初始化
        SendEvent(Consts.E_StartController);
        //跳转场景
        LoadLevel(4);
    }


    public void LoadLevel(int level)
    {
        ScenesArgs e = new ScenesArgs()
        {
            //获取当前场景索引
            scenesIndex = SceneManager.GetActiveScene().buildIndex
        };
        
        //发送退出场景事件
        SendEvent(Consts.E_ExitScenes, e);
        //加载新场景
        SceneManager.LoadScene(level,LoadSceneMode.Single);
    }

    //场景加载后调用
    private void OnScenesLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        ScenesArgs e = new ScenesArgs()
        {
            //获取当前场景索引
            scenesIndex = scene.buildIndex
        };
        
        //发送进入场景事件
        SendEvent(Consts.E_EnterScenes, e);
        SceneManager.sceneLoaded -= OnScenesLoaded;
    }

    void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
