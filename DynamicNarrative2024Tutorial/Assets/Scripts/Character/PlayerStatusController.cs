using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public float floatingEnergy = 100f;
    public float floatingEnegyCost; //能量消耗速度
    public float lowEnergyNum = 30f;
    

    public delegate void CharacterDeath();
    public static event CharacterDeath OnCharacterDeath;
    public delegate void PlayerReset();
    public static event PlayerReset OnPlayerReset;

    private bool isLowEnergy;
    private bool isDead;
    private Vector3 birthPos;
    private PlayerMoveController moveCtrl;
    [HideInInspector] public HeroAnimationCotroller anim;

    public static PlayerStatusController Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        moveCtrl = GetComponent<PlayerMoveController>();
        anim = GetComponent<HeroAnimationCotroller>();
        InitalizePlayer();
    }

    private void Update()
    {
        if (floatingEnergy <= lowEnergyNum && !isLowEnergy)
        {
            LowEnergy();
        }

        if (floatingEnergy > lowEnergyNum && isLowEnergy)
        {
            QuitLowEnergy();
        }

        if (floatingEnergy<=0 && !isDead)
        {
            CharacterDie();
        }

    }

    private void InitalizePlayer()
    {
        birthPos = transform.position;
        isLowEnergy = false;
        isDead = false;
        floatingEnergy = 100f;
    }

  

    public void PauseMove()
    {
        moveCtrl.PausePlayerMove();
    }

    public void ResumeMove()
    {
        moveCtrl.ResumePlayerMove();
    }

    /// <summary>
    /// 耗能
    /// </summary>
    public void CostEnergy()
    {
        floatingEnergy -= floatingEnegyCost * Time.deltaTime;

        anim.ChangeHeroSize(floatingEnergy / 100f);
    }

    /// <summary>
    /// 充能
    /// </summary>
    /// <param name="eng"></param>
    public void ChargeEnergy(float eng)
    {
        // 充能动画
        anim.PlayAnimation_Charge();

        floatingEnergy += eng;
        floatingEnergy = Mathf.Min(floatingEnergy, 100f);

        anim.ChangeHeroSize(floatingEnergy/100f);

    }


    private void LowEnergy()
    {
        isLowEnergy = true;

        // 动画
        anim.PlayAnimation_LowEnergy();

        // 声音
        MusicPlayer.Instance.PlaySound(SoundType.LowEnergy);
    }

    private void QuitLowEnergy()
    {
        isLowEnergy = false;

        // 动画
        anim.PlayAnimation_QuitLowEnergy();

        // 声音
        MusicPlayer.Instance.StopSound(SoundType.LowEnergy);
    }

    public void ResetPlayer()
    {
        // 触发事件:声音
        OnPlayerReset?.Invoke();

        transform.position = birthPos;
        moveCtrl.ResetPlayerMove();
        moveCtrl.ResumePlayerMove();
        floatingEnergy = 100f;
        isLowEnergy = false;
        isDead = false;
        anim.Reset();

    }

    public void CharacterDie()
    {
        if (isDead) return;

        Debug.Log("YOU DIE");
        isDead = true;

        // 停止玩家控制
        moveCtrl.PausePlayerMove();

        // 动画
        anim.PlayAnimation_Death();

        // parent
        transform.SetParent(null);

        // 角色死亡，触发事件:声音
        OnCharacterDeath?.Invoke();

        // 镜头重置特效
        StartCoroutine(PlayCamEffect());

    }

    IEnumerator PlayCamEffect()
    {
        yield return new WaitForSeconds(1f);

        SceneEffectController.Instance.CameraResetEffect_IN();

        StartCoroutine(ReTryLevel());
    }

    IEnumerator ReTryLevel()
    {
        yield return new WaitForSeconds(1f);

        ResetPlayer();
        SceneEffectController.Instance.CameraResetEffect_OUT();

    }



}
