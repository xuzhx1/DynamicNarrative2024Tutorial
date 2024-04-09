using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuizheSu;

namespace HuizheSu {
    public class DialogBoxModified : MonoBehaviour
    {
        public float charsPerSecond = 0.02f;//打字时间间隔

        [HideInInspector] public TypewriterEffectModified[] typers;
        private bool hasPlayerEntered = false;
        private bool isDialoguePlaying = false; // 是否正在播放对话
        private bool[] typerHasPlayed;
        private int typerPlayingIndex;
        private bool isDialogueBoxHasAllPlayed; // 是否是已经全部播放过的对话
        private Animator anim;
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private bool followThePlayer = false;


        private void Start()
        {
            typers = GetComponentsInChildren<TypewriterEffectModified>();
            typerHasPlayed = new bool[typers.Length];
            typerPlayingIndex = 0;

            anim = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            ChangeColor();

        }

        private void Update()
        {
            if (!hasPlayerEntered) return;

            // 对话框间按键播放
            if (!isDialoguePlaying && !isDialogueBoxHasAllPlayed && Input.GetButtonDown("Jump"))
            {
                PlayNexrTyper();
            }
        }

        private void StartDialogue()
        {
            isDialoguePlaying = true;
            SetDialogAnim(true);
            StartCoroutine(LateStartTyper());
            MusicPlayer.Instance.PlaySound(SoundType.Dialog);
        }

        public void PlayNexrTyper()
        {
            typers[typerPlayingIndex].Reset();

            if (typerPlayingIndex == typers.Length - 1)
            {
                FinishAllTypers();
            }
            else
            {
                typerPlayingIndex++;
                isDialoguePlaying = true;
                ChangeColor();
                typers[typerPlayingIndex].StartTyper();
                MusicPlayer.Instance.PlaySound(SoundType.Dialog);
            }

        }

        IEnumerator LateStartTyper()
        {
            yield return new WaitForSeconds(0.5f);

            typers[typerPlayingIndex].StartTyper();

        }

        public void FinishOneTyper()
        {
            typerHasPlayed[typerPlayingIndex] = true;
            isDialoguePlaying = false;

        }

        private void FinishAllTypers()
        {
            isDialogueBoxHasAllPlayed = true;
            SetDialogAnim(false);
            StartCoroutine(ResumePlayerMove());
        }

        IEnumerator ResumePlayerMove()
        {
            yield return new WaitForSeconds(0.5f);

            PlayerStatusController.Instance.ResumeMove();
        }

        public void SetDialogAnim(bool isShow)
        {
            anim.SetBool("Show", isShow);
        }

        public void ResetBox()
        {
            hasPlayerEntered = false;
            typerPlayingIndex = 0;
            isDialoguePlaying = false;
            typerHasPlayed = new bool[typers.Length];
            isDialogueBoxHasAllPlayed = false;
            SetDialogAnim(false);

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isDialogueBoxHasAllPlayed) return;

            // 检查碰撞的对象是否是传送门
            if (other.gameObject.CompareTag("Player"))
            {
                hasPlayerEntered = true;

                // 玩家输入停止
                PlayerStatusController.Instance.PauseMove();

                // 开始播放对话
                StartDialogue();
                if(followThePlayer)
                {
                    transform.SetParent(other.transform);
                }
            }
        }

        private void ChangeColor()
        {
            switch (typers[typerPlayingIndex].color)
            {
                case TypewriterEffectModified.EColor.Black:
                    _spriteRenderer.color = new Color(0f, 0f, 0f);
                    break;
                case TypewriterEffectModified.EColor.White:
                    _spriteRenderer.color = new Color(1f, 1f, 1f);
                    break;
                case TypewriterEffectModified.EColor.Red:
                    _spriteRenderer.color = new Color(1f, 0f, 0f);
                    break;
            }

        }

        private void OnEnable()
        {
            PlayerStatusController.OnCharacterDeath += ResetBox;
        }

        private void OnDisable()
        {
            PlayerStatusController.OnCharacterDeath -= ResetBox;
         

        }

    }


}



