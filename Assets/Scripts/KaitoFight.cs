using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KaitoFight : MonoBehaviour
{
    public float moveAmount = 1.0f; // Cantidad que se mueve en Z cuando se pulsa A o D
    private Animator kaitoAnimator;
    private Transform kaitoTransform;
    private bool isActionInProgress = false;
    private Vector2 movimiento;

    void Start()
    {
        // Buscar el Animator en el hijo Kaito
        kaitoAnimator = GetComponentInChildren<Animator>();
        kaitoTransform = GetComponentInChildren<Transform>();

        if (kaitoAnimator == null)
        {
            Debug.LogError("No se encontró un Animator en el hijo Kaito.");
        }
    }

    void Update()
    {
<<<<<<< Updated upstream
        if (!kaitoAnimator.GetBool("crouch")) transform.position = transform.position + new Vector3(0, 0, movimiento.x * Time.deltaTime * 2);
=======
        /*
        if (isActionInProgress)
        {
            // Si una animación está en progreso, no se permite otra acción
            return;
        }

        // Comprobar las teclas y ejecutar las animaciones
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(PerformAction("goLeft", moveAmount));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(PerformAction("goRight", -moveAmount));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(PerformAction("crouch"));
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(PerformAction("punchLeft"));
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(PerformAction("punchRight"));
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(PerformAction("kickLeft"));
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(PerformAction("kickRight"));
        }
        else
        {
            // Si no se pulsa nada, ejecutar la animación de idleFight
            kaitoAnimator.SetTrigger("idleFight");
        }
        */
        AplicarMovimiento();
    }

    public void OnMovimiento(InputValue value)
    {
        movimiento = value.Get<Vector2>();

        if (movimiento.x > 0) kaitoAnimator.SetBool("goRight", true);
        else kaitoAnimator.SetBool("goRight", false);
        if (movimiento.x < 0) kaitoAnimator.SetBool("goLeft", true);
        kaitoAnimator.SetBool("goLeft", false);

    }

    void AplicarMovimiento()
    {
        

        transform.position = transform.position + new Vector3(0, 0, -movimiento.x * Time.deltaTime * 2);
>>>>>>> Stashed changes
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

        if (movimiento.magnitude == 0) kaitoAnimator.SetTrigger("idle");

    }

    public void OnAtacar(InputValue value)
    {
        Vector2 vectorAtaque = value.Get<Vector2>();

        if (vectorAtaque.x > 0) kaitoAnimator.SetTrigger("kickRight");
        else if (vectorAtaque.x < 0) kaitoAnimator.SetTrigger("punchLeft");
        else if (vectorAtaque.y > 0) kaitoAnimator.SetTrigger("punchRight");
        else if (vectorAtaque.y < 0) kaitoAnimator.SetTrigger("kickLeft");
    }
}
