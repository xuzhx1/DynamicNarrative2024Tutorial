using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEffectController : MonoBehaviour
{
    private Animator CamResetEft;

    public static SceneEffectController Instance;

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
        CamResetEft = GetComponent<Animator>();

        //CameraResetEffect_OUT();
    }


    public void CameraResetEffect_IN()
    {
        CamResetEft.SetTrigger("ResetIn");
    }

    public void CameraResetEffect_OUT()
    {
        CamResetEft.SetTrigger("ResetOut");
    }

    public void CameraResetEffect_Close()
    {
        CamResetEft.SetTrigger("CloseEffect"); 
    }


}
