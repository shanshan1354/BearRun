using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : Model
{
    #region 字段
    private bool isPlay = true;
    private bool isPause = false;
    #endregion

    #region 属性
    public override string Name { get { return Consts.M_GameModel; } }

    public bool IsPlay
    {
        get
        {
            return isPlay;
        }

        set
        {
            isPlay = value;
        }
    }

    public bool IsPause
    {
        get
        {
            return isPause;
        }

        set
        {
            isPause = value;
        }
    }
    #endregion
}
