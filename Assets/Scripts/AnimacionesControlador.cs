using UnityEngine;

public class AnimacionesControlador : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIsWalking(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    public void SetIsBackwards(bool isBackwards)
    {
        animator.SetBool("IsBackwards", isBackwards);
    }

    public void SetIsLeft(bool isLeft)
    {
        animator.SetBool("IsLeft", isLeft);
    }

    public void SetIsRight(bool isRight)
    {
        animator.SetBool("IsRight", isRight);
    }

    public void SetIsSprinting(bool isSprinting)
    {
        animator.SetBool("IsSprinting", isSprinting);
    }
}
