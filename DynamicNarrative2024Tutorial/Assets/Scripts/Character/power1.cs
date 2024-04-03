using UnityEngine;

public class JumpForceController : MonoBehaviour
{
    public GameObject heroObject; // 需要控制的物体
    public float newJumpForce = 10f; // 碰撞后的新跳跃力度
    private PlayerStatusController playerStatusController;
    private PlayerMoveController playerMoveController;

    private bool isColliding = false;

    private void Start()
    {
        playerMoveController = heroObject.GetComponent<PlayerMoveController>();
        playerStatusController = FindObjectOfType<PlayerStatusController>();
    }

    private void Update()
    {
        float floatingEnergy = playerStatusController.floatingEnergy;

        if (!isColliding)
        {
            playerMoveController.NewJumpForce = floatingEnergy / 10f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMoveController.NewJumpForce = newJumpForce;
            isColliding = true;

            // 销毁脚本所附加的游戏对象
            Destroy(gameObject);
        }
    }
}