using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GroundSetting : MonoBehaviour
{
    public GameObject childPrefab;
    public int maxChildren = 10; // 最大子物体数量
    public float size = 1;
    [Range(0,5)]public float offset = 0;

    [SerializeField] private int childNum;
    private float tempSize;

    private void Start()
    {
        AdjustNum();
        tempSize = size;
    }

    private void Update()
    {
        if (size != tempSize)
        {
            AdjustChildren();
            tempSize = size;
        }
    }

    private int AdjustNum()
    {
        childNum = (int)Mathf.Round(size) + 1;
        return childNum;
    }


    private void AdjustChildren()
    {
        AdjustNum();

        // 根据需要增加或删除子物体
        while (transform.childCount <= maxChildren && AdjustNum() >= GetComponentsInChildren<Transform>().Length)
        {
            GameObject newChild = Instantiate(childPrefab, transform);
            newChild.transform.localPosition = Vector3.zero; // 将新子物体的位置设置为父物体的中心
        }
        while (transform.childCount > maxChildren || AdjustNum() < GetComponentsInChildren<Transform>().Length)
        {
            DestroyImmediate(transform.GetChild(transform.childCount - 1).gameObject); // 删除最后一个子物体
        }

        Transform[] children = GetComponentsInChildren<Transform>();

        // 调整子物体的位置
        Vector3 nowPos = transform.position;

        for (int i = 0; i < childNum - 1; i++)
        {
            children[i].localPosition = new Vector2( - size / 2f  + size / childNum * i, 0) * offset;
        }

        transform.position = nowPos;
    }
}
