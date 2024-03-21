using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationCotroller : MonoBehaviour
{
    public Animator heroAnim;
    public Animator heroEffectAnim;
    [Range(0, 1)] public float lowEnergyMinSize = 0.75f;

    private Vector3 originHeroSize;

    private void Start()
    {
        originHeroSize = heroAnim.transform.localScale;
    }

    public void ChangeHeroSize(float newScale)
    {
        float shrinkSize = (1f - (1f - lowEnergyMinSize) * (1f - newScale));
        heroAnim.transform.localScale = Vector3.one * shrinkSize;
        heroEffectAnim.transform.localScale = Vector3.one * Mathf.Min(shrinkSize * 1.3f,1f);
    }

    public void PlayAnimation_Moving()
    {
        SetAnimWaiting(false);
        heroAnim.SetBool("IsMoving", true);
        heroEffectAnim.SetBool("IsMoving", true);
    }

    public void PlayAnimation_Moving(Animator anim)
    {
        anim.SetBool("IsMoving", true);
    }

    public void PlayAnimation_Idle()
    {
        SetAnimWaiting(false);
        heroAnim.SetBool("IsMoving", false);
        heroEffectAnim.SetBool("IsMoving", false);
    }

    public void PlayAnimation_Idle(Animator anim)
    {
        anim.SetBool("IsMoving", false);
    }

    public void PlayAnimation_Jump()
    {
        SetAnimWaiting(false);
        heroAnim.SetTrigger("Jump");
        heroEffectAnim.SetTrigger("Jump");
        heroAnim.SetBool("Falling", true);
        heroEffectAnim.SetBool("Falling", true);
    }


    public void PlayAnimation_QuitFallling()
    {
        PlayAnimation_QuitFloating();
        heroAnim.SetBool("Falling", false);
        heroEffectAnim.SetBool("Falling", false);
    }

    public void PlayAnimation_Floating()
    {
        SetAnimWaiting(false);
        heroAnim.SetBool("Floating", true);
        heroEffectAnim.SetBool("Floating", true);
    }

    public void PlayAnimation_QuitFloating()
    {
        heroAnim.SetBool("Floating", false);
        heroEffectAnim.SetBool("Floating", false);
    }

    public void PlayAnimation_LowEnergy()
    {
        SetAnimWaiting(false);
        heroAnim.SetBool("IsLowEnergy", true);
        heroEffectAnim.SetBool("IsLowEnergy", true);
    }

    public void PlayAnimation_QuitLowEnergy()
    {
        SetAnimWaiting(false);
        heroAnim.SetBool("IsLowEnergy", false);
        heroEffectAnim.SetBool("IsLowEnergy", false);
    }

    public void PlayAnimation_Charge()
    {
        SetAnimWaiting(false);
        heroAnim.SetTrigger("Charge");
        heroEffectAnim.SetTrigger("Charge");
    }

    public void PlayAnimation_Death()
    {
        SetAnimWaiting(false);
        heroAnim.SetTrigger("Death");
        heroEffectAnim.SetBool("IsWaiting", true);
    }

    public void PlayAnimation_Win()
    {
        SetAnimWaiting(false);
        heroAnim.SetTrigger("Win");
        heroEffectAnim.SetTrigger("Win");
    }
        public void SetAnimWaiting(bool isWaiting)
    {
        heroAnim.SetBool("IsWaiting", isWaiting);
        heroEffectAnim.SetBool("IsWaiting", isWaiting);
    }

    public void SetAnimWaiting(Animator anim,bool isWaiting)
    {
        anim.SetBool("IsWaiting", isWaiting);
    }

    public void SetEffectWaiting(bool isWaiting)
    {
        heroEffectAnim.SetBool("IsWaiting", isWaiting);
    }


    public void Reset()
    {
        heroAnim.transform.localScale = originHeroSize;
        heroAnim.SetTrigger("ResetAll");
        heroEffectAnim.SetTrigger("ResetAll");
        heroAnim.SetBool("Falling", false);
        heroEffectAnim.SetBool("Falling", false);
        heroAnim.SetBool("IsWaiting", false);
        heroEffectAnim.SetBool("IsWaiting", false);
    }
}
