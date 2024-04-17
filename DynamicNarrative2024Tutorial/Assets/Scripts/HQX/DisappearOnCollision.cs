using UnityEngine;

public class DisappearOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) // 碰撞对象是玩家
        {
            // gameObject.SetActive(false); // 将物体设置为不活跃状态，使其消失
            Destroy(gameObject);
        }
    }
}
