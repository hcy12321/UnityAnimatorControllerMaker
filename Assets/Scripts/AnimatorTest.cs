using UnityEngine;
using System.Collections;

public class AnimatorTest : MonoBehaviour
{
    private Animator animator;

    public string animationName = "run";

    public float Speed = 1.0f;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 添加一些测试功能按钮
    /// </summary>
    void OnGUI()
    {
#if UNITY_EDITOR
        if (GUILayout.Button("Play"))
        {
            Play(animationName);
        }
        if (GUILayout.Button("Replay"))
        {
            RePlay(animationName);
        }
        if (GUILayout.Button("Pause"))
        {
            Pause();
        }
#endif
    }

    /// <summary>
    /// 设置速度
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    /// <summary>
    /// 重新播放指定名称动画
    /// </summary>
    /// <param name="name"></param>
    public void RePlay(string name)
    {
        animator.speed = Speed;
        animator.Play(name, 0, 0.0f);
    }

    /// <summary>
    /// 播放指定名称动画
    /// </summary>
    /// <param name="name"></param>
    public void Play(string name)
    {
        animator.speed = Speed;
        animator.Play(name);
    }

    /// <summary>
    /// 暂停动画
    /// </summary>
    public void Pause()
    {
        animator.speed = 0.0f;
    }
}
