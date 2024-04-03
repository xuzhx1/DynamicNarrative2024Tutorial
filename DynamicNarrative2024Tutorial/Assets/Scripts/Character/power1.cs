using UnityEngine;

public class JumpForceController : MonoBehaviour
{
    public GameObject heroObject; // ��Ҫ���Ƶ�����
    public float newJumpForce = 10f; // ��ײ�������Ծ����
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

            // ���ٽű������ӵ���Ϸ����
            Destroy(gameObject);
        }
    }
}