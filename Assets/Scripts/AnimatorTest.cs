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


    void OnGUI()
    {
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
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public void RePlay(string name)
    {
        animator.speed = Speed;
        animator.Play(name, 0, 0.0f);
    }

    public void Play(string name)
    {
        animator.speed = Speed;
        animator.Play(name);
    }

    public void Pause()
    {
        animator.speed = 0.0f;
    }
}
