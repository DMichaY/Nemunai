using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KaitoFight : FighterClass
{
    public float speed = 3.0f;
    public Slider lifeBar;

    private Animator kaitoAnimator;
    private Vector2 movement;
    private bool canMove = true;
    private Rigidbody rb;
    private float life = 100;

    void Start()
    {
        // Buscar los componentes
        kaitoAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        lifeBar.maxValue = life;
        lifeBar.value = life;

        if (kaitoAnimator == null)
        {
            Debug.LogError("No se encontró un Animator en el hijo Kaito.");
        }
    }

    //Movimiento por rigidbody
    void Update()
    {
        if (!kaitoAnimator.GetBool("crouch") && canMove) rb.velocity = new Vector3(0, 0, movement.x * speed);
        else rb.velocity = Vector3.zero;
    }

    //Animaciones y dirección de movimiento
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

    //Animaciones de ataque
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

    //Activar movimiento después de atacar
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

    //Desactivar movimiento durante ataques
    public void DeactivateMovement()
    {
        canMove = false;
        kaitoAnimator.SetBool("goRight", false);
        kaitoAnimator.SetBool("goLeft", false);
        kaitoAnimator.SetBool("crouch", false);
    }

    //Perder vida y activar animación de recibir golpe
    public override void GetHit(float damage)
    {
        life -= damage;
        lifeBar.value = life;
        if (Random.Range(0, 2) == 0) kaitoAnimator.SetTrigger("damage1");
        else kaitoAnimator.SetTrigger("damage2");
    }
}