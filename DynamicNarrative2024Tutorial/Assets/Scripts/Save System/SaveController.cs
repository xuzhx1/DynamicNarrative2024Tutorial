using System;
using System.Collections;
using System.Collections.Generic;
using Save_System;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private float time;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的对象是否是角色
        if (collision.gameObject.CompareTag("Player") && Time.time - time > 1f)
        {
            time = Time.time;
            SaveManager.Instance.Save();
        }
    }
}
