using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLoadScene : MonoBehaviour
{
    public string targetScene; // 目标场景的名称

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的对象是否是角色
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("YOU WIN");

            PlayerStatusController.Instance.PauseMove();

            // Play sound
            MusicPlayer.Instance.PlaySound(SoundType.Win);

            // Hero Animation
            PlayerStatusController.Instance.anim.PlayAnimation_Win();
            anim.SetBool("Passed",true);

            StartCoroutine(PlaySceneEffect());
        }
    }

    IEnumerator PlaySceneEffect()
    {
        yield return new WaitForSeconds(1f);

        // 播放传送门特效
        SceneEffectController.Instance.CameraResetEffect_IN();

        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);

        // 停止声音
        MusicManager.GetInstance().StopAllSound();

        // 切换到目标场景
        ScenesMgr.GetInstance().LoadSceneAsyn(targetScene, () =>
        {
        });

    }


}
