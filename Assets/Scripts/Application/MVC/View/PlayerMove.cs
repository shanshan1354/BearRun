using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : View
{
    #region 字段

    //奔跑速度
    public float runSpeed = 20;
    //玩家左右移动位置偏移,向左X-2,,右边+2
    float xOffsetPos=0;
    //跳跃高度
    float jumpHight;
    //奔跑的距离
    float runDistance=0;
    //减速后的加速度
    float acceleratedSpeed;
    //翻滚时间
    float rollTime;
    //记录撞击之前的速度
    float speedBeforHit;

    //玩家位置索引,左边0,中间1,右边2
    int playerPosIndex=1;
    //目标位置索引,左边0,中间1,右边2
    int targetPosIndex = 1;

    //判断玩家是否输入
    bool activeInput = false;
    //判断玩家是否进行翻滚
    bool isRoll = false;
    //是否正在减速
    bool isDcreaseSpeed=false;
    //玩家动作(枚举)
    PlayerAction playerAction=PlayerAction.Null;

    //鼠标按下位置
    Vector3 mouseDownPos = Vector3.zero;
    //鼠标滑动位置
    Vector3 mousePos = Vector3.zero;

    #region 常量

    //左右移动速度
    const float xSpeed = 13;
    //最高跳跃高度
    const float yDistance = 5;
    //下落重力加速度
    const float gravity = 9.8f;
    //最大奔跑速度
    const float maxRunSpeed = 45;
    //每跑一段距离增加的速度
    const float addRunSpeed = 5;
    //每此增加速度需要奔跑的距离
    const float maxRunDistance = 200;

    #endregion

    #region 组件

    //角色控制器
    CharacterController character;
    GameModel gameModel;

    #endregion

    #endregion
    #region 属性
    public override string Name { get { return Consts.V_PlayerMove; } }
    #endregion
    #region Unity回调
    void Awake()
    {
        character = GetComponent<CharacterController>();
        gameModel = GetModel<GameModel>();
    }

    void Update()
    {
        
        //游戏开始,并且处于没有暂停的状态
        if (gameModel.IsPlay&&!gameModel.IsPause)
        {
            character.Move((Vector3.forward * runSpeed + new Vector3(0, jumpHight, 0)) * Time.deltaTime);
            runDistance += runSpeed * Time.deltaTime;
            //限制奔跑最大速度
            if (runDistance > maxRunDistance)
            {
                runDistance = 0;
                runSpeed += addRunSpeed;
                if (runSpeed > maxRunSpeed)
                {
                    runSpeed = maxRunSpeed;
                }
            }
            //滞空高度
            jumpHight -= gravity * Time.deltaTime;

            //判断玩家是否正在翻滚中
            IsInRolling();
            //输入控制
            playerAction = PlayerAction.Null;
            MouseInputDir();
            KeyBoardInputDir();
            //玩家操作动画
            PlayerAct();
            //玩家水平移动
            PlayerPos();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //如果撞到障碍物就减速,并实现撞击特效
        if (other.tag==Tag.smallBlock)
        {
            other.gameObject.SendMessage("HitPlayer");
            HitBlock();
        }
        else if (other.tag==Tag.bigBlock)
        {
            if (isRoll)
            {
                return;
            }
            other.gameObject.SendMessage("HitPlayer");
            HitBlock();
        }
    }
    #endregion

    #region 方法
    public override void HandleEvent(string eventName, object data)
    {

    }
    //手势输入
    void MouseInputDir()
    {
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            mouseDownPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
        }
        Vector3 mouseDir = mousePos - mouseDownPos;
        if (Mathf.Abs(mouseDir.magnitude) > 20 && activeInput == true)
        {
            if (Mathf.Abs(mouseDir.x)> Mathf.Abs(mouseDir.y)&&mouseDir.x>0)
            {
                //向右
                playerAction = PlayerAction.Right;
            }
            if (Mathf.Abs(mouseDir.x) > Mathf.Abs(mouseDir.y) && mouseDir.x < 0)
            {
                //向左
                playerAction = PlayerAction.Left;
            }
            if (Mathf.Abs(mouseDir.x) < Mathf.Abs(mouseDir.y) && mouseDir.y > 0)
            {
                //跳
                playerAction = PlayerAction.Jump;
            }
            if (Mathf.Abs(mouseDir.x) < Mathf.Abs(mouseDir.y) && mouseDir.y < 0)
            {
                //向下翻滚
                playerAction = PlayerAction.Roll;
            }
            activeInput = false;
        }

    }

    //键盘输入
    void KeyBoardInputDir()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //向左
            playerAction = PlayerAction.Left;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //向右
            playerAction = PlayerAction.Right;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            //跳 
            playerAction = PlayerAction.Jump;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //向下翻滚
            playerAction = PlayerAction.Roll;
        }
    }

    //玩家操作动画
    void PlayerAct()
    {
        switch (playerAction)
        {
            case PlayerAction.Null:
                break;
            case PlayerAction.Jump:
                //判断玩家是否在地面上
                if (character.isGrounded)
                {
                    jumpHight = yDistance;
                    SendMessage("AnimManager", playerAction);
                    //播放跳跃音效
                    Game.Instance.sound.PlayAudioEffect("Se_UI_Jump");
                }
                break;
            case PlayerAction.Roll:
                if (isRoll)
                    return;
                isRoll = true;
                rollTime = 0.733f;
                SendMessage("AnimManager", playerAction);
                //播放滚动音效
                Game.Instance.sound.PlayAudioEffect("Se_UI_Slide");
                break;
            case PlayerAction.Left:
                if (playerPosIndex>0)
                {
                    targetPosIndex--;
                    xOffsetPos = -2;
                    SendMessage("AnimManager", playerAction);
                    //播放向左移动音效
                    Game.Instance.sound.PlayAudioEffect("Se_UI_Huadong");
                }
                break;
            case PlayerAction.Right:
                if (playerPosIndex<2)
                {
                    targetPosIndex++;
                    xOffsetPos = 2;
                    SendMessage("AnimManager", playerAction);
                    //播放向右移动音效
                    Game.Instance.sound.PlayAudioEffect("Se_UI_Huadong");
                }
                break;
            default:
                break;
        }
        
    }
    //玩家水平移动
    void PlayerPos()
    {
        if (playerPosIndex!=targetPosIndex)
        {
            float x = Mathf.Lerp(0, xOffsetPos, xSpeed*Time.deltaTime);
            transform.position += new Vector3(x, 0, 0);
            xOffsetPos -= x;
            if (Mathf.Abs(xOffsetPos) <0.05f)
            {
                xOffsetPos = 0;
                playerPosIndex = targetPosIndex;
                switch (playerPosIndex)
                {
                    case 0:
                        transform.position = new Vector3(-2, transform.position.y, transform.position.z);
                        break;
                    case 1:
                        transform.position = new Vector3(0, transform.position.y, transform.position.z);
                        break;
                    case 2:
                        transform.position = new Vector3(2, transform.position.y, transform.position.z);
                        break;
                    default:
                        break;
                }
            }
            //限制玩家水平位置,确保不会移动到跑道外
            if (transform.position.x<-2)
            {
                transform.position= new Vector3(-2, transform.position.y, transform.position.z);
                playerPosIndex = 0;
            }
            if (transform.position.x>2)
            {
                transform.position = new Vector3(2, transform.position.y, transform.position.z);
                playerPosIndex = 2;
            }

        }
    }
    //玩家是否正在翻滚中
    void IsInRolling()
    {
        if (isRoll)
        {
            rollTime -= Time.deltaTime;
            if (rollTime < 0)
            {
                isRoll = false;
            }
        }
    }

    //减速
    void HitBlock()
    {
        if (isDcreaseSpeed)
            return;

        speedBeforHit = runSpeed;
        //让加速度也等于奔跑的速度
        acceleratedSpeed = runSpeed;
        runSpeed = 0;
        isDcreaseSpeed = true;
        StartCoroutine("DecreaseSpeed");
    }

    IEnumerator DecreaseSpeed()
    {
        while (runSpeed<=speedBeforHit)
        {
            runSpeed += acceleratedSpeed *Time.deltaTime;
            yield return 0;
        }
        runSpeed = speedBeforHit;
        isDcreaseSpeed = false;
    }
    #endregion
}
