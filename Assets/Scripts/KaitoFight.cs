using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KaitoFight : MonoBehaviour
{
    public float speed = 4.0f;
    private Animator kaitoAnimator;
    private Vector2 movimiento;

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
        if (!kaitoAnimator.GetBool("crouch")) transform.position = transform.position + new Vector3(0, 0, movimiento.x * Time.deltaTime * speed);
    }

    public void OnMovimiento(InputValue value)
    {
        movimiento = value.Get<Vector2>();

        if (movimiento.x > 0) kaitoAnimator.SetBool("goRight", true);
        else kaitoAnimator.SetBool("goRight", false);
        if (movimiento.x < 0) kaitoAnimator.SetBool("goLeft", true);
        else kaitoAnimator.SetBool("goLeft", false);
        if (movimiento.y < 0) kaitoAnimator.SetBool("crouch", true);
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
    }
}
