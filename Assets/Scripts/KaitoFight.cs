using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KaitoFight : MonoBehaviour
{
    public float speed = 4.0f;
    private Animator kaitoAnimator;
    private Vector2 movement;
    private bool canMove = true;

    void Start()
    {
        // Buscar el Animator en el hijo Kaito
        kaitoAnimator = GetComponentInChildren<Animator>();

        if (kaitoAnimator == null)
        {
            Debug.LogError("No se encontró un Animator en el hijo Kaito.");
        }
    }

    void Update()
    {
        if (!kaitoAnimator.GetBool("crouch") && canMove) transform.position = transform.position + new Vector3(0, 0, movement.x * Time.deltaTime * speed);
    }

    public void OnMovimiento(InputValue value)
    {
        movement = value.Get<Vector2>();

        if (movement.x > 0) kaitoAnimator.SetBool("goRight", true);
        else kaitoAnimator.SetBool("goRight", false);
        if (movement.x < 0) kaitoAnimator.SetBool("goLeft", true);
        else kaitoAnimator.SetBool("goLeft", false);
        if (movement.y < 0) kaitoAnimator.SetBool("crouch", true);
        else kaitoAnimator.SetBool("crouch", false);
    }

    public void OnAtacar(InputValue value)
    {
        Vector2 vectorAtaque = value.Get<Vector2>();

        if (vectorAtaque.x > 0)
        {
            kaitoAnimator.SetTrigger("kickRight");
            kaitoAnimator.ResetTrigger("punchLeft");
            kaitoAnimator.ResetTrigger("punchRight");
            kaitoAnimator.ResetTrigger("kickLeft");
        }
        else if (vectorAtaque.x < 0)
        {
            kaitoAnimator.SetTrigger("punchLeft");
            kaitoAnimator.ResetTrigger("kickRight");
            kaitoAnimator.ResetTrigger("punchRight");
            kaitoAnimator.ResetTrigger("kickLeft");
        }
        else if (vectorAtaque.y > 0)
        {
            kaitoAnimator.SetTrigger("punchRight");
            kaitoAnimator.ResetTrigger("kickRight");
            kaitoAnimator.ResetTrigger("punchLeft");
            kaitoAnimator.ResetTrigger("kickLeft");
        }
        else if (vectorAtaque.y < 0)
        {
            kaitoAnimator.SetTrigger("kickLeft");
            kaitoAnimator.ResetTrigger("kickRight");
            kaitoAnimator.ResetTrigger("punchLeft");
            kaitoAnimator.ResetTrigger("punchRight");
        }
        canMove = false;
        kaitoAnimator.SetBool("goRight", false);
        kaitoAnimator.SetBool("goLeft", false);
        kaitoAnimator.SetBool("crouch", false);
    }

    public void ActivateMovement()
    {
        canMove = true;
        if (movement.x > 0) kaitoAnimator.SetBool("goRight", true);
        else kaitoAnimator.SetBool("goRight", false);
        if (movement.x < 0) kaitoAnimator.SetBool("goLeft", true);
        else kaitoAnimator.SetBool("goLeft", false);
        if (movement.y < 0) kaitoAnimator.SetBool("crouch", true);
        else kaitoAnimator.SetBool("crouch", false);
    }
}
