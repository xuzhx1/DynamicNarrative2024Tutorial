using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    private float charsPerSecond;//打字时间间隔
    private string words;//保存需要显示的文字

    [HideInInspector] public bool isActive = false;
    private float timer;//计时器
    private TextMeshProUGUI tmp;
    private DialogBox box;
    [SerializeField]private int currentPos = 0;//当前打字位置

    // Use this for initialization
    void Start()
    {
        timer = 0;
        isActive = false;
        tmp = GetComponent<TextMeshProUGUI>();
        box = GetComponentInParent<DialogBox>();
        charsPerSecond = box.charsPerSecond;
        charsPerSecond = Mathf.Max(0.001f, charsPerSecond);
        words = tmp.text;
        tmp.text = "";//获取Text的文本信息，保存到words中，然后动态更新文本显示内容，实现打字机的效果
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
            OnStartWriter();
    }

    public void StartTyper()
    {
        isActive = true;
    }

    /// <summary>
    /// 执行打字任务
    /// </summary>
    private void OnStartWriter()
    {
        // 跳过打字
        if(Input.GetButtonDown("Jump"))
        {
            OnFinish();
            return;
        }

        timer += Time.deltaTime;
        if (timer >= charsPerSecond)
        {
            timer = 0;
            currentPos++;
            tmp.text = words.Substring(0, currentPos);//刷新文本显示内容

            if (currentPos >= words.Length-1 || currentPos >= 40)
            {
                OnFinish();
            }
        }

    }
    /// <summary>
    /// 结束打字，初始化数据
    /// </summary>
    void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        tmp.text = words;

        box.FinishOneTyper();
    }

    public void Reset()
    {
        words = tmp.text;
        tmp.text = "";
        timer = 0;
        currentPos = 0;
    }


}