using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool isMovingPlt = false;
    public float movingSpeed = 1f;

    private bool reverse = false;

    private Vector3 endPos;
    private Vector3 startPos;

    private void Start()
    {
        endPos = transform.GetChild(0).position;
        startPos = transform.position;
    }

    private void Update()
    {
        if(isMovingPlt && endPos!=null)
        {
            transform.position = Vector3.Lerp(transform.position, reverse ? startPos : endPos,
                movingSpeed * Time.deltaTime);

            float dis = (transform.position - (reverse ? startPos : endPos)).magnitude;

            if (dis < 1f)
            {
                reverse = !reverse;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
