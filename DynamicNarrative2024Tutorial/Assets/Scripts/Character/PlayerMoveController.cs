using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float acceleration; // 加速度
    public float maxSpeed; // 最高速度
    public float deceleration; // 减速度
    public float jumpForce; // 跳跃力度
    public float brakingForce; //急停速率
    public float floatingForce; //悬浮力度
    

    private bool isJumping = false;
    private bool isPause = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isPause)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        if(!Mathf.Approximately(moveHorizontal,0))
        {
            Accelerate(moveHorizontal);
        }
        else
        {
            Decelerate();
        }

        if (Input.GetButtonDown("Jump") && !isJumping && rb.velocity.y <= 0)
        {
            Jump();
        }

        if (Input.GetButton("Jump") && isJumping && rb.velocity.y < 1f)
        {
            Floating();
        }

        if(Input.GetButtonUp("Jump") && isJumping)
        {
            // 动画
            PlayerStatusController.Instance.anim.PlayAnimation_QuitFloating();
        }

    }

    private void Accelerate(float moveHorizontal)
    {
        
        if (Mathf.Abs(rb.velocity.x) < maxSpeed || rb.velocity.x * moveHorizontal < 0)
        {
            float reverseAcceleration = rb.velocity.x * moveHorizontal < 0 ? brakingForce : 1f;
            rb.AddForce(new Vector2(moveHorizontal * acceleration * reverseAcceleration, 0));

            // 动画
            PlayerStatusController.Instance.anim.PlayAnimation_Moving();

            // 声音
            MusicPlayer.Instance.StopSound(SoundType.Decelerate);
            MusicPlayer.Instance.PlaySound(SoundType.Move);
        }
    }

    private void Decelerate()
    {
        if (rb.velocity.x != 0)
        {
            float newVelocity = Mathf.Lerp(rb.velocity.x, 0, deceleration * Time.deltaTime);
            rb.velocity = new Vector2(newVelocity, rb.velocity.y);

            // 动画
            PlayerStatusController.Instance.anim.SetEffectWaiting(true);

            // 声音
            MusicPlayer.Instance.StopSound(SoundType.Move);
            MusicPlayer.Instance.PlaySound(SoundType.Decelerate);
        }
        else
        {
            // 动画
            PlayerStatusController.Instance.anim.PlayAnimation_Idle();

            // 声音
            MusicPlayer.Instance.StopSound(SoundType.Move);
            MusicPlayer.Instance.StopSound(SoundType.Decelerate);
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isJumping = true;

        // 动画
        PlayerStatusController.Instance.anim.PlayAnimation_Jump();

        // 声音
        MusicPlayer.Instance.PlaySound(SoundType.Jump);
    }

    private void Floating()
    {
        rb.AddForce(new Vector2(0, floatingForce * Time.deltaTime * 100f));
        PlayerStatusController.Instance.CostEnergy();

        // 动画
        PlayerStatusController.Instance.anim.PlayAnimation_Floating();

        // 声音
        MusicPlayer.Instance.PlaySound(SoundType.Floating);
    }

    public void PausePlayerMove()
    {
        isPause = true;
        rb.velocity = Vector2.zero;
    }

    public void ResumePlayerMove()
    {
        isPause = false;
    }

    public void ResetPlayerMove()
    {
        rb.velocity = Vector2.zero;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

            // 动画
            PlayerStatusController.Instance.anim.PlayAnimation_QuitFallling();

            // 声音
            if (rb.velocity.y != 0f)
                MusicPlayer.Instance.PlaySound(SoundType.Hit);
        }

        if(collision.gameObject.CompareTag("DeathZone"))
        {
            PlayerStatusController.Instance.CharacterDie();
            Debug.Log("Reach Death Zone!");

            
        }
    }
}
