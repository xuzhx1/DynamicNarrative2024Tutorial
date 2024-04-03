using UnityEngine;
using TMPro;

public class ActivateTMPOnCollision : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // ָ����TMP����

    private void OnCollisionEnter(Collision collision)
    {
        textMeshPro.gameObject.SetActive(true); // ����TMP����
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            textMeshPro.gameObject.SetActive(false); // ����TMP����
        }
    }
}