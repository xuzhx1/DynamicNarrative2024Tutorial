using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPoint : MonoBehaviour
{
    public float engeryNum;

    private Animator anim;
    private bool isCharged;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ResetEnergyPoint()
    {
        StartCoroutine(LateReset());
    }

    IEnumerator LateReset()
    {
        yield return new WaitForSeconds(1.2f);

        isCharged = false;
        anim.SetBool("Charged", false);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的对象是否是角色
        if (collision.gameObject.CompareTag("Player"))
        {

            if(!isCharged)
            {
                isCharged = true;

                PlayerStatusController.Instance.ChargeEnergy(engeryNum);

                // 动画
                anim.SetBool("Charged", true);

                // 声音
                MusicPlayer.Instance.PlaySound(SoundType.ChargeEnergy);
            }
            
            
        }
    }



    private void OnEnable()
    {
        PlayerStatusController.OnCharacterDeath += ResetEnergyPoint;
    }

    private void OnDisable()
    {
        PlayerStatusController.OnCharacterDeath -= ResetEnergyPoint;
    }

}
