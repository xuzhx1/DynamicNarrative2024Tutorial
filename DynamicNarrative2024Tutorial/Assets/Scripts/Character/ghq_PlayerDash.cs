using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghq_PlayerDash : MonoBehaviour
{
    
    //获取PlayerMoveController脚本
    private PlayerMoveController moveCtrl;
    //获取PlayerStatusController脚本
    private PlayerStatusController statusCtrl;
    //最大速度
    private float maxSpeed;
    
    //计时器
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        //获取PlayerMoveController脚本和PlayerStatusController脚本
        moveCtrl = GetComponent<PlayerMoveController>();
        statusCtrl = GetComponent<PlayerStatusController>();
        
        //获取玩家的最大速度
        maxSpeed = moveCtrl.maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //如果玩家的状态是LowEnergy，那么玩家的最大速度变成20，但是只有0.5秒
        if (PlayerStatusController.Instance.isLowEnergy)
        {
            //计时器开始计时
            timer += Time.deltaTime;
            //如果计时器大于0.5秒，那么玩家的速度还是5
            if (timer > 0.5f)
            {
                moveCtrl.maxSpeed = maxSpeed;
                //强制让此时玩家的速度小于最大速度
                if (moveCtrl.rb.velocity.x > moveCtrl.maxSpeed)
                {
                    moveCtrl.rb.velocity = new Vector2(moveCtrl.maxSpeed, moveCtrl.rb.velocity.y);
                }
            }
            //如果计时器小于0.5秒，那么玩家的速度变成20
            else
            {
                moveCtrl.maxSpeed = 30;

            }
        }
        
        //如果玩家的状态不是LowEnergy，那么玩家的最大速度变成5
        if (!PlayerStatusController.Instance.isLowEnergy)
        {
            moveCtrl.maxSpeed = 5;
            
            //强制让此时玩家的速度小于最大速度
            if (moveCtrl.rb.velocity.x > moveCtrl.maxSpeed)
            {
                moveCtrl.rb.velocity = new Vector2(moveCtrl.maxSpeed, moveCtrl.rb.velocity.y);
            }
            //计时器归零
            timer = 0;
        }
    }
}
