using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 玩家对象的Transform组件
    public float followRadius = 5f; // 角色开始跟随玩家的半径
    public float followSpeed = 2f; // 角色跟随玩家的速度

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
            // 计算角色向玩家移动的方向
            Vector3 direction = (player.position - transform.position).normalized;

            // 将角色沿着方向向玩家移动
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        // 在Scene视图中绘制一个圆形，表示跟随范围
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}
