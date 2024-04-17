using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public string destinationSceneName; // 目标场景的名称

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 确保只有玩家触发传送门
        {
            // 加载目标场景
            SceneManager.LoadScene(destinationSceneName);
        }
    }
}
