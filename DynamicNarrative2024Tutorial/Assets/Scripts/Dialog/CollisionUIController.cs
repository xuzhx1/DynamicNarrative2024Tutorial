using UnityEngine;
using TMPro;

public class ActivateTMPOnCollision : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // 指定的TMP对象

    private void OnCollisionEnter(Collision collision)
    {
        textMeshPro.gameObject.SetActive(true); // 激活TMP对象
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            textMeshPro.gameObject.SetActive(false); // 禁用TMP对象
        }
    }
}