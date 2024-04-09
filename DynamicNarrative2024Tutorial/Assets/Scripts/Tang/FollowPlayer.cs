using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 玩家对象的Transform组件
    public float followRadius = 5f; // 角色开始跟随玩家的半径
    public float followSpeed = 2f; // 角色跟随玩家的速度
    public Vector3 followOffset; // 跟随位置偏移

    private bool isFollowing = false; // 是否正在跟随玩家

    void Update()
    {
        // 计算角色与玩家之间的距离
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 如果玩家进入了跟随范围，开始跟随玩家
        if (distanceToPlayer <= followRadius)
        {
            isFollowing = true;
        }
        else
        {
            isFollowing = false;
        }

        // 如果正在跟随玩家，更新角色位置
        if (isFollowing)
        {
            // 计算目标位置，加上跟随位置偏移
            Vector3 targetPosition = player.position + followOffset;

            // 将角色朝着目标位置移动S
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        // 在Scene视图中绘制一个圆形，表示跟随范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
