using UnityEngine;

public class floatingForceController : MonoBehaviour
{
    public GameObject heroObject; // ��Ҫ���Ƶ�����
    public float newfloatingForce = 5f; // ��ײ�������Ծ����
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

            // ���ٽű������ӵ���Ϸ����
            Destroy(gameObject);
        }
    }
}