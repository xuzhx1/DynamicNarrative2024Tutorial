using UnityEngine;

public class floatingForceController : MonoBehaviour
{
    public GameObject heroObject; // 需要控制的物体
    public float newfloatingForce = 5f; // 碰撞后的新跳跃力度
    private PlayerMoveController playerMoveController;

    private void Start()
    {
        playerMoveController = heroObject.GetComponent<PlayerMoveController>();
    }

    private void Update()
    {
        float floatingForce = playerMoveController.floatingForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMoveController.floatingForce = newfloatingForce;

            // 销毁脚本所附加的游戏对象
            Destroy(gameObject);
        }
    }
}