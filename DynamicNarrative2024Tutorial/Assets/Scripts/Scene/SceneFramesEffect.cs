using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFramesEffect : MonoBehaviour
{
    public Transform[] frames;
    public Vector2 maxOffset;
    [Range(0.5f,1f)]public float biasAmount = 0.75f;

    private Vector3 playerOriginPos;

    // Start is called before the first frame update
    void Start()
    {
        playerOriginPos = PlayerStatusController.Instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<frames.Length;i++)
        {
            Vector3 bias = (playerOriginPos - PlayerStatusController.Instance.transform.position);
            float biasX = (2 / (1 + Mathf.Exp(-bias.x)) - 1) * maxOffset.x;
            float biasY = (2 / (1 + Mathf.Exp(-bias.y)) - 1) * maxOffset.y;
            frames[i].localPosition = new Vector2(biasX, biasY) * Mathf.Pow(biasAmount,i);
        }
    }
}
